using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Domain.Mapping;

public static class UserEntityExtension
{
    public static UserDomain ToDomain(this UserEntity entity)
    {
        return new UserDomain
        (
            entity.FName,
            entity.LName,
            entity.Age,
            entity.JoinDate,
            entity.Id,
            EmailText.Create(entity.Email)
        );
    }
}
