using SimpleNotes.Domain.Entities;
using SimpleNotes.Domain.Mapping;

namespace SimpleNotes.Domain.Tests.Mapping;

public class NoteEntityExtensionsTests
{
    [Fact]
    public void Given_ValidNoteItemEntity_When_ToDomain_Then_ReturnNoteDomain()
    {
        // Assign
        var validNote = new NoteItemEntity
        {
            Id = 1,
            Title = "Test Note",
            Content = "This is a test note",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            UserId = 1
        };

        // Act
        var noteDomain = validNote.ToDomain();

        // Assert
        Assert.NotNull(noteDomain);

        Assert.Equal(validNote.Id, noteDomain.Id);
        Assert.Equal(validNote.UserId, noteDomain.UserId);
        Assert.Equal(validNote.Title, noteDomain.Title.Value);
        Assert.Equal(validNote.Content, noteDomain.Content?.Value);
        Assert.Equal(validNote.CreatedAt, noteDomain.CreatedAt);
        Assert.Equal(validNote.LastUpdatedAt, noteDomain.LastUpdatedAt);
    }
}