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

    public async Task<Result<IEnumerable<UserDomain>>> ListUsersAsync(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var entities = await _userRepository.ListUsersAsync(cancellationToken);
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

    public async Task<Result<UserDomain?>> GetUserAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var user = await _userRepository.GetUserAsync(id, cancellationToken);
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

    public async Task<Result<UserDomain?>> GetByEmailAsync(EmailText email, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var userEmail = await _userRepository.GetByEmailAsync(email, cancellationToken);
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

    public async Task<Result<IEnumerable<NoteDomain>>> GetUserNotesAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var userNotes = await _userRepository.GetUserNotesAsync(id, cancellationToken);
            var userNotesDomain = userNotes.Select(n => n.ToDomain());

            return Result.Ok(userNotesDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<UserDomain>> CreateUserAsync(UserDomain user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var userExists = await _userRepository.GetUserAsync(user.Id, cancellationToken);

            if (userExists != null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID already exists."));
            }

            var userEntity = user.ToEntity();
            var userCreate = await _userRepository.CreateUserAsync(userEntity, cancellationToken);
            var userDomain = userCreate.ToDomain();

            return Result.Ok(userDomain);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> UpdateUserAsync(UserDomain user, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var userEntiy = user.ToEntity();
            var userUpdate = await _userRepository.UpdateUserAsync(userEntiy, cancellationToken);

            return Result.Ok(userUpdate);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async Task<Result<bool>> DeleteUserAsync(int id, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        try
        {
            var userExists = await _userRepository.GetUserAsync(id, cancellationToken);

            if (userExists == null)
            {
                return Result.Fail(new ValidationError().WithError("Id", "User ID does not exist."));
            }

            var userDelete = await _userRepository.DeleteUserAsync(id, cancellationToken);

            return Result.Ok(userDelete);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
