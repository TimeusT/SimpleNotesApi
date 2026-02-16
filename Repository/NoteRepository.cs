using SimpleNotesApi.Controllers;
using SimpleNotesApi.Repository.Entities;

namespace SimpleNotesApi.Repository
{
    public class NoteRepository : INoteRepository
    {
            // Need a private list to store the notes in memory
        private readonly List<NoteItemEntity> _notes = new();
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        // Need a private variable to keep track of the next ID to assign to a new note
        private int _nextId = 1;


        // Get all notes
        public IEnumerable<NoteItemEntity> List()
        {
            return _context.Notes.ToList();
        }

        // Getting the ID
        public NoteItemEntity? Get(int id)
        {
            return _context.Notes.Find(id);
        }

        // Create method
        public NoteItemEntity Create(NoteItemEntity note)
        {            
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
}
