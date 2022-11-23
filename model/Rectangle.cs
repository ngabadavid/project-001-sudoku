namespace Sudoku.model
{
    public class Rectangle
    {
        public Coordinate Origin { get; set; }
        public Coordinate Opposite { get; set; }
        public Rectangle(Coordinate origin, Coordinate opposite)
        {
            this.Origin = origin;
            this.Opposite = opposite;
        }
    }
}
