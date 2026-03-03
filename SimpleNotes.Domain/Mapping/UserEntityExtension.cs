using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Domain.Mapping;

public static class UserEntityExtension
{
    public static UserDomain ToDomain(this UserEntity entity)
    {
        return new UserDomain
        (
            entity.FirstName,
            entity.LastName,
            entity.Age,
            entity.JoinDate,
            entity.Id,
            EmailText.Create(entity.Email),
            entity.ToAddressDomain()
        );
    }

    public static AddressDomain? ToAddressDomain(this UserEntity entity)
    {
        // passing as null here !
        if (entity.Address == null) return null;

        return new AddressDomain(
            entity.Address.StreetNo,
            entity.Address.City,
            entity.Address.State,
            entity.Address.PostalCode,
            entity.Address.Country
            );
    }
}
