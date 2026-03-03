using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface IAddressService
{
    // Get all existing addresses
    IEnumerable<AddressDomain> GetAllAddress();
    // Get Address by User
    AddressDomain? GetAddress(int id);
    // Create the Address
    AddressDomain CreateAddress(AddressDomain address);
    // Update Address
    bool UpdateAddress(AddressDomain user);
    // Delete Address
    bool DeleteAddress(int id);
}
