using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Infrastructure.Interfaces;

public interface INoteRepository
{
    IEnumerable<NoteItemEntity> List();

    NoteItemEntity? Get(int id);

    NoteItemEntity Create(NoteItemEntity note);

    bool Update(NoteItemEntity note);

    bool Delete(int id);
}
