namespace ShapeCalculationTool
{
    public class Triangle : IShape
    {
        public char Shape { get; set; } = 't';
        public int Length { get; set; }

        public double CalcArea()
        {
            return Math.Round(Math.Sqrt(3) / 4 * Math.Pow(Length, 2), 2);
        }

        public double CalcBoundaryLength()
        {
            return 3 * Length;
        }

        public string ShapeName => "Triangle (Equilateral)";
    }
}