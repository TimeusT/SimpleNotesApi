using Azure;
using Azure.Data.Tables;

namespace SimpleNotes.Domain.Entities;

// Here are all the properties that we can use for this class
public class NoteItemEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    // Required foreign key
    public int UserId { get; set; }

    // Linked user
    public UserEntity User { get; set; } = null!; // Navigation back to UserEntity
}

public class NoteTableStorageEntity : NoteItemEntity, ITableEntity
{
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }

}