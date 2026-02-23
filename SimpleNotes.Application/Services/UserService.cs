using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Mapping;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<UserDomain> ListUsers()
    {
        return _userRepository.ListUsers().Select(u => u.ToDomain());
    }

    public UserDomain? GetUser(int id)
    {
        return _userRepository.GetUser(id)?.ToDomain();
    }

    public UserDomain CreateUser(UserDomain user)
    {
        // Convert to Entity
        var userEntity = user.ToEntity();
        // Create user
        var userCreated = _userRepository.CreateUser(userEntity);
        // Convert to Domain
        var userDomain = userCreated.ToDomain();
        // return
        return userDomain;
    }

    public bool UpdateUser(UserDomain user)
    {
        // Convert to Entity
        var userEntity = user.ToEntity();
        // Update
        var userUpdated = _userRepository.UpdateUser(userEntity);
        // Return
        return userUpdated;
    }

    public bool DeleteUser(int id)
    {
        // Delete user
        return _userRepository.DeleteUser(id);
    }
}
