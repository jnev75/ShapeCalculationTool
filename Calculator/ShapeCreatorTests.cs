using Moq;

namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class ShapeCreatorTests
    {
        // Unit Tests
        [Theory]
        [InlineData('s', 5, typeof(Square))]
        [InlineData('t', 3, typeof(Triangle))]
        [InlineData('c', 8, typeof(Circle))]
        public void CreateShape_ValidShapeTypeAndLength_ReturnsCorrectShape(char shapeType, int length, Type expectedType)
        {
            // Arrange
            var validatorMock = new Mock<IMenuDataValidator>();
            var creatorMock = new Mock<IShapeCreator>();
            var shapeMenu = new ShapeMenu(validatorMock.Object, creatorMock.Object);

            // Act
            var shape = shapeMenu.CreateShape(shapeType, length);

            // Assert
            Assert.IsType(expectedType, shape);
            Assert.Equal(shapeType, shape.Shape);
            Assert.Equal(length, shape.Length);
        }

        [Fact]
        public void ShapeCreator_CreateShape_InvalidShapeType_ThrowsException()
        {
            // Arrange
            var shapeCreator = new ShapeCreator();

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => shapeCreator.CreateShape('x', 5));

            // Assert
            Assert.NotNull(exception);
            Assert.Equal('x', exception.ActualInvalidCharacter);
            Assert.Equal("Must enter a valid shape from the menu options [s|t|c]", exception.Message);
        }
    }
}
