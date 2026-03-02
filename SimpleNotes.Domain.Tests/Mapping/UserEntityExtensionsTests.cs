using SimpleNotes.Domain.Entities;
using SimpleNotes.Domain.Mapping;

namespace SimpleNotes.Domain.Tests.Mapping;

public class UserEntityExtensionsTests
{
    [Fact]
    public void Given_ValidUserEntity_When_ToDomain_Then_ReturnToDomain()
    {
        // Assign
        var validUser = new UserEntity
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Age = 30,
            Email = "John@Doe.com",
            JoinDate = DateTime.UtcNow
        };

        // Act
        var validDomain = validUser.ToDomain();

        // Assert
        Assert.NotNull(validDomain);

        Assert.Equal(validUser.Id, validDomain.Id);
        Assert.Equal(validUser.FirstName, validDomain.FirstName);
        Assert.Equal(validUser.LastName, validDomain.LastName);
        Assert.Equal(validUser.Age, validDomain.Age);
        Assert.Equal(validUser.Email, validDomain.Email?.Value);
        Assert.Equal(validUser.JoinDate, validDomain.JoinDate);
    }
}
