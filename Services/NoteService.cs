using SimpleNotesApi.Controllers;
using SimpleNotesApi.Repository;
using SimpleNotesApi.Services.Domain;

namespace SimpleNotesApi.Services
{
    public class NoteService : INoteService
    {
        
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        // Need a private variable to keep track of the next ID to assign to a new note
        private int _nextId = 1;

        // Get all notes
        public IEnumerable<NoteDomain> List()
        {
            return _noteRepository.List().Select(x => x.ToDomain());
        }

        // Get by ID
        public NoteDomain? Get(int id)
        {
            return _noteRepository.Get(id)?.ToDomain();
        }

        // Create a new note
        public NoteDomain Create(NoteDomain note)
        {
            //TODO call repo and map
            
        }

        // Update existing note
        public bool Update(NoteDomain note)
        {
            //TODO call repo and map
            
        }

        // Delete existing note
        public bool Delete(int id)
        {
            //TODO call repo and map
        }

    }
}
