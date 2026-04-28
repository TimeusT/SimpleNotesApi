using Azure;
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
    public NoteItemEntity? Get(string uniqueId)
    {
        //_context.Notes.Find(id);
        return _context.Notes.FirstOrDefault(x => x.UniqueId == uniqueId);
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
        var existingNote = _context.Notes.Find(note.UniqueId);

        if (existingNote == null) return false;

        existingNote.Title = note.Title;
        existingNote.Content = note.Content;
        existingNote.LastUpdatedAt = DateTime.UtcNow;

        _context.SaveChanges();

        return true;
    }

    // Delete Note
    public bool Delete(string uniqueId)
    {
        var existingNote = _context.Notes.Find(uniqueId);

        if (existingNote == null) return false;

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
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            throw;
        }
    }

    public NoteItemEntity? Get(string uniqueId)
    {
        try
        {
            // Get the note with uniqueId
            var note = _tableClient
                .Query<NoteTableStorageEntity>(n => n.RowKey == uniqueId)
                .FirstOrDefault();

            return note;
        }
        catch (RequestFailedException ex) when (ex.Status == 404)
        {
            throw;
        }
    }

    public NoteItemEntity Create(NoteItemEntity note)
    {
        // Create entity
        var newNote = new TableEntity
        {
            PartitionKey = note.Email,
            RowKey = note.UniqueId
        };

        // Assign properties
        newNote["UniqueId"] = note.UniqueId;
        newNote["Email"] = note.Email;
        newNote["Title"] = note.Title;
        newNote["Content"] = note.Content;
        newNote["CreatedAt"] = note.CreatedAt;
        newNote["LastUpdatedAt"] = note.LastUpdatedAt;

        // Transaction
        var transaction = new List<TableTransactionAction>
            {
                new TableTransactionAction(TableTransactionActionType.Add, newNote),
            };

        _tableClient.SubmitTransaction(transaction);

        // return note
        return note;
    }

    public bool Update(NoteItemEntity note)
    {
        try
        {
            //// Find the user
            //var getNote = _tableClient.GetEntity<NoteTableStorageEntity>(
            //    partitionKey: note.Email,
            //    rowKey: note.UniqueId
            //);

            //// Convert to TableEntity to ignore UserId and User
            //var tableNote = getNote.ToTableEntity();

            //// Save changes
            //_tableClient.UpdateEntity(getNote, ETag.All, TableUpdateMode.Merge);

            //return true;
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool Delete(string uniqueId)
    {
        // Get user by id

        // Check existence        

        // Get entity

        // Delete

        //return true

        throw new NotImplementedException();
    }
}