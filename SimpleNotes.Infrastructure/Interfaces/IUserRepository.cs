using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<UserEntity>> ListUsersAsync();

    Task<UserEntity?> GetUserAsync(int id);

    Task<UserEntity?> GetByEmailAsync(EmailText id);

    Task<IEnumerable<NoteItemEntity>> GetUserNotesAsync(int id);

    Task<UserEntity> CreateUserAsync(UserEntity user);

    Task<bool> UpdateUserAsync(UserEntity user);

    Task<bool> DeleteUserAsync(int id);
}