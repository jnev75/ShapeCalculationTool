namespace ShapeCalculationTool
{
    public class Square : IShape
    {
        public char Shape { get; set; } = 's';
        public int Length { get; set; }

        public double CalcArea()
        {
            return Math.Pow(Length, 2);
        }

        public double CalcBoundaryLength()
        {
            return 4 * Length;
        }

        public string ShapeName => "Square";
    }
}