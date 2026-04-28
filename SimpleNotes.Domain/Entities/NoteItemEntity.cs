using Azure;
using Azure.Data.Tables;

namespace SimpleNotes.Domain.Entities;

// Here are all the properties that we can use for this class
public class NoteItemEntity
{
    public int Id { get; set; }

    public string UniqueId { get; set; } = Guid.NewGuid().ToString();

    public string Email { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime LastUpdatedAt { get; set; }

    // Required foreign key
    public int UserId { get; set; } // is NOT the UniqueId

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

public static class TableNoteEntityExtension
{
    public static TableEntity ToTableEntity(this NoteItemEntity entity)
    {
        return new TableEntity
        {
            PartitionKey = entity.Email,
            RowKey = entity.UniqueId,
            ["Title"] = entity.Title,
            ["Content"] = entity.Content
        };
    }
}