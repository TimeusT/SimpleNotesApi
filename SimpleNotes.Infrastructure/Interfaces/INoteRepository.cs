using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<NoteItemEntity>> ListAsync();

    Task<NoteItemEntity?> GetAsync(int id);

    Task<NoteItemEntity> CreateAsync(NoteItemEntity note);

    Task<bool> UpdateAsync(NoteItemEntity note);

    Task<bool> DeleteAsync(int id);
}
