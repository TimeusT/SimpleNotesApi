using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> ListUsersAsync(CancellationToken cancellationToken);

    Task<UserEntity?> GetUserAsync(int id, CancellationToken cancellationToken);

    Task<UserEntity?> GetByEmailAsync(EmailText id, CancellationToken cancellationToken);

    Task<IEnumerable<NoteItemEntity>> GetUserNotesAsync(int id, CancellationToken cancellationToken);

    Task<UserEntity> CreateUserAsync(UserEntity user, CancellationToken cancellationToken);

    Task<bool> UpdateUserAsync(UserEntity user, CancellationToken cancellationToken);

    Task<bool> DeleteUserAsync(int id, CancellationToken cancellationToken);
}