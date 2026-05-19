using FluentResults;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface IUserService
{
    Task <Result<IEnumerable<UserDomain>>> ListUsersAsync();
    
    Task<Result<UserDomain?>> GetUserAsync(int id);
    
    Task<Result<UserDomain?>> GetByEmailAsync(EmailText email);
    
    Task<Result<IEnumerable<NoteDomain>>> GetUserNotesAsync(int id);
    
    Task<Result<UserDomain>> CreateUserAsync(UserDomain user);
    
    Task<Result<bool>> UpdateUserAsync(UserDomain user);
    
    Task<Result<bool>> DeleteUserAsync(int id);
}
