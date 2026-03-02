using SimpleNotes.Application.Tests;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Interfaces;

public interface IAddressService
{
    // Get Address by User
    UserDomain? GetAddress(int id);
    // Create the Address
    AddressDomain Create(AddressDomain address);
    // Update Address
    bool Update(UserDomain user);
    // Delete Address
    bool Delete(UserDomain id);
}
