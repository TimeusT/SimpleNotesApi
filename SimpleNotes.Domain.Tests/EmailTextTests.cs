namespace SimpleNotes.Domain.Tests;

public class EmailTextTests
{
    [Fact]
    public void Given_ValidValue_When_CreateEmailText_Then_ReturnEmailText()
    {
        // Assign
        var validValue = "test@test.com";

        // Act
        var emailText = EmailText.Create(validValue);

        // Assert
        Assert.Equal(validValue, emailText.Value);
        Assert.False(emailText.IsNull);
    }


    public void Given_InvalidValue_When_CreateEmailText_Then_ThrowArgumentException()
    {
        // Assign
        var invalidValue = "ABC 123 !@#";

        // Act
        var exception = Assert.Throws<ArgumentException>(() =>
            EmailText.Create(invalidValue));

        // Assert
        Assert.Equal("Value must be in valid email format.", exception.Message);
    }
}