using SimpleNotes.Application.DTOs;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Application.Mapping;

public static class AddressDomainExtension
{
    public static AddressResponse ToResponse(this AddressDomain domain)
    {
        return new AddressResponse
        {
            StreetNo = domain.StreetNo,
            City = domain.City,
            State = domain.State,
            PostalCode = domain.PostalCode,
            Country = domain.Country,
        };
    }

    public static AddressEntity ToEntity(this AddressDomain request)
    {
        return new AddressEntity
        {
            Id = request.Id,
            StreetNo = request.StreetNo,
            City = request.City,
            State = request.State,
            PostalCode = request.PostalCode,
            Country = request.Country
        };
    }
}
