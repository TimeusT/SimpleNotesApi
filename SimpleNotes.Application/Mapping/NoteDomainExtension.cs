using SimpleNotes.Application.DTOs;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Application.Mapping;

public static class NoteDomainExtension
{
    public static NoteResponse ToResponse(this NoteDomain entity)
    {
        return new NoteResponse
        {
            Id = entity.Id,
            Title = entity.Title.Value,
            Content = entity.Content?.Value,
            LastUpdatedAt = entity.LastUpdatedAt,
            UserId = entity.UserId
        };
    }

    public static NoteItemEntity ToEntity(this NoteDomain domain)
    {
        return new NoteItemEntity
        {
            Id = domain.Id,
            Title = domain.Title.Value,
            Content = domain.Content?.Value,
            CreatedAt = domain.CreatedAt,
            LastUpdatedAt = domain.LastUpdatedAt,
            UserId = domain.UserId
        };
    }
}
