using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Domain.Mapping;

public static class NoteEntityExtensions
{
    public static NoteDomain ToDomain(this NoteItemEntity entity)
    {
        return new NoteDomain
        (
            AlphaText.Create(entity.Title),
            entity.UserId,
            AlphaText.Create(entity.Content),
            entity.Id,
            entity.CreatedAt,
            entity.LastUpdatedAt
        );
    }
}
