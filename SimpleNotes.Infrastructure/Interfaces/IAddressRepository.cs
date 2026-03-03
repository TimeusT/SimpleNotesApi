using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface IAddressRepository
{
    IEnumerable<AddressEntity> GetAllAddress();

    AddressEntity? GetAddress(int id);

    AddressEntity CreateAddress(AddressEntity user);

    bool UpdateAddress(AddressEntity user);

    bool DeleteAddress(int id);
}
