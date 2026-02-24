namespace SimpleNotes.Domain.Tests
{
    public class AlphaTextTests
    {
        [Fact]
        public void Given_ValidValue_When_CreateAlphaText_Then_ReturnAlphaText()
        {
            //Assign
            var validValue = "test@test.com";

            //Act
            var alphaText = AlphaText.Create(validValue);

            //Assert
            Assert.Equal(validValue, alphaText.Value);
            Assert.False(alphaText.IsNull);
        }
    }
}
