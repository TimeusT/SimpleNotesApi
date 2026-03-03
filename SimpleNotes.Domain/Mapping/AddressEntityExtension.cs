using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Domain.Mapping;

public static class AddressEntityExtension
{
    public static AddressDomain ToDomain(this AddressEntity entity)
    {
        return new AddressDomain(
            entity.StreetNo,
            entity.City,
            entity.State,
            entity.PostalCode,
            entity.Country,
            entity.Id
        );
    }
}
