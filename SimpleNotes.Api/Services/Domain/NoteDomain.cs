namespace SimpleNotes.Api.Services.Domain
{
    public class NoteDomain
    {
        public int Id { get; private set; }
        public AlphaText Title { get; private set; }
        public AlphaText? Content { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastUpdatedAt { get; private set; }

        public NoteDomain(
            AlphaText title,
            AlphaText? content = default,
            int? id = default,
            DateTime? createdAt = default,
            DateTime? lastUpdatedAt = default)
        {
            Id = id ?? 0;
            Title = title;
            Content = content;
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
}
