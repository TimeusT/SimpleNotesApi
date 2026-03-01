using SimpleNotes.Domain.Entities;
using SimpleNotes.Domain.Mapping;

namespace SimpleNotes.Domain.Tests.Mapping;

public class NoteEntityExtensionsTests
{
    [Fact]
    public void Given_ValidNoteItemEntity_When_ToDomain_Then_ReturnNoteDomain()
    {
        // Assign
        var validUser = new UserEntity
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Email = "test@test.com"
        };

        var validEntity = new NoteItemEntity
        {
            Id = 1,
            Title = "Test Note",
            Content = "This is a test note.",
            CreatedAt = DateTime.UtcNow,
            LastUpdatedAt = DateTime.UtcNow,
            UserId = 1,
            User = validUser
        };

        // Act
        var noteDomain = validEntity.ToDomain();
        
        // Assert
        Assert.NotNull(noteDomain);

        Assert.Equal(validEntity.Id, noteDomain.Id);
        Assert.Equal(validEntity.UserId, noteDomain.UserId);
        Assert.Equal(validEntity.Title, noteDomain.Title.Value);
        Assert.Equal(validEntity.Content, noteDomain.Content?.Value);
        Assert.Equal(validEntity.CreatedAt, noteDomain.CreatedAt);
        Assert.Equal(validEntity.LastUpdatedAt, noteDomain.LastUpdatedAt);
    }
}