namespace SimpleNotes.Api.Repository.Entities

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
}
