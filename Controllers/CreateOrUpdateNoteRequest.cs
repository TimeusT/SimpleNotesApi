using System.ComponentModel.DataAnnotations;

namespace SimpleNotesApi.Controllers
{
    public class CreateNoteRequest
    {    
        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Content { get; set; }
    }

    public class UpdateNoteRequest
    {
        public int Id { get; set; }

        [MaxLength(25)]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "The field {0} can only contain letters and spaces.")]
        public string Title { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Content { get; set; }
    }

    public class NoteResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime LastUpdatedAt { get; set; }
    }
}
