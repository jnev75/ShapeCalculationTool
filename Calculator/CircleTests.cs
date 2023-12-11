namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class CircleTests
    {
        // Unit Tests
        [Theory]
        [InlineData(1, 0.79)]
        [InlineData(5, 19.63)]
        [InlineData(999, 783828.15)]
        public void CalcArea_With_ValidLength_ReturnsExpectedArea(int length, double expectedArea)
        {
            // Arrange
            var circle = new Circle { Length = length };

            // Act
            var actualArea = circle.CalcArea();

            // Assert
            Assert.Equal(expectedArea, actualArea, 2);  // Allowing for some rounding differences
        }

        [Theory]
        [InlineData(1, 3.14)]
        [InlineData(5, 15.71)]
        [InlineData(999, 3138.45)]
        public void CalcBoundaryLength_With_ValidLength_ReturnsExpectedLength(int length, double expectedLength)
        {
            // Arrange
            var circle = new Circle { Length = length };

            // Act
            var actualLength = circle.CalcBoundaryLength();

            // Assert
            Assert.Equal(expectedLength, actualLength, 2);  // Allowing for some rounding differences
        }
    }
}