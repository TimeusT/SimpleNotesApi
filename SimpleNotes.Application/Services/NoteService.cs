using SimpleNotes.Api.Controllers;
using SimpleNotes.Api.Repository;
using SimpleNotes.Api.Services.Domain;

namespace SimpleNotes.Application.Services;
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
            // convert domain to entity
            var noteEntity = note.ToEntity();

            // call create method in repo
            var noteCreate = _noteRepository.Create(noteEntity);

            // convert entity to domain
            var noteDomain = noteCreate.ToDomain();

            // returns domain
            return noteDomain;
        }

        // Update existing note
        public bool Update(NoteDomain note)
        {
            //TODO call repo and map
            // convert domain to entity
            var noteEntity = note.ToEntity();

            // call update method
            var noteUpdate = _noteRepository.Update(noteEntity);

            return noteUpdate;
        }

        // Delete existing note
        public bool Delete(int id)
        {
            //TODO call repo and map
            // call delete on repo
            var noteDelete = _noteRepository.Delete(id);

            return noteDelete;
        }
    }
}
