using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Data;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Infrastructure.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly AppDbContext _context;

    public AddressRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get all addresses
    public IEnumerable<AddressEntity> GetAllAddress()
    {
        return _context.Address.ToList();
    }

    // Get address by user Id
    public AddressEntity? GetAddress(int id)
    {
        return _context.Address.FirstOrDefault(x => x.Id == id); ;
    }

    // Create address
    public AddressEntity CreateAddress(AddressEntity address)
    {
        _context.Address.Add(address);
        _context.SaveChanges();
        return address;
    }

    // Update address
    public bool UpdateAddress(AddressEntity address)
    {
        var existingAddress = _context.Address.Find(address.Id);

        if (existingAddress == null) return false;

        existingAddress.StreetNo = address.StreetNo;
        existingAddress.City = address.City;
        existingAddress.State = address.State;
        existingAddress.PostalCode = address.PostalCode;
        existingAddress.Country = address.Country;

        _context.SaveChanges();

        return true;
    }

    // Delete address
    public bool DeleteAddress(int id)
    {
        var existingAddress = _context.Address.Find(id);

        if (existingAddress == null) return false;

        _context.Address.Remove(existingAddress);
        _context.SaveChanges();

        return true;
    }
}
