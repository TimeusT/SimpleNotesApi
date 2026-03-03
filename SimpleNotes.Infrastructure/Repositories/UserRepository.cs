using Microsoft.EntityFrameworkCore;
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

