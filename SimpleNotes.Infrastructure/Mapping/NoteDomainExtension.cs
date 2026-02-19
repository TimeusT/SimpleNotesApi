using SimpleNotes.Api.Controllers;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Mapping;

public static class NoteDomainExtension
{
    //TODO create NoteDomainExtensions to map to/from NoteItemEntity
    public static NoteResponse ToResponse(this NoteDomain entity)
    {
        return new NoteResponse
        {
            Id = entity.Id,
            Title = entity.Title.Value,
            Content = entity.Content?.Value,
            LastUpdatedAt = entity.LastUpdatedAt
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
            LastUpdatedAt = domain.LastUpdatedAt
        };
    }
}
