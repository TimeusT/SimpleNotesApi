using Azure.Data.Tables;
using Microsoft.Extensions.Options;
using SimpleNotes.Domain.Entities;
using SimpleNotes.Infrastructure.Data;
using SimpleNotes.Infrastructure.Interfaces;

namespace SimpleNotes.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    // Need a private list to store the notes in memory
    private readonly AppDbContext _context;

    public NoteRepository(AppDbContext context)
    {
        _context = context;
    }

    // Get all notes
    public IEnumerable<NoteItemEntity> List()
    {
        return _context.Notes.ToList();
    }

    // Getting the ID
    public NoteItemEntity? Get(int id)
    {
        //_context.Notes.Find(id);
        return _context.Notes.FirstOrDefault(x => x.Id == id);
    }

    // Create method
    public NoteItemEntity Create(NoteItemEntity note)
    {
        // !! If userId doesn't match, return NotFound()
        _context.Notes.Add(note);
        _context.SaveChanges();
        return note;
    }

    // Update note
    public bool Update(NoteItemEntity note)
    {
        var existingNote = _context.Notes.Find(note.Id);

        if (existingNote == null) return false;

        existingNote.Title = note.Title;
        existingNote.Content = note.Content;
        existingNote.LastUpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();

        return true;
    }

    // Delete Note
    public bool Delete(int id)
    {
        var existingNote = _context.Notes.Find(id);

        if (existingNote == null)
            return false;

        _context.Notes.Remove(existingNote);
        _context.SaveChanges();

        return true;
    }

}


public class NoteTableStorageOptions
{
    public required string ConnectionString { get; set; }
    public string NoteTableName { get; set; } = "SimpleNotesNote";
}

public class TableStorageNoteRepository : INoteRepository
{
    private readonly TableClient _tableClient;

    public TableStorageNoteRepository(IOptions<NoteTableStorageOptions> options)
    {
        var serviceClient = new TableServiceClient(options.Value.ConnectionString);
        _tableClient = serviceClient.GetTableClient(options.Value.NoteTableName);
        _tableClient.CreateIfNotExists();
    }

    public IEnumerable<NoteItemEntity> List()
    {
        try
        {
            var notes = _tableClient.Query<NoteTableStorageEntity>();
            var allNotes = new List<NoteTableStorageEntity>();

            foreach (var note in notes)
            {
                allNotes.Add(note);
            }

            return allNotes;
        }
        catch
        {
            throw;
        }
    }

    public NoteItemEntity? Get(int id)
    {
        throw new NotImplementedException();
    }

    public NoteItemEntity Create(NoteItemEntity note)
    {
        throw new NotImplementedException();
    }

    public bool Delete(int id)
    {
        throw new NotImplementedException();
    }

    public bool Update(NoteItemEntity note)
    {
        throw new NotImplementedException();
    }
}