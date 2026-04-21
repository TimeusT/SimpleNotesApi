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
    public UserEntity? GetUser(int id)
    {
        return _context.Users
            .Include(x => x.Address)
            .FirstOrDefault(x => x.Id == id);
    }

    // Get their email
    public UserEntity? GetByEmail(EmailText email)
    {
        return _context.Users
            .Include(x => x.Address)
            .FirstOrDefault(e => e.Email == email.Value);
    }

    // Get notes using User Id
    public IEnumerable<NoteItemEntity> GetUserNotes(int id)
    {
        // return the list of notes
        return _context.Notes.Where(n => n.UserId == id).ToList();
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
    public bool DeleteUser(int id)
    {
        // Find user that matched Id
        var user = _context.Users
            .Include(n => n.Notes)
            .Include(a => a.Address)
            .FirstOrDefault(u => u.Id == id);

        // If no user, then false
        if (user == null) return false;

        // Remove user and address
        _context.Users.Remove(user);

        _context.SaveChanges();

        return true;
    }
}

public class TableStorageOptions
{
    public required string ConnectionString { get; set; }
    public string UserTableName { get; set; } = "SimpleNotesUser";
}

public class TableStorageUserRepository : IUserRepository
{
    private readonly TableClient _tableClient;

    public TableStorageUserRepository(IOptions<TableStorageOptions> options)
    {
        var serviceClient = new TableServiceClient(options.Value.ConnectionString);
        _tableClient = serviceClient.GetTableClient(options.Value.UserTableName);
        _tableClient.CreateIfNotExists();
    }

    public IEnumerable<UserEntity> ListUsers()
    {
        try
        {
            var entities = _tableClient.Query<UserTableStorageEntity>();
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

    public UserEntity? GetUser(int id)
    {
        try
        {
            // Lookup table
            var lookupResponse = _tableClient.GetEntity<UserIdLookupEntity>(
                partitionKey: "UserId",
                rowKey: id.ToString()
            );

            // GetByEmail after finding the Lookup table (since this takes ID)
            return GetByEmail(EmailText.Create(lookupResponse.Value.Email));
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public IEnumerable<NoteItemEntity> GetUserNotes(int id)
    {
        throw new NotImplementedException();
    }

    public UserEntity CreateUser(UserEntity user)
    {
        try
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
                RowKey = user.Id.ToString(),
                Email = user.Email!,
            };

            // Assign properties
            newUser["Email"] = user.Email;
            newUser["Id"] = user.Id;
            newUser["FirstName"] = user.FirstName;
            newUser["LastName"] = user.LastName;
            newUser["Age"] = user.Age;
            newUser["JoinDate"] = user.JoinDate;

            //_tableClient.AddEntity(newUser);

            var transation = new List<TableTransactionAction>
            {
                new TableTransactionAction(TableTransactionActionType.Add, newUser),
                new TableTransactionAction(TableTransactionActionType.Add, lookupUser)
            };

            _tableClient.SubmitTransaction(transation);

            return user;
        }
        catch
        {
            throw;
        }
    }

    public bool UpdateUser(UserEntity user)
    {
        try
        {
            // Find user
            var getUser = GetUser(user.Id);

            // Check is user exists
            if (getUser == null) return false;

            // Get user from table
            var existingUser = _tableClient.GetEntity<UserTableStorageEntity>(
                partitionKey: getUser.Email,
                rowKey: getUser.Email
            );

            var updateUser = existingUser.Value;

            // Update user
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.Age = user.Age;

            // Save changes
            _tableClient.UpdateEntity(updateUser, updateUser.ETag, TableUpdateMode.Merge);

            return true;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return false;
        }
    }

    public bool DeleteUser(int id)
    {
        try
        {
            // GetUser by id then get email
            var user = GetUser(id);

            // Delete entity
            _tableClient.DeleteEntity(
                partitionKey: user?.Email,
                rowKey: user?.Email
            );

            // Delete lookup entity
            _tableClient.DeleteEntity(
                partitionKey: "UserId",
                rowKey: id.ToString()
            );

            return true;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return false;
        }
    }
}
