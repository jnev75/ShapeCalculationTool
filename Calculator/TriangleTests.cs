namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class TriangleTests
    {
        // Unit Tests
        [Theory]
        [InlineData(1, 0.43)]
        [InlineData(5, 10.83)]
        [InlineData(999, 432147.11)]
        public void CalcArea_With_ValidLength_ReturnsExpectedArea(int length, double expected)
        {
            // Arrange
            var triangle = new Triangle { Length = length };

            // Act
            var area = triangle.CalcArea();

            // Assert
            Assert.Equal(expected, area, 2); // Using delta for rounding precision
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(5, 15)]
        [InlineData(999, 2997)]
        public void CalcBoundaryLength_WithValidLength_ReturnsExpectedBoundaryLength(int length, double expected)
        {
            // Arrange
            var triangle = new Triangle { Length = length };

            // Act
            var boundaryLength = triangle.CalcBoundaryLength();

            // Assert
            Assert.Equal(expected, boundaryLength);
        }
    }
}