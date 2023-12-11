using Moq;

namespace ShapeCalculationTool.Tests
{
    [Collection("Integration Test")]
    public class CalculatorTest
    {
        // Integration Test
        [Fact]
        public void Main_ShouldRunSuccessfully()
        {
            // Arrange
            var mockShapeMenu = new Mock<IShapeMenu>();
            Calculator.ShapeMenu = mockShapeMenu.Object;

            // Act
            Calculator.Main();

            // Assert
            mockShapeMenu.Verify(m => m.Run(), Times.Once);
        }
    }
}