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

    public async Task<IEnumerable<NoteItemEntity>> ListAsync()
    {
        return await _context.Notes.ToListAsync();
    }

    public async Task<NoteItemEntity?> GetAsync(int id)
    {
        return await _context.Notes.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<NoteItemEntity> CreateAsync(NoteItemEntity note)
    {
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        return note;
    }

    public async Task<bool> UpdateAsync(NoteItemEntity note)
    {
        var existingNote = await _context.Notes.FindAsync(note.Id);

        if (existingNote == null) return false;

        existingNote.Title = note.Title;
        existingNote.Content = note.Content;
        existingNote.LastUpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingNote = await _context.Notes.FindAsync(id);

        if (existingNote == null)
            return false;

        _context.Notes.Remove(existingNote);
        await _context.SaveChangesAsync();

        return true;
    }

}
