using System.Text;

namespace ShapeCalculationTool
{
    public class InvalidInputException : Exception
    {
        public char ActualInvalidCharacter { get; }
        public int ActualInvalidInteger { get; }

        public InvalidInputException(char invalidCharacter, string message) : base(message)
        {
            ActualInvalidCharacter = invalidCharacter;
        }

        public InvalidInputException(int invalidInteger, string message) : base(message)
        {
            ActualInvalidInteger = invalidInteger;
        }
    }

    public class InvalidOperationException : Exception
    {
        public char ActualInvalidCharacter { get; }

        public InvalidOperationException(char invalidCharacter, string message) : base(message)
        {
            ActualInvalidCharacter = invalidCharacter;
        }
    }
    
    public interface IMenuDataValidator
    {
        char GetLetterInput(string menuDialogue, string prompt, string letterPattern);
        int GetNumberInput(string prompt);
        bool IsValidInput(string input, string letterPattern);
        bool IsValidInput(string input, int parsedNumber);
    }

    public interface IShapeMenu
    {
        void Run();
        void DisplayMenuHeader();
        char GetShapeInput();
        int GetLengthInput();
        IShape CreateShape(char shapeType, int length);
        void CalcShapeResults(IShape selectedShape);
        void DisplayResults(string shapeName, int length, double area, double boundaryLength);
        char GetResponseInput();
        string FormatLine(char lineStyle);
    }

    public interface IShapeCreator
    {
        IShape CreateShape(char shapeType, int length);
    }

    public class ShapeCreator : IShapeCreator
    {
        public IShape CreateShape(char shapeType, int length)
        {
            IShape selectedShape = shapeType switch
            {
                's' => new Square { Shape = shapeType, Length = length },
                't' => new Triangle { Shape = shapeType, Length = length },
                'c' => new Circle { Shape = shapeType, Length = length },
                _ => throw new InvalidOperationException(shapeType, "Must enter a valid shape from the menu options [s|t|c]"),
            };

            return selectedShape;
        }
    }

    public class MenuDataValidator : IMenuDataValidator
    {
        public char GetLetterInput(string menuDialogue, string prompt, string letterPattern)
        {
            Console.WriteLine(menuDialogue);
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();

            return IsValidInput(input!, letterPattern) ? input![0] : '\0';
        }

        public int GetNumberInput(string prompt)
        {
            Console.Write(prompt);
            var input = Console.ReadLine()?.Trim();

            return (int.TryParse(input, out var parsedNumber) && IsValidInput(input, parsedNumber)) ? parsedNumber : -1;
        }

        public bool IsValidInput(string input, string letterPattern)
        {
            return !string.IsNullOrEmpty(input) && input.Length == 1 && char.IsLetter(input[0]) && letterPattern.Contains(input[0]);
        }

        public bool IsValidInput(string input, int parsedNumber)
        {
            return !string.IsNullOrEmpty(input) && parsedNumber is > 0 and < 1000;
        }
    }

    public class ShapeMenu : ShapeCreator, IShapeMenu
    {
        private readonly IMenuDataValidator _menuDataValidator;
        private readonly IShapeCreator _shapeCreator;

        public ShapeMenu(IMenuDataValidator menuDataValidator, IShapeCreator shapeCreator)
        {
            _menuDataValidator = menuDataValidator;
            _shapeCreator = shapeCreator;
        }

        public void Run()
        {
            var response = 'n';

            do
            {
                if (response.Equals('y'))
                {
                    Console.WriteLine();
                }

                DisplayMenuHeader();

                var shape = GetShapeInput();
                var length = GetLengthInput();

                var selectedShape = _shapeCreator.CreateShape(shape, length);
                CalcShapeResults(selectedShape);

                response = GetResponseInput();
                
            } while (response.Equals('y'));
        }

        public void DisplayMenuHeader()
        {
            Console.WriteLine(FormatLine('='));
            Console.WriteLine("\t\t\t\t Shape Calculation Tool");
            Console.WriteLine(FormatLine('=') + "\n");
        }

        public char GetShapeInput()
        {
            char shape = 'u';
            do
            {
                try
                {
                    shape = _menuDataValidator.GetLetterInput(
                        "> Shapes:\n\n[ Square: s | Triangle: t | Circle: c ]\n",
                        "Please enter a valid character for shape ('s', 't', or 'c'): ",
                        "stc"
                    );

                    if (shape.Equals('\0'))
                    {
                        throw new InvalidInputException(shape, "Shape must be a valid character (1 lowercase letter)");
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError: Input is invalid.\n{ex.Message}\n");
                    // Re-throw the exception to exit the loop in case of an invalid input
                    // throw; // Comment out or remove this line for normal program execution
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nUnexpected error:\n{ex.Message}\n");
                    // Re-throw the exception to exit the loop in case validation fails
                    // throw; // Comment out or remove this line for normal program execution
                }
            } while (shape.Equals('\0'));

            return shape;
        }

        public int GetLengthInput()
        {
            int length = 0;
            do
            {
                try
                {
                    length = _menuDataValidator.GetNumberInput("Please enter a valid integer for length (between 1 and 999): ");

                    if (length.Equals(-1))
                    {
                        throw new InvalidInputException(length, "Length must be a valid whole number (3 digits maximum)");
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError: Input is invalid.\n{ex.Message}\n");
                    // Re-throw the exception to exit the loop in case of an invalid input
                    // throw; // Comment out or remove this line for normal program execution
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nUnexpected error:\n{ex.Message}\n");
                    // Re-throw the exception to exit the loop in case validation fails
                    // throw; // Comment out or remove this line for normal program execution
                }
            } while (length.Equals(-1));

            return length;
        }

        public void CalcShapeResults(IShape selectedShape)
        {
            var area = selectedShape.CalcArea();
            var boundaryLength = selectedShape.CalcBoundaryLength();

            DisplayResults(selectedShape.ShapeName, selectedShape.Length, area, boundaryLength);
        }

        public void DisplayResults(string shapeName, int length, double area, double boundaryLength)
        {
            Console.WriteLine($"\n{FormatLine('*')}");
            Console.WriteLine($"\nShape Calculation Results:\n");
            Console.WriteLine($"Shape: {shapeName}");
            Console.WriteLine($"Length: {length} cm");
            Console.WriteLine($"Area: {area} cm²");
            Console.WriteLine($"Boundary Length: {boundaryLength} cm\n");
            Console.WriteLine($"{FormatLine('*')}\n");
        }

        public char GetResponseInput()
        {
            char response = 'u';
            do
            {
                try
                {
                    response = _menuDataValidator.GetLetterInput(
                        "> Responses:\n\n[ Yes: y | No: n ]\n",
                        "Would you like to calculate area and boundary length metrics for another shape (y/n)? ",
                        "yn"
                    );

                    if (response.Equals('\0'))
                    {
                        throw new InvalidInputException(response, "Response must be a valid character (1 lowercase letter)");
                    }
                }
                catch (InvalidInputException ex)
                {
                    Console.WriteLine($"\nError: Input is invalid.\n{ex.Message}\n");
                    // Re-throw the exception to exit the loop in case of an invalid input
                    // throw; // Comment out or remove this line for normal program execution
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nUnexpected error:\n{ex.Message}\n");
                    // Re-throw the exception to exit the loop in case validation fails
                    // throw; // Comment out or remove this line for normal program execution
                }

            } while (response.Equals('\0'));

            return response;
        }

        public string FormatLine(char lineStyle)
        {
            StringBuilder sb = new();
            for (var i = 0; i < 88; i++)
            {
                sb.Append(lineStyle);
            }

            return sb.ToString();
        }
    }
}