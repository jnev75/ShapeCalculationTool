using Moq;

namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class ShapeMenuTests
    {
        // Unit Tests
        [Fact]
        public void DisplayMenuHeader_WritesExpectedOutputToConsole()
        {
            // Arrange
            using var sw = new StringWriter();
            Console.SetOut(sw);

            // Act
            Mock<IMenuDataValidator> menuDataValidatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            var shapeMenu = new ShapeMenu(menuDataValidatorMock.Object, shapeCreator.Object);
            shapeMenu.DisplayMenuHeader();
            var result = sw.ToString().Trim().Split('\n');

            // Assert
            Assert.Equal(88, result[0].Trim().Length);
            Assert.Contains('=', result[0].Trim());
            Assert.Equal("Shape Calculation Tool", result[1].Trim());
            Assert.Equal(88, result[2].Trim().Length);
            Assert.Contains('=', result[2].Trim());
        }

        [Fact]
        public void Run_ShouldExecuteCorrectly()
        {
            // Arrange
            var menuDataValidatorMock = new Mock<IMenuDataValidator>();
            var shapeCreatorMock = new Mock<IShapeCreator>();
            var shapeMenu = new ShapeMenu(menuDataValidatorMock.Object, shapeCreatorMock.Object);

            // Mock user inputs
            menuDataValidatorMock.SetupSequence(x => x.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns('s')
                .Returns('n');

            menuDataValidatorMock.Setup(x => x.GetNumberInput(It.IsAny<string>())).Returns(5);

            var squareMock = new Mock<IShape>();
            shapeCreatorMock.Setup(x => x.CreateShape('s', 5)).Returns(squareMock.Object);

            // Act
            shapeMenu.Run();

            // Assert
            menuDataValidatorMock.Verify(x => x.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            menuDataValidatorMock.Verify(x => x.GetNumberInput(It.IsAny<string>()), Times.Once);
            shapeCreatorMock.Verify(x => x.CreateShape('s', 5), Times.Once);
            squareMock.Verify(x => x.CalcArea(), Times.Once);
            squareMock.Verify(x => x.CalcBoundaryLength(), Times.Once);
        }

        [Theory]
        [InlineData('s')]
        [InlineData('t')]
        [InlineData('c')]
        public void GetShapeInput_ValidInput_ReturnsValidShape(char userInput)
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();
 
            validatorMock.SetupSequence(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(userInput);
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act
            char shape = shapeMenu.GetShapeInput();

            // Assert
            Assert.True("stc".Contains(shape));
        }

        [Fact]
        public void GetShapeInput_InvalidInput_ExceptionThrown()
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.Setup(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns('\0');
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() => shapeMenu.GetShapeInput());

            Assert.NotNull(exception);
            validatorMock.Verify(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal('\0', exception.ActualInvalidCharacter);
            Assert.Equal($"Shape must be a valid character (1 lowercase letter)", exception.Message);
        }

        [Fact]
        public void GetShapeInput_ExceptionCaught_UnexpectedError()
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.Setup(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception("Unexpected error"));
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => shapeMenu.GetShapeInput());

            Assert.NotNull(exception);
            validatorMock.Verify(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal("Unexpected error", exception.Message);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(999)]
        public void GetLengthInput_ValidInput_ReturnsValidShape(int userInput)
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();
            
            validatorMock.SetupSequence(v => v.GetNumberInput(It.IsAny<string>()))
                .Returns(userInput);
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act
            int length = shapeMenu.GetLengthInput();

            // Assert
            Assert.True(length >= 1 && length <= 999);
        }

        [Fact]
        public void GetLengthInput_InvalidInput_ExceptionThrown()
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.Setup(v => v.GetNumberInput(It.IsAny<string>()))
                .Returns(-1);
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() => shapeMenu.GetLengthInput());

            Assert.NotNull(exception);
            validatorMock.Verify(v => v.GetNumberInput(It.IsAny<string>()), Times.Once);
            Assert.Equal(-1, exception.ActualInvalidInteger);
            Assert.Equal($"Length must be a valid whole number (3 digits maximum)", exception.Message);
        }

        [Fact]
        public void GetLengthInput_ExceptionCaught_UnexpectedError()
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.Setup(v => v.GetNumberInput(It.IsAny<string>()))
                .Throws(new Exception("Unexpected error"));
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => shapeMenu.GetLengthInput());

            Assert.NotNull(exception);
            validatorMock.Verify(v => v.GetNumberInput(It.IsAny<string>()), Times.Once);
            Assert.Equal("Unexpected error", exception.Message);
        }

        [Fact]
        public void CalcShapeResults_ShouldCall_CalcAreaAndCalcBoundaryLength_And_DisplayResults()
        {
            // Arrange
            var shapeMock = new Mock<IShape>();
            shapeMock.Setup(s => s.CalcArea()).Returns(25.0); // Replace with the actual expected area value
            shapeMock.Setup(s => s.CalcBoundaryLength()).Returns(20.0); // Replace with the actual expected boundary length value;

            // Use SetupGet to simulate getting the value of a property
            shapeMock.SetupGet(s => s.ShapeName).Returns("Square");
            shapeMock.SetupGet(s => s.Length).Returns(5);

            var shapeMenu = new ShapeMenu(new Mock<IMenuDataValidator>().Object, new Mock<IShapeCreator>().Object);

            // Act
            shapeMenu.CalcShapeResults(shapeMock.Object);

            // Assert
            shapeMock.Verify(s => s.CalcArea(), Times.Once);
            shapeMock.Verify(s => s.CalcBoundaryLength(), Times.Once);
            Assert.Equal("Square", shapeMock.Object.ShapeName);
            Assert.Equal(5, shapeMock.Object.Length);
            Assert.Equal(25.0, shapeMock.Object.CalcArea());
            Assert.Equal(20.0, shapeMock.Object.CalcBoundaryLength());
        }

        [Fact]
        public void DisplayResults_ShouldDisplayCorrectResultsWithLines()
        {
            // Arrange
            var shapeMenu = new ShapeMenu(new MenuDataValidator(), new ShapeCreator());
            var shapeName = "Square";
            var length = 5;
            var area = 25.0;
            var boundaryLength = 20.0;

            // Act
            var output = CaptureConsoleOutput(() => shapeMenu.DisplayResults(shapeName, length, area, boundaryLength));

            // Assert
            var expectedOutput = $"{new string('*', 88)}{Environment.NewLine}" +
                                 "Shape Calculation Results:" +
                                 $"{Environment.NewLine}{Environment.NewLine}" +
                                 $"Shape: {shapeName}{Environment.NewLine}" +
                                 $"Length: {length} cm{Environment.NewLine}" +
                                 $"Area: {area} cm²{Environment.NewLine}" +
                                 $"Boundary Length: {boundaryLength} cm{Environment.NewLine}" +
                                 $"{new string('*', 88)}{Environment.NewLine}{Environment.NewLine}";

            Assert.Contains($"{new string('*', 88)}", output);
            Assert.Contains("Shape Calculation Results:", output);
            Assert.Contains($"Shape: {shapeName}", output);
            Assert.Contains($"Length: {length} cm", output);
            Assert.Contains($"Area: {area} cm²", output);
            Assert.Contains($"Boundary Length: {boundaryLength} cm", output);
            Assert.Contains($"{new string('*', 88)}", output);
        }

        [Theory]
        [InlineData('y')]
        [InlineData('n')]
        public void GetResponseInput_ValidInput_ReturnsValidResponse(char userInput)
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.SetupSequence(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(userInput);
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act
            char response = shapeMenu.GetResponseInput();

            // Assert
            Assert.True("yn".Contains(response));
        }

        [Fact]
        public void GetResponseInput_InvalidInput_ExceptionThrown()
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.Setup(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns('\0');
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidInputException>(() => shapeMenu.GetResponseInput());

            Assert.NotNull(exception);
            validatorMock.Verify(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal('\0', exception.ActualInvalidCharacter);
            Assert.Equal($"Response must be a valid character (1 lowercase letter)", exception.Message);
        }

        [Fact]
        public void GetResponseInput_ExceptionCaught_UnexpectedError()
        {
            // Arrange
            Mock<IMenuDataValidator> validatorMock = new();
            Mock<IShapeCreator> shapeCreator = new();

            validatorMock.Setup(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception("Unexpected error"));
            var shapeMenu = new ShapeMenu(validatorMock.Object, shapeCreator.Object);

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => shapeMenu.GetResponseInput());

            Assert.NotNull(exception);
            validatorMock.Verify(v => v.GetLetterInput(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.Equal("Unexpected error", exception.Message);
        }

        [Theory]
        [InlineData('=', 88)]
        [InlineData('*', 88)]
        public void FormatLine_ShouldReturnCorrectStringWithSpecificLength(char lineStyle, int expectedLength)
        {
            // Arrange
            var shapeMenu = new ShapeMenu(new MenuDataValidator(), new ShapeCreator());

            // Act
            var result = shapeMenu.FormatLine(lineStyle);

            // Assert
            Assert.Equal(expectedLength, result.Length);
            Assert.Contains(lineStyle, result);
        }

        private static string CaptureConsoleOutput(Action action)
        {
            // Redirect console output to a StringWriter for testing
            using var consoleOutput = new StringWriter();
            Console.SetOut(consoleOutput);

            // Perform the action that writes to the console
            action.Invoke();

            // Return the captured console output
            return consoleOutput.ToString();
        }

        // Custom class to mock IMenuDataValidator for testing unexpected errors
        public class MockMenuDataValidator(bool exceptionOnGetLetterInput, bool exceptionOnGetNumberInput) : IMenuDataValidator
        {
            private readonly bool _exceptionOnGetLetterInput = exceptionOnGetLetterInput;
            private readonly bool _exceptionOnGetNumberInput = exceptionOnGetNumberInput;

            public char GetLetterInput(string menuDialogue, string prompt, string letterPattern)
            {
                if (_exceptionOnGetLetterInput)
                {
                    throw new InvalidInputException('\0', "Unexpected error");
                }

                return 's'; // Return a valid character for successful test cases
            }

            public int GetNumberInput(string prompt)
            {
                if (_exceptionOnGetNumberInput)
                {
                    throw new InvalidInputException(-1, "Unexpected error");
                }

                return 1; // Return a valid integer for successful test cases
            }

            public bool IsValidInput(string input, string letterPattern)
            {
                throw new NotImplementedException();
            }

            public bool IsValidInput(string input, int parsedNumber)
            {
                throw new NotImplementedException();
            }
        }
    }
}