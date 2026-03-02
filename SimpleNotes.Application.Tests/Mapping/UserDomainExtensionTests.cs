using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;
using SimpleNotes.Domain.Entities;

namespace SimpleNotes.Application.Tests.Mapping;

public class NoteDomainExtensionsTests
{
    [Fact]
    public void Given_ValidUserDomain_When_ToResponse_Then_ReturnUserResponse()
    {
        // Assign
        var validDomain = new UserDomain
        (
            "John",
            "Doe",
            30,
            DateTime.Now,
            1,
            EmailText.Create("John@Doe.com")
        );

        // Act
        var validResponse = validDomain.ToResponse();

        // Assert
        Assert.NotNull(validResponse);

        Assert.Equal(validDomain.FirstName, validResponse.FirstName);
        Assert.Equal(validDomain.LastName, validResponse.LastName);
        Assert.Equal(validDomain.Age, validResponse.Age);
        Assert.Equal(validDomain.Email?.Value, validResponse.Email);
    }

    [Fact]
    public void Given_ValidUserDomain_When_ToEntity_Then_ReturnUserItemEntity()
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

        var validDomain = new UserDomain
        (
            "John",
            "Doe",
            30,
            DateTime.Now,
            1,
            EmailText.Create("John@Doe.com"),
            new List<NoteItemEntity> { validNote }
        );

        // Act
        var validEntity = validDomain.ToEntity();

        // Assert
        Assert.NotNull(validEntity);

        Assert.Equal(validDomain.FirstName, validEntity.FirstName);
        Assert.Equal(validDomain.LastName, validEntity.LastName);
        Assert.Equal(validDomain.Age, validEntity.Age);
        Assert.Equal(validDomain.Email?.Value, validEntity.Email);
    }
}
