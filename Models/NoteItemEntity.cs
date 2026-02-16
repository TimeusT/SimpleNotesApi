using System.ComponentModel.DataAnnotations;

namespace SimpleNotesApi.Models
{
    // Here are all the attributes that we can use for this class
    public class NoteItemEntity
    {
        // ID
        public int Id { get; set; }

        // Title      
        public string Title { get; set; } = string.Empty;

        // Content
        public string? Content { get; set; }
        
        // CreatedAt
        public DateTime CreatedAt { get; set; }
        
        // LastUpdatedAt
        public DateTime LastUpdatedAt { get; set; }
    }
    
    public class CreateOrUpdateNoteRequest
    {    
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Content { get; set; }
    }
}
