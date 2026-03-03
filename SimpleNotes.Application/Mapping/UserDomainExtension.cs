using SimpleNotes.Application.DTOs;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Application.Mapping;

public static class UserDomainExtension
{
    public static UserResponse ToResponse(this UserDomain domain)
    {
        return new UserResponse
        {
            Id = domain.Id,
            FirstName = domain.FirstName,
            LastName = domain.LastName,
            Age = domain.Age,
            Email = domain.Email?.Value,
            Address = domain.ToAddressResponse()
        };
    }

    public static UserAddress? ToAddressResponse(this UserDomain domain)
    {
        if (domain.Address == null) return null;

        return new UserAddress
        {
            StreetNo = domain.Address.StreetNo,
            City = domain.Address.City,
            State = domain.Address.State,
            PostalCode = domain.Address.PostalCode,
            Country = domain.Address.Country
        };
    }

    public static UserEntity ToEntity(this UserDomain domain)
    {
        return new UserEntity
        {
            Id = domain.Id,
            FirstName = domain.FirstName,
            LastName = domain.LastName,
            Age = domain.Age,
            Email = domain.Email?.Value,
            JoinDate = domain.JoinDate,
            Address = domain.ToAddressEntity()
        };
    }

    public static AddressEntity? ToAddressEntity(this UserDomain domain)
    {
        if (domain.Address == null) return null;

        return new AddressEntity
        {
            Id = domain.Address.Id,
            StreetNo = domain.Address.StreetNo,
            City = domain.Address.City,
            State = domain.Address.State,
            PostalCode = domain.Address.PostalCode,
            Country = domain.Address.Country
        };
    }
}
