using SimpleNotesApi.Repository.Entities;

namespace SimpleNotesApi.Services.Domain
{
    public static class NoteItemEntityExtensions
    {
        public static NoteDomain ToDomain(this NoteItemEntity entity)
        {
            return new NoteDomain
            (                
                AlphaText.Create( entity.Title),
                AlphaText.Create( entity.Content),
                entity.Id,
                entity.CreatedAt,
                entity.LastUpdatedAt
            );
        }
    }
}
