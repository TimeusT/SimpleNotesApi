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
        return _context.Users.ToList();
    }

    // Get their id
    public UserEntity? GetUser(int id)
    {
        return _context.Users.Find(id);
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
        // Find user + error handling
        if (!_context.Users.Any(x => x.Id == user.Id)) return false;

        // Update user
        _context.Update(user);

        // Save
        _context.SaveChanges();

        return true;
    }

    // Delete user
    public bool DeleteUser(int id)
    {
        // Delete user
        //_context.Remove(id);
        // Iterate through all notes and remove them
        //_context.Remove(_context.Notes.Where(n => n.UserId == id).ToList());
        // save changes
        //_context.SaveChanges();

        var user = _context.Users.Include(u => u.Notes).FirstOrDefault(n => n.Id == id);

        if (user != null)
        {
            _context.Users.Remove(user);
        }
        _context.SaveChanges();

        return true;
    }
}

