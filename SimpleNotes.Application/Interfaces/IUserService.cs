using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface IUserService
{
    // List all users
    IEnumerable<UserDomain> ListUsers();
    // Get by Id
    UserDomain? GetUser(int id);
    // Get notes with User Id
    IEnumerable<NoteDomain> GetUserNotes(int id);
    // Create user
    UserDomain CreateUser(UserDomain user);
    // Update user
    bool UpdateUser(UserDomain user);
    // Delete user
    bool DeleteUser(int id);
}
