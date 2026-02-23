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
        return _context.Users.ToList();
    }

    // Get their id
    public UserEntity? GetUser(int id)
    {
        return _context.Users.Find(id);
    }
    // Create user
    public UserEntity CreateUser(UserEntity user)
    {
        // TODO
        return user;
    }

    // Update user
    public bool UpdateUser(UserEntity user)
    {
        // TODO
        return true;
    }

    // Delete user
    public bool DeleteUser(int id)
    {
        // TODO
        return true;
    }
}

