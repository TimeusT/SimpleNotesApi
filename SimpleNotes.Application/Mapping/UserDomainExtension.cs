using SimpleNotes.Application.DTOs;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Application.Mapping;

public static class UserDomainExtension
{
    public static UserEntity ToEntity(this UserDomain domain)
    {
        return new UserEntity
        {
            Id = domain.Id,
            FName = domain.FName,
            LName = domain.LName,
            Age = domain.Age,
            Email = domain.Email?.Value,
            JoinDate = domain.JoinDate
        };
    }

    public static UserResponse ToResponse(this UserDomain domain)
    {
        return new UserResponse
        {
            Id = domain.Id,
            FName = domain.FName,
            LName = domain.LName,
            Age = domain.Age,
            Email = domain.Email?.Value
        };
    }
}
