namespace SimpleNotes.Domain;

public class NoteDomain
{
    public string UniqueId { get; private set; }
    public EmailText Email { get; private set; }
    public AlphaText Title { get; private set; }
    public AlphaText? Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastUpdatedAt { get; private set; }

    public NoteDomain(
        AlphaText title,
        EmailText email,
        AlphaText? content = default,
        string? uniqueId = default,
        DateTime? createdAt = default,
        DateTime? lastUpdatedAt = default)
    {
        Title = title;
        Email = email;
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
