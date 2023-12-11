namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class SquareTests
    {
        // Unit Tests
        [Theory]
        [InlineData(1, 1)]
        [InlineData(5, 25)]
        [InlineData(999, 998001)]
        public void CalcArea_With_ValidLength_ReturnsExpectedArea(int length, double expected)
        {
            // Arrange
            var square = new Square { Length = length };

            // Act
            var area = square.CalcArea();

            // Assert
            Assert.Equal(expected, area);
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(5, 20)]
        [InlineData(999, 3996)]
        public void CalcBoundaryLength_With_ValidLength_ReturnsExpectedBoundaryLength(int length, double expected)
        {
            // Arrange
            var square = new Square { Length = length };

            // Act
            var boundaryLength = square.CalcBoundaryLength();

            // Assert
            Assert.Equal(expected, boundaryLength);
        }
    }
}