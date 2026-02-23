using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface IUserRepository
{
    IEnumerable<UserEntity> ListUsers();

    UserEntity? GetUser(int id);

    UserEntity CreateUser(UserEntity user);

    bool UpdateUser(UserEntity user);

    bool DeleteUser(int id);
}
