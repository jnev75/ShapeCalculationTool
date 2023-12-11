namespace ShapeCalculationTool
{
    public interface IShape
    {
        char Shape { get; set; }
        int Length { get; set; }
        double CalcArea();
        double CalcBoundaryLength();
        string ShapeName { get; }
    }
}