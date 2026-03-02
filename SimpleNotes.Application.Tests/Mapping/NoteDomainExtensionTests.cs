using SimpleNotes.Application.Mapping;
using SimpleNotes.Domain;

namespace SimpleNotes.Application.Tests.Mapping;

public class NoteDomainExtensionTests
{
    [Fact]
    public void Given_ValidNoteDomain_When_ToResponse_Then_ReturnNoteResponse()
    {
        // Assign
        var validDomain = new NoteDomain
        (
            AlphaText.Create("Test"),
            1,
            AlphaText.Create("Testing"),
            1
        );

        // Act
        var validResponse = validDomain.ToResponse();

        // Assert
        Assert.NotNull(validResponse);

        Assert.Equal(validDomain.Title.Value, validResponse.Title);
        Assert.Equal(validDomain.UserId, validResponse.UserId);
        Assert.Equal(validDomain.Content?.Value, validResponse.Content);
        Assert.Equal(validDomain.Id, validResponse.Id);
    }

    [Fact]
    public void Given_ValidNoteDomain_When_ToEntity_Then_ReturnNoteItemEntity()
    {
        // Assign
        var validDomain = new NoteDomain
        (
            AlphaText.Create("Test"),
            1,
            AlphaText.Create("Testing"),
            1
        );

        // Act
        var validEntity = validDomain.ToEntity();

        // Assert
        Assert.NotNull(validEntity);

        Assert.Equal(validDomain.Title.Value, validEntity.Title);
        Assert.Equal(validDomain.UserId, validEntity.UserId);
        Assert.Equal(validDomain.Content?.Value, validEntity.Content);
        Assert.Equal(validDomain.Id, validEntity.Id);
    }
}
