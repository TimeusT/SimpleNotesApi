using SimpleNotes.Application.Interfaces;
using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Mapping;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Application.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _addressRepository;

    public AddressService(IAddressRepository addressRepository)
    {
        _addressRepository = addressRepository;
    }

    // Get all addresses
    public IEnumerable<AddressDomain> GetAllAddress()
    {
        return _addressRepository.GetAllAddress().Select(x => x.ToDomain());
    }

    // Get address with user id
    public AddressDomain? GetAddress(int id)
    {
        return _addressRepository.GetAddress(id)?.ToDomain();
    }

    // Create Address
    public AddressDomain CreateAddress(AddressDomain address)
    {
        var addressEntity = address.ToEntity();

        var addressCreat = _addressRepository.CreateAddress(addressEntity);

        var addressDomain = addressCreat.ToDomain();

        return addressDomain;
    }

    // Update address
    public bool UpdateAddress(AddressDomain address)
    {
        var addressEntity = address.ToEntity();

        var addressUpdate = _addressRepository.UpdateAddress(addressEntity);

        return addressUpdate;
    }

    // Delete address
    public bool DeleteAddress(int id)
    {
        var addressDelete = _addressRepository.DeleteAddress(id);

        return addressDelete;
    }
}
