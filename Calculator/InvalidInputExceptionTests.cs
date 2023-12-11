namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class InvalidInputExceptionTests
    {
        // Unit Tests
        [Fact]
        public void InvalidInputException_Should_SetInvalidCharacter()
        {
            const char invalidCharacter = 'a';
            var exception = new InvalidInputException(invalidCharacter, "Invalid character error");
            Assert.Equal(invalidCharacter, exception.ActualInvalidCharacter);
        }

        [Fact]
        public void InvalidInputException_Should_SetInvalidInteger()
        {
            const int invalidInteger = 42;
            var exception = new InvalidInputException(invalidInteger, "Invalid integer error");
            Assert.Equal(invalidInteger, exception.ActualInvalidInteger);
        }

        [Fact]
        public void InvalidInputException_Should_InheritFromException_I()
        {
            const char invalidCharacter = 'a';
            var exception = new InvalidInputException(invalidCharacter, "Invalid character error");
            Assert.IsAssignableFrom<Exception>(exception);
        }

        [Fact]
        public void InvalidInputException_Should_InheritFromException_II()
        {
            const int invalidInteger = 42;
            var exception = new InvalidInputException(invalidInteger, "Invalid integer error");
            Assert.IsAssignableFrom<Exception>(exception);
        }
    }
}