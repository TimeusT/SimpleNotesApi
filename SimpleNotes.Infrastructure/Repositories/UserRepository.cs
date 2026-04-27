using Azure;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Data;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Infrastructure.Repositories;

/*
Here should be where we update the database using the data
*/

public class UserRepository : IUserRepository
{
    // DI
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    // List all users
    public IEnumerable<UserEntity> ListUsers()
    {
        return _context.Users.Include(x => x.Address).ToList();
    }

    // Get their id
    public UserEntity? GetUser(string uniqueId)
    {
        return _context.Users
            .Include(x => x.Address)
            .FirstOrDefault(x => x.UniqueId == uniqueId);
    }

    // Get their email
    public UserEntity? GetByEmail(EmailText email)
    {
        return _context.Users
            .Include(x => x.Address)
            .FirstOrDefault(e => e.Email == email.Value);
    }

    // Get notes using User Id
    public IEnumerable<NoteItemEntity> GetUserNotes(string uniqueId)
    {
        // return the list of notes        
        return _context.Notes
            .Where(n => n.User.UniqueId == uniqueId).ToList();
    }

    // Create user
    public UserEntity CreateUser(UserEntity user)
    {
        // Create user
        _context.Users.Add(user);
        // Save
        _context.SaveChanges();

        return user;
    }

    // Update user
    public bool UpdateUser(UserEntity user)
    {
        if (user == null) return false;

        var updatedUser = _context.Users
            .Include(u => u.Address)
            .FirstOrDefault(u => u.Id == user.Id);

        if (updatedUser == null) return false;

        // Update User
        updatedUser.FirstName = user.FirstName;
        updatedUser.LastName = user.LastName;
        updatedUser.Age = user.Age;

        // Update or create address
        if (updatedUser.Address == null && user.Address != null)
        {
            updatedUser.Address = new AddressEntity();
        }

        if (updatedUser.Address != null && user.Address != null)
        {
            updatedUser.Address.StreetNo = user.Address.StreetNo;
            updatedUser.Address.City = user.Address.City;
            updatedUser.Address.State = user.Address.State;
            updatedUser.Address.PostalCode = user.Address.PostalCode;
            updatedUser.Address.Country = user.Address.Country;
        }

        _context.SaveChanges();

        return true;
    }

    // Delete user
    public bool DeleteUser(string uniqueId)
    {
        // Find user that matched Id
        var user = _context.Users
            .Include(n => n.Notes)
            .Include(a => a.Address)
            .FirstOrDefault(u => u.UniqueId == uniqueId);

        // If no user, then false
        if (user == null) return false;

        // Remove user and address
        _context.Users.Remove(user);

        _context.SaveChanges();

        return true;
    }
}

public class UserTableStorageOptions
{
    public required string ConnectionString { get; set; }
    public string UserTableName { get; set; } = "SimpleNotesUser";
}

public class TableStorageUserRepository : IUserRepository
{
    private readonly TableClient _tableClient;

    public TableStorageUserRepository(IOptions<UserTableStorageOptions> options)
    {
        var serviceClient = new TableServiceClient(options.Value.ConnectionString);
        _tableClient = serviceClient.GetTableClient(options.Value.UserTableName);
        _tableClient.CreateIfNotExists();
    }

    public IEnumerable<UserEntity> ListUsers()
    {
        try
        {
            var entities = _tableClient.Query<UserTableStorageEntity>(
                filter: "PartitionKey ne 'UserId'"
            );

            var list = new List<UserTableStorageEntity>();

            foreach (var entity in entities)
            {
                list.Add(entity);
            }

            return list;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            throw;
        }
    }

    public UserEntity? GetByEmail(EmailText email)
    {
        try
        {
            var entityResponse = _tableClient.GetEntity<UserTableStorageEntity>(
                partitionKey: email.Value,
                rowKey: email.Value
            );

            return entityResponse;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public UserEntity? GetUser(string uniqueId)
    {
        try
        {
            // Lookup table
            var lookupResponse = _tableClient.GetEntity<UserIdLookupEntity>(
                partitionKey: "UserId",
                rowKey: uniqueId
            );

            // GetByEmail after finding the Lookup table (since this takes ID)
            return GetByEmail(EmailText.Create(lookupResponse.Value.Email));
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public IEnumerable<NoteItemEntity> GetUserNotes(string uniqueId)
    {
        throw new NotImplementedException();
    }

    public UserEntity CreateUser(UserEntity user)
    {
        // Create entity
        var newUser = new TableEntity
        {
            PartitionKey = user.Email!,
            RowKey = user.Email!,
        };

        // Create lookup entity
        var lookupUser = new UserIdLookupEntity
        {
            PartitionKey = "UserId",
            RowKey = user.UniqueId,
            Email = user.Email!,
        };


        // Assign properties
        newUser["Email"] = user.Email;
        newUser["FirstName"] = user.FirstName;
        newUser["LastName"] = user.LastName;
        newUser["Age"] = user.Age;
        newUser["JoinDate"] = user.JoinDate;
        newUser["UniqueId"] = user.UniqueId;

        var transation = new List<TableTransactionAction>
            {
                new TableTransactionAction(TableTransactionActionType.Add, newUser),
                new TableTransactionAction(TableTransactionActionType.Add, lookupUser)
            };

        _tableClient.SubmitTransaction(transation);

        return user;
    }

    public bool UpdateUser(UserEntity user)
    {
        try
        {
            // Find the user
            var lookupUser = _tableClient.GetEntity<UserIdLookupEntity>(
                partitionKey: "UserId",
                rowKey: user.UniqueId
            );

            // Map to TableEntity because there is address and note
            var tableUser = user.ToTableEntity(lookupUser.Value.Email);

            // Save changes
            _tableClient.UpdateEntity(tableUser, ETag.All, TableUpdateMode.Merge);

            return true;
        }
        catch (RequestFailedException ex) when (ex.Status == 404) // For a specific error
        {
            return false;
        }
        catch (Exception ex) // For generic errors
        {
            return false;
        }
    }

    public bool DeleteUser(string uniqueId)
    {
        try
        {
            // GetUser by id 
            var user = GetUser(uniqueId);

            // Check user exists
            if (user == null) return false;

            // Get user entity
            var userEntity = new TableEntity
            {
                PartitionKey = user.Email,
                RowKey = user.Email
            };

            // Get matching lookup entity
            var lookupEntity = new UserIdLookupEntity
            {
                PartitionKey = "UserId",
                RowKey = uniqueId,
                Email = user.Email!
            };

            // Create transaction
            var transaction = new List<TableTransactionAction>
            {
                new TableTransactionAction(TableTransactionActionType.Delete, userEntity),
                new TableTransactionAction(TableTransactionActionType.Delete, lookupEntity)
            };

            // Delete transaction
            _tableClient.SubmitTransaction(transaction);

            return true;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return false;
        }
    }
}
