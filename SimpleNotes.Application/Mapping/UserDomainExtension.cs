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
            Notes = domain.Notes.ToList()
        };
    }
}
