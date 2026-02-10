using SimpleNotesApi.Models;
using System.Collections.Immutable;
using System.Data;

namespace SimpleNotesApi.Services
{
    public class NoteService : INoteService
    {
            // Need a private list to store the notes in memory
        private readonly List<NoteItem> _notes = new();
            // Need a private variable to keep track of the next ID to assign to a new note
        private int _nextId = 1;

            // GetAll just returns everything in the list
        public IEnumerable<NoteItem> GetAll()
        {
            return _notes;
        }

            // FirstOrDefault iterates through all items in the list and returns the first one that matches the condition (x.Id == id). If no items match, it returns null.
        public NoteItem? GetById(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id);
        }

            // Create method
        public NoteItem CreateNote(NoteItem newNote)
        {
            // 1. Assign a new ID
            // 2. Assign the CreatedAt and LastUpdatedAt timestamps
            // 3. Add new not to the list
            // 4. return the new note
            _nextId++;

            newNote.CreatedAt = DateTime.UtcNow;
            newNote.LastUpdatedAt = DateTime.UtcNow;

            _notes.Add(newNote);

            return newNote;
        }

            // Update Note
        public bool Update(int id, NoteItem updateItem)
        {
                // Iterate through IDs list and assign the matching one into existingNote variable
            var existingNote = GetById(id);

               // If no note with the given ID is found, return false
            if (existingNote == null)
            {
                return false;
            }

                // IF the Title property is NOT null or whitespace, update to new Title
            if (!string.IsNullOrWhiteSpace(updateItem.Title))
                existingNote.Title = updateItem.Title;

                // IF the Content property is NOT null or whitespace, update to new Content
            if (!string.IsNullOrWhiteSpace(updateItem.Content))
                existingNote.Content = updateItem.Content;

                // Update the LastUpdatedAt timestamp to the current time
            existingNote.LastUpdatedAt = DateTime.UtcNow;
            return true;
        }

            // Delete Note
        public bool Delete(int id)
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
        }
    }
}
