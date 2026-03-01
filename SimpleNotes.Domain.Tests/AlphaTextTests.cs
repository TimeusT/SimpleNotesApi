namespace SimpleNotes.Domain.Tests
{
    public class AlphaTextTests
    {
        [Fact]
        public void Given_ValidValue_When_CreateAlphaText_Then_ReturnAlphaText()
        {
            // Assign
            var validValue = "test value"; // Creating a valid email address for testing purposes

            // Act
            var alphaText = AlphaText.Create(validValue); // Creating an instance of AlphaText with the valid value

            // Assert
            Assert.Equal(validValue, alphaText.Value);
            Assert.False(alphaText.IsNull);
        }
     }
}
