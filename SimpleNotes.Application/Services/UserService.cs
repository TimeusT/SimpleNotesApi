using FluentResults;
using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Mapping;
using SimpleNotes.Infrastructure;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Result<IEnumerable<UserDomain>> ListUsers()
    {
        try
        {
            var users = _userRepository.ListUsers().Select(x => x.ToDomain());

            if (users == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "No user exists."));
            }

            return Result.Ok(users);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<UserDomain?> GetUser(int id)
    {
        try
        {
            var user = _userRepository.GetUser(id)?.ToDomain();

            if (user == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID does not exist."));
            }

            return user;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<UserDomain?> GetByEmail(EmailText email)
    {
        try
        {
            var userEmail = _userRepository.GetByEmail(email)?.ToDomain();

            if (userEmail == null)
            {
                return Result.Fail(new ValidationError().WithError("Email", "This email does not exist."));
            }

            return userEmail;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<IEnumerable<NoteDomain>> GetUserNotes(int id)
    {
        try
        {
            var userNotes = _userRepository.GetUserNotes(id).Select(n => n.ToDomain());

            return Result.Ok(userNotes);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<UserDomain> CreateUser(UserDomain user)
    {
        try
        {
            var userExists = _userRepository.GetUser(user.Id);

            if (userExists != null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID already exists."));
            }

            var userEntity = user.ToEntity();

            var userCreate = _userRepository.CreateUser(userEntity);

            var userDomain = userCreate.ToDomain();

            return Result.Ok(userDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<bool> UpdateUser(UserDomain user)
    {
        try
        {
            var userEntiy = user.ToEntity();

            var userUpdate = _userRepository.UpdateUser(userEntiy);

            return Result.Ok(userUpdate);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public Result<bool> DeleteUser(int id)
    {
        try
        {
            var userExists = _userRepository.GetUser(id);

            if (userExists == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID does not exist."));
            }

            var userDelete = _userRepository.DeleteUser(id);

            return Result.Ok(userDelete);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
