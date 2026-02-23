using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Application.Interfaces;

public interface IUserService
{
    // List all users
    IEnumerable<UserDomain> ListUsers();
    // Get by Id
    UserDomain? GetUser(int id);
    // Create user
    UserDomain CreateUser(UserEntity user);
    // Update user
    bool UpdateUser(int id);
    // Delete user
    bool DeleteUser(int id);
}
