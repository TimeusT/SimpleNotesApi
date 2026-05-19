using Azure;
using Azure.Data.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Data;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserEntity>> ListUsersAsync(CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.Address)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserEntity?> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.Address)
            .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }

    public async Task<UserEntity?> GetByEmailAsync(EmailText email, CancellationToken cancellationToken)
    {
        return await _context.Users
            .Include(x => x.Address)
            .FirstOrDefaultAsync(e => e.Email == email.Value);
    }

    public async Task<IEnumerable<NoteItemEntity>> GetUserNotesAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .Where(n => n.UserId == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<bool> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        if (user == null) return false;

        var updatedUser = await _context.Users
            .Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Id == user.Id,
                cancellationToken);

        if (updatedUser == null) return false;

        updatedUser.FirstName = user.FirstName;
        updatedUser.LastName = user.LastName;
        updatedUser.Age = user.Age;

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

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(n => n.Notes)
            .Include(a => a.Address)
            .FirstOrDefaultAsync(u => u.Id == id,
                cancellationToken);

        if (user == null) return false;

        _context.Users.Remove(user);

        await _context.SaveChangesAsync(cancellationToken);

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

    public async Task<IEnumerable<UserEntity>> ListUsersAsync(CancellationToken cancellationToken)
    {
        try
        {
            var entities = _tableClient.QueryAsync<UserTableStorageEntity>(cancellationToken: cancellationToken);
            var list = new List<UserTableStorageEntity>();

            await foreach (var entity in entities.WithCancellation(cancellationToken))
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

    public async Task<UserEntity?> GetByEmailAsync(EmailText email, CancellationToken cancellationToken)
    {
        try
        {
            var entityResponse = await _tableClient.GetEntityAsync<UserTableStorageEntity>(
                cancellationToken: cancellationToken,
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

    public async Task<UserEntity?> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            // Lookup table
            var lookupResponse = await _tableClient.GetEntityAsync<UserIdLookupEntity>(
                cancellationToken: cancellationToken,
                partitionKey: "UserId",
                rowKey: id.ToString()
            );

            // GetByEmail after finding the Lookup table (since this takes ID)
            return await GetByEmailAsync(EmailText.Create(lookupResponse.Value.Email), cancellationToken);
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return null;
        }
    }

    public async Task<IEnumerable<NoteItemEntity>> GetUserNotesAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<UserEntity> CreateUserAsync(UserEntity user, CancellationToken cancellationToken)
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

            await _tableClient.SubmitTransactionAsync(transation);

            return user;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken)
    {
        try
        {
            // Find user
            var getUser = await GetUserAsync(user.Id, cancellationToken);

            // Check is user exists
            if (getUser == null) return false;

            // Get user from table
            var existingUser = await _tableClient.GetEntityAsync<UserTableStorageEntity>(
                partitionKey: getUser.Email,
                rowKey: getUser.Email
            );

            var updateUser = existingUser.Value;

            // Update user
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.Age = user.Age;

            // Save changes
            await _tableClient.UpdateEntityAsync(updateUser, updateUser.ETag, TableUpdateMode.Merge);

            return true;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return false;
        }
    }

    public async Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            // Find user
            var user = await GetUserAsync(id, cancellationToken);

            // Delete entity and lookup entity
            await Task.WhenAll(_tableClient.DeleteEntityAsync(
                    partitionKey: user?.Email,
                    rowKey: user?.Email
                ),
                _tableClient.DeleteEntityAsync(
                    partitionKey: "UserId",
                    rowKey: id.ToString()
                ));

            return true;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            return false;
        }
    }
}
