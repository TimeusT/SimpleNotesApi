using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<UserEntity> ListUsers()
    {
        // TODO
        // List all users
    }

    public UserDomain GetUser(int id)
    {
        // TODO
        // Get user by id
    }

    public UserDomain CreateUser(UserDomain user)
    {
        // TODO
        // Convert to Entity
        // Create user
        // Convert to Domain
        // return
    }

    public bool UpdateUser(UserDomain user)
    {
        // TODO
        // Convert to Entity
        // Update
        // Return
    }

    public bool DeleteUser(UserDomain user)
    {
        // TODO
        // Delete user
        // Return
    }
}
