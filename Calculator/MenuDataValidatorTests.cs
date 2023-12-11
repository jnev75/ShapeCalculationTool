namespace ShapeCalculationTool.Tests
{
    [Collection("Unit Tests")]
    public class MenuDataValidatorTests
    {
        // Unit Tests
        private readonly StringWriter? consoleOutput = new();
        private StringReader? consoleInput;
        private readonly MenuDataValidator menuDataValidator = new();

        public MenuDataValidatorTests()
        {
            Console.SetOut(consoleOutput);
        }

        [Theory]
        [InlineData("s\n")]
        [InlineData("t\n")]
        [InlineData("c\n")]
        [InlineData("s \n")]
        [InlineData(" t\n")]
        [InlineData(" c \n")]
        public void TestGetLetterInput_With_Valid_CharacterData(string expected)
        {
            // Arrange
            string menuDialogue = "> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]\n";
            string prompt = "Please enter a valid character for shape ('s', 't', or 'c'): ";
            string letterPattern = "stc";

            // Simulate user input
            string userInput = expected;
            consoleInput?.Dispose(); // Dispose the previous StringReader
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal(expected.Trim()[0], result);
            Assert.Contains("> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]", consoleOutput?.ToString());
            Assert.Contains("Please enter a valid character for shape ('s', 't', or 'c'):", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("\ns\n")]
        [InlineData(" \ns\n")]
        [InlineData("r\ns\n")]
        [InlineData("st\ns\n")]
        [InlineData("S\ns\n")]
        [InlineData("$\ns\n")]
        [InlineData("0\ns\n")]
        [InlineData("1000\ns\n")]
        [InlineData("-1\ns\n")]
        public void TestGetLetterInput_With_Invalid_Then_Valid_CharacterData(string userInput)
        {
            // Arrange
            string menuDialogue = "> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]\n";
            string prompt = "Please enter a valid character for shape ('s', 't', or 'c'): ";
            string letterPattern = "stc";

            // Simulate user input: first invalid, then valid
            consoleInput?.Dispose();
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal('\0', result);
            Console.WriteLine("Console Output:");
            Console.WriteLine(consoleOutput?.ToString());

            // Act
            result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal('s', result);
            Assert.Contains("> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]", consoleOutput?.ToString());
            Assert.Contains("Please enter a valid character for shape ('s', 't', or 'c'):", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("\n", '\0')]
        [InlineData(" \n", '\0')]
        [InlineData("r\n", '\0')]
        [InlineData("st\n", '\0')]
        [InlineData("S\n", '\0')]
        [InlineData("$\n", '\0')]
        [InlineData("0\n", '\0')]
        [InlineData("1000\n", '\0')]
        [InlineData("-1\n", '\0')]
        public void TestGetLetterInput_With_Invalid_CharacterData(string userInput, char expected)
        {
            // Arrange
            string menuDialogue = "> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]\n";
            string prompt = "Please enter a valid character for shape ('s', 't', or 'c'): ";
            string letterPattern = "stc";

            // Simulate user input
            consoleInput?.Dispose(); // Dispose the previous StringReader
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal(expected, result);
            Assert.Contains("> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]\n", consoleOutput?.ToString());
            Assert.Contains("Please enter a valid character for shape ('s', 't', or 'c'): ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("1\n", 1)]
        [InlineData("5\n", 5)]
        [InlineData("999\n", 999)]
        public void TestGetNumberInput_With_Valid_IntegerData(string userInput, int expected)
        {
            // Arrange
            string prompt = "Please enter a valid integer for length (between 1 and 999): ";

            // Simulate user input
            consoleInput?.Dispose(); // Dispose the previous StringReader
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetNumberInput(prompt);

            // Assert
            Assert.Equal(expected, result);
            Assert.Contains("Please enter a valid integer for length (between 1 and 999): ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("\n5\n")]
        [InlineData(" \n5\n")]
        [InlineData("s\n5\n")]
        [InlineData("S\n5\n")]
        [InlineData("$\n5\n")]
        [InlineData("0\n5\n")]
        [InlineData("1000\n5\n")]
        [InlineData("-1\n5\n")]
        public void TestGetNumberInput_With_Invalid_Then_Valid_IntegerData(string userInput)
        {
            // Arrange
            string prompt = "Please enter a valid integer for length (between 1 and 999): ";

            // Simulate user input: first invalid, then valid
            consoleInput?.Dispose();
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetNumberInput(prompt);

            // Assert
            Assert.Equal(-1, result);
            Console.WriteLine("Console Output:");
            Console.WriteLine(consoleOutput?.ToString());

            // Act
            result = menuDataValidator.GetNumberInput(prompt);

            // Assert
            Assert.Equal(5, result);
            Assert.Contains("Please enter a valid integer for length (between 1 and 999): ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("\n", -1)]
        [InlineData(" \n", -1)]
        [InlineData("s\n", -1)]
        [InlineData("S\n", -1)]
        [InlineData("$\n", -1)]
        [InlineData("0\n", -1)]
        [InlineData("1000\n", -1)]
        [InlineData("-1\n", -1)]
        public void TestGetNumberInput_With_Invalid_IntegerData(string userInput, int expected)
        {
            // Arrange
            string prompt = "Please enter a valid integer for length (between 1 and 999): ";

            // Simulate user input
            consoleInput?.Dispose(); // Dispose the previous StringReader
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetNumberInput(prompt);

            // Assert
            Assert.Equal(expected, result);
            Assert.Contains("Please enter a valid integer for length (between 1 and 999): ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("y\n")]
        [InlineData("n\n")]
        [InlineData("y \n")]
        [InlineData(" n\n")]
        [InlineData(" y \n")]
        public void TestGetResponseInput_With_Valid_CharacterData(string expected)
        {
            // Arrange
            string menuDialogue = "> Responses:\n\n[ Yes: y | No: n ]\n";
            string prompt = "Would you like to calculate area and boundary length metrics for another shape (y/n)? ";
            string letterPattern = "yn";

            // Simulate user input
            string userInput = expected;
            consoleInput?.Dispose(); // Dispose the previous StringReader
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal(expected.Trim()[0], result);
            Assert.Contains("> Responses:\n\n[ Yes: y | No: n ]\n", consoleOutput?.ToString());
            Assert.Contains("Would you like to calculate area and boundary length metrics for another shape (y/n)? ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("\ny\n")]
        [InlineData(" \ny\n")]
        [InlineData("r\ny\n")]
        [InlineData("st\ny\n")]
        [InlineData("Y\ny\n")]
        [InlineData("$\ny\n")]
        [InlineData("0\ny\n")]
        [InlineData("1000\ny\n")]
        [InlineData("-1\ny\n")]
        public void TestGetResponseInput_With_Invalid_Then_Valid_CharacterData(string userInput)
        {
            // Arrange
            string menuDialogue = "> Responses:\n\n[ Yes: y | No: n ]\n";
            string prompt = "Would you like to calculate area and boundary length metrics for another shape (y/n)? ";
            string letterPattern = "yn";

            // Simulate user input: first invalid, then valid
            consoleInput?.Dispose();
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal('\0', result);
            Console.WriteLine("Console Output:");
            Console.WriteLine(consoleOutput?.ToString());

            // Act
            result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal('y', result);
            Assert.Contains("> Responses:\n\n[ Yes: y | No: n ]\n", consoleOutput?.ToString());
            Assert.Contains("Would you like to calculate area and boundary length metrics for another shape (y/n)? ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("\n", '\0')]
        [InlineData(" \n", '\0')]
        [InlineData("r\n", '\0')]
        [InlineData("st\n", '\0')]
        [InlineData("Y\n", '\0')]
        [InlineData("$\n", '\0')]
        [InlineData("0\n", '\0')]
        [InlineData("1000\n", '\0')]
        [InlineData("-1\n", '\0')]
        public void TestGetResponseInput_With_Invalid_CharacterData(string userInput, char expected)
        {
            // Arrange
            string menuDialogue = "> Responses:\n\n[ Yes: y | No: n ]\n";
            string prompt = "Would you like to calculate area and boundary length metrics for another shape (y/n)? ";
            string letterPattern = "yn";

            // Simulate user input
            consoleInput?.Dispose(); // Dispose the previous StringReader
            consoleInput = new StringReader(userInput);
            Console.SetIn(consoleInput);

            // Act
            var result = menuDataValidator.GetLetterInput(menuDialogue, prompt, letterPattern);

            // Assert
            Assert.Equal(expected, result);
            Assert.Contains("> Responses:\n\n[ Yes: y | No: n ]\n", consoleOutput?.ToString());
            Assert.Contains("Would you like to calculate area and boundary length metrics for another shape (y/n)? ", consoleOutput?.ToString());
        }

        [Theory]
        [InlineData("s", "stc", true)]
        [InlineData("t", "stc", true)]
        [InlineData("c", "stc", true)]
        [InlineData("s ", "stc", true)]
        [InlineData(" t", "stc", true)]
        [InlineData(" c ", "stc", true)]
        [InlineData("y", "yn", true)]
        [InlineData("n", "yn", true)]
        [InlineData("y ", "yn", true)]
        [InlineData(" n", "yn", true)]
        [InlineData(" y ", "yn", true)]
        public void IsValidInput_Valid_CharacterData_ShouldReturnTrue(string userInput, string letterPattern, bool expected)
        {
            // Arrange & Act
            var result = menuDataValidator.IsValidInput(userInput.Trim(), letterPattern);

            // Assert
            Assert.Equal(expected, result);
            Assert.True(result);
        }

        [Theory]
        [InlineData("", "stc", false)]
        [InlineData(" ", "stc", false)]
        [InlineData("r", "stc", false)]
        [InlineData("st", "stc", false)]
        [InlineData("S", "stc", false)]
        [InlineData("$", "stc", false)]
        [InlineData("0", "stc", false)]
        [InlineData("1000", "stc", false)]
        [InlineData("-1", "stc", false)]
        [InlineData("", "yn", false)]
        [InlineData(" ", "yn", false)]
        [InlineData("r", "yn", false)]
        [InlineData("st", "yn", false)]
        [InlineData("S", "yn", false)]
        [InlineData("$", "yn", false)]
        [InlineData("0", "yn", false)]
        [InlineData("1000", "yn", false)]
        [InlineData("-1", "yn", false)]
        public void IsValidInput_Invalid_CharacterData_ShouldReturnFalse(string userInput, string letterPattern, bool expected)
        {
            // Arrange & Act
            var result = menuDataValidator.IsValidInput(userInput, letterPattern);

            // Assert
            Assert.Equal(expected, result);
            Assert.False(result);
        }

        [Theory]
        [InlineData("1", 1, true)]
        [InlineData("5", 5, true)]
        [InlineData("999", 999, true)]
        public void IsValidInput_Valid_IntegerInput_ShouldReturnTrue(string input, int parsedNumber, bool expectedResult)
        {
            // Arrange & Act
            var result = menuDataValidator.IsValidInput(input, parsedNumber);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.True(result);
        }

        [Theory]
        [InlineData("", 0, false)]
        [InlineData(" ", 0, false)]
        [InlineData("0", 0, false)]
        [InlineData("1000", 1000, false)]
        [InlineData("-1", -1, false)]
        [InlineData("abc", 0, false)]
        public void IsValidInput_Invalid_IntegerInput_ShouldReturnFalse(string input, int parsedNumber, bool expectedResult)
        {
            // Arrange & Act
            var result = menuDataValidator.IsValidInput(input, parsedNumber);

            // Assert
            Assert.Equal(expectedResult, result);
            Assert.False(result);
        }
    }
}