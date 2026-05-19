using Microsoft.EntityFrameworkCore;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Data;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NoteItemEntity>> ListAsync(CancellationToken cancellationToken)
    {
        return await _context.Notes.ToListAsync(cancellationToken);
    }

    public async Task<NoteItemEntity?> GetAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Notes
            .FirstOrDefaultAsync(x => x.Id == id,
                cancellationToken);
    }

    public async Task<NoteItemEntity> CreateAsync(NoteItemEntity note, CancellationToken cancellationToken)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync(cancellationToken);

        return note;
    }

    public async Task<bool> UpdateAsync(NoteItemEntity note, CancellationToken cancellationToken)
    {
        var existingNote = await _context.Notes.FindAsync(note.Id, cancellationToken);

        if (existingNote == null) return false;

        existingNote.Title = note.Title;
        existingNote.Content = note.Content;
        existingNote.LastUpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var existingNote = await _context.Notes.FindAsync(id, cancellationToken);

        if (existingNote == null)
            return false;

        _context.Notes.Remove(existingNote);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }

}
