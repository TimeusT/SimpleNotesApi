namespace SimpleNotes.Domain;

public class NoteDomain
{
    public string UniqueId { get; private set; }
    public AlphaText Title { get; private set; }
    public AlphaText? Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }
    public int UserId { get; private set; }

    public NoteDomain(
        AlphaText title,
        int userId,
        AlphaText? content = default,
        string? uniqueId = default,
        DateTime? createdAt = default,
        DateTime? lastUpdatedAt = default)
    {
        Title = title;
        UserId = userId;
        Content = content;
        UniqueId = uniqueId ?? Guid.NewGuid().ToString();
        CreatedAt = createdAt ?? DateTime.UtcNow;
        LastUpdatedAt = lastUpdatedAt ?? DateTime.UtcNow;
    }

    public void Update(AlphaText title, AlphaText content)
    {
        Title = title;
        Content = content;
        LastUpdatedAt = DateTime.UtcNow;
    }
}
