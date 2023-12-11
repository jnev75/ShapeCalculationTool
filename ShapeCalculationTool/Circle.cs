namespace ShapeCalculationTool
{
    public class Circle : IShape
    {
        public char Shape { get; set; } = 'c';
        public int Length { get; set; }

        public double CalcArea()
        {
            var radius = Length / 2.0;
            return Math.Round(Math.PI * Math.Pow(radius, 2), 2);
        }

        public double CalcBoundaryLength()
        {
            var radius = Length / 2.0;
            return Math.Round(2 * Math.PI * radius, 2);
        }

        public string ShapeName => "Circle";
    }
}