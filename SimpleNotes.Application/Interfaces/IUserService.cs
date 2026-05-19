using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface IUserService
{
    Task <Result<IEnumerable<UserDomain>>> ListUsersAsync(CancellationToken cancellationToken);
    
    Task<Result<UserDomain?>> GetUserAsync(int id, CancellationToken cancellationToken);
    
    Task<Result<UserDomain?>> GetByEmailAsync(EmailText email, CancellationToken cancellationToken);
    
    Task<Result<IEnumerable<NoteDomain>>> GetUserNotesAsync(int id, CancellationToken cancellationToken);
    
    Task<Result<UserDomain>> CreateUserAsync(UserDomain user, CancellationToken cancellationToken);
    
    Task<Result<bool>> UpdateUserAsync(UserDomain user, CancellationToken cancellationToken);
    
    Task<Result<bool>> DeleteUserAsync(int id, CancellationToken cancellationToken);
}
