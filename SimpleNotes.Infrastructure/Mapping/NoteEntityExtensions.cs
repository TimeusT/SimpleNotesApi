using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Application.DTOs;

namespace SimpleNotes.Infrastructure.Mapping;

public static class NoteEntityExtensions
{
    public static NoteDomain ToDomain(this NoteItemEntity entity)
    {
        return new NoteDomain
        (
            AlphaText.Create(entity.Title),
            AlphaText.Create(entity.Content),
            entity.Id,
            entity.CreatedAt,
            entity.LastUpdatedAt
        );
    }
}
