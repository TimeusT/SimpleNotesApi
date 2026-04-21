using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface IUserRepository
{
    IEnumerable<UserEntity> ListUsers();

    UserEntity? GetUser(string uniqueId);

    UserEntity? GetByEmail(EmailText id);

    IEnumerable<NoteItemEntity> GetUserNotes(string uniqueId);

    UserEntity CreateUser(UserEntity user);

    bool UpdateUser(UserEntity user);

    bool DeleteUser(string uniqueId);
}