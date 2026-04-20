using Azure;
using Azure.Data.Tables;

namespace SimpleNotes.Domain.Entities;

public class UserEntity
{
    public int Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public int Age { get; set; }

    public string? Email { get; set; }

    public DateTime JoinDate { get; set; }

    public ICollection<NoteItemEntity> Notes { get; set; } = new List<NoteItemEntity>(); // Navigation (points to NoteItemEntity)

    // Link Address one-to-one relation
    public AddressEntity? Address { get; set; }
}

public class UserTableStorageEntity : UserEntity, ITableEntity
{
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }
}

public class UserIdLookupEnity : ITableEntity
{
    public required string PartitionKey { get; set; }

    public required string RowKey { get; set; }

    public DateTimeOffset? Timestamp { get; set; }

    public ETag ETag { get; set; }

    public required string Email { get; set; }
}