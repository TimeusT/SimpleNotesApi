using SimpleNotesApi.Data;
using SimpleNotesApi.Models;

namespace SimpleNotesApi.Services
{
    public class NoteService : INoteService
    {
            // Need a private list to store the notes in memory
        private readonly List<NoteItemEntity> _notes = new();
        private readonly AppDbContext _context;

        public NoteService(AppDbContext context)
        {
            _context = context;
        }

        // Need a private variable to keep track of the next ID to assign to a new note
        private int _nextId = 1;

            // GetAll just returns everything in the list
        /*public IEnumerable<NoteItem> GetAll()
        {
            return _notes;
        }*/
        public IEnumerable<NoteItemEntity> GetAll()
        {
            return _context.Notes.ToList();
        }


        // FirstOrDefault iterates through all items in the list and returns the first one that matches the condition (x.Id == id). If no items match, it returns null.
        /*public NoteItem? GetById(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id);
        }*/

        public NoteItemEntity? GetById(int id)
        {
            return _context.Notes.Find(id);
        }


        // Create method
        /*public NoteItem CreateNote(NoteItem newNote)
        {
            // 1. Assign a new ID
            // 2. Assign the CreatedAt and LastUpdatedAt timestamps
            // 3. Add new not to the list
            // 4. return the new note
            newNote.Id = _nextId;
            _nextId++;

            newNote.CreatedAt = DateTime.UtcNow;
            newNote.LastUpdatedAt = DateTime.UtcNow;

            _notes.Add(newNote);

            return newNote;
        }*/
        public NoteItemEntity CreateNote(CreateOrUpdateNoteRequest noteItem)
        {
            var newNote = new NoteItemEntity
            {
                Title = noteItem.Title,
                Content = noteItem.Content,
                CreatedAt = DateTime.UtcNow,
                LastUpdatedAt = DateTime.UtcNow
            };

            _context.Add(newNote);

            _context.SaveChanges();
            return newNote;
        }

        // Update Note
        /*public bool Update(int id, NoteItem updateItem)
        {
            var existingNote = GetById(id);

            if (existingNote == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(updateItem.Title))
                existingNote.Title = updateItem.Title;

            if (!string.IsNullOrWhiteSpace(updateItem.Content))
                existingNote.Content = updateItem.Content;

            existingNote.LastUpdatedAt = DateTime.UtcNow;
            return true;
        }*/

        public bool Update(int id, CreateOrUpdateNoteRequest updatedNote)
        {
            var existingNote = _context.Notes.Find(id);

            if (existingNote == null)
                return false;

            existingNote.Title = updatedNote.Title;
            existingNote.Content = updatedNote.Content;
            existingNote.LastUpdatedAt = DateTime.UtcNow;

            _context.SaveChanges();

            return true;
        }

        // Delete Note
        /*public bool Delete(int id)
        {
                // Checking whether the note exists
            var existingNote = GetById(id);
            if (existingNote == null)
            {
                return false;
            }

                // Remove the note from the list and return true to indicate successful deletion
            _notes.Remove(existingNote);
            return true;
        }*/

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
}
