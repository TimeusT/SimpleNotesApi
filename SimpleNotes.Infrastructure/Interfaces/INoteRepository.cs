using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface INoteRepository
{
    Task<IEnumerable<NoteItemEntity>> ListAsync(CancellationToken cancellationToken);

    Task<NoteItemEntity?> GetAsync(int id, CancellationToken cancellationToken);

    Task<NoteItemEntity> CreateAsync(NoteItemEntity note, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(NoteItemEntity note, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}
