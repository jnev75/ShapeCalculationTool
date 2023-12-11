namespace ShapeCalculationTool
{
    public static class Calculator
    {
        private static IShapeMenu? _shapeMenu;

        // Allow injecting IShapeMenu through a property
        public static IShapeMenu ShapeMenu
        {
            get => _shapeMenu ??= new ShapeMenu(new MenuDataValidator(), new ShapeCreator());
            set => _shapeMenu = value;
        }

        public static void Main()
        {
            ShapeMenu.Run();
        }
    }
}