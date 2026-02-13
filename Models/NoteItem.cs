using System.ComponentModel.DataAnnotations;

namespace SimpleNotesApi.Models
{
    // Here are all the attributes that we can use for this class
    public class NoteItem
    {
        // ID
        public int Id { get; set; }

        // Title
        [Required]
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z\s]+$",ErrorMessage = "Title can only contain letters and spaces.")]
        public string Title { get; set; } = string.Empty;

        // Content
        [Required]
        [MaxLength(100)]
        public string? Content { get; set; }
        
        // CreatedAt
        public DateTime CreatedAt { get; set; }
        
        // LastUpdatedAt
        public DateTime LastUpdatedAt { get; set; }
    }
}
