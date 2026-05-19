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

    public async Task<Result<IEnumerable<UserDomain>>> ListUsersAsync()
    {
        try
        {
            var entities = await _userRepository.ListUsersAsync();
            var users = entities.Select(x => x.ToDomain());

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

    public async Task<Result<UserDomain?>> GetUserAsync(int id)
    {
        try
        {
            var user = await _userRepository.GetUserAsync(id);
            var userDomain = user?.ToDomain();

            if (userDomain == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID does not exist."));
            }

            return userDomain;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<UserDomain?>> GetByEmailAsync(EmailText email)
    {
        try
        {
            var userEmail = await _userRepository.GetByEmailAsync(email);
            var userDomain = userEmail?.ToDomain();

            if (userDomain == null)
            {
                return Result.Fail(new ValidationError().WithError("Email", "This email does not exist."));
            }

            return userDomain;
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<IEnumerable<NoteDomain>>> GetUserNotesAsync(int id)
    {
        try
        {
            var userNotes = await _userRepository.GetUserNotesAsync(id);
            var userNotesDomain = userNotes.Select(n => n.ToDomain());

            return Result.Ok(userNotesDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<UserDomain>> CreateUserAsync(UserDomain user)
    {
        try
        {
            var userExists = await _userRepository.GetUserAsync(user.Id);

            if (userExists != null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID already exists."));
            }

            var userEntity = user.ToEntity();
            var userCreate = await _userRepository.CreateUserAsync(userEntity);
            var userDomain = userCreate.ToDomain();

            return Result.Ok(userDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateUserAsync(UserDomain user)
    {
        try
        {
            var userEntiy = user.ToEntity();
            var userUpdate = await _userRepository.UpdateUserAsync(userEntiy);

            return Result.Ok(userUpdate);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteUserAsync(int id)
    {
        try
        {
            var userExists = await _userRepository.GetUserAsync(id);

            if (userExists == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID does not exist."));
            }

            var userDelete = await _userRepository.DeleteUserAsync(id);

            return Result.Ok(userDelete);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
