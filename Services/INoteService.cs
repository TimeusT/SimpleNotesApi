using SimpleNotesApi.Models;

namespace SimpleNotesApi.Services
{
    public interface INoteService
    {
            // Reading notes (GET)
            // Use IEnumerable because you're getting everything from a 'collection'
        IEnumerable<NoteItemEntity> GetAll();
        
            // Getting notes by ID (GET)
            // ? means it might not exist. (It might return something it might return nothing)
            // "?" = Nullable
        NoteItemEntity? GetById(int id);

            // Creating notes (POST)
        NoteItemEntity CreateNote(CreateOrUpdateNoteRequest note);

            // Updating notes (PUT)
            // Needs to take two parameters: id and what to update
        bool Update(int id, CreateOrUpdateNoteRequest updateItem);

            // Deleting notes (DELETE)
            // Needs ID to know which note to delete
        bool Delete(int id);

        /*
         Think of Update and Delete as "actions" that you want to perform on a note.
         They should return an indication of success or failure.
        */
    }
}
