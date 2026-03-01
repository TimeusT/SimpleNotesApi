namespace SimpleNotes.Domain.Tests
{
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
    }
}
