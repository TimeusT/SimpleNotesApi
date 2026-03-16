using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface IUserService
{
    // List all users
    Result<IEnumerable<UserDomain>> ListUsers();
    // Get by Id
    Result<UserDomain?> GetUser(int id);
    // Get by Email
    UserDomain? GetByEmail(EmailText email);
    // Get notes with User Id
    //Result<IEnumerable<NoteDomain>> GetUserNotes(int id);
    // Create user
    Result<UserDomain> CreateUser(UserDomain user);
    // Update user
    Result<bool> UpdateUser(UserDomain user);
    // Delete user
    Result<bool> DeleteUser(int id);
}
