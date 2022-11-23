namespace Sudoku.model
{
    public class Coordinate
    {
        public int PositionX { get; set; }
        public int PositionY { get; set; }
        public Coordinate(int positionX, int positionY)
        {
            this.PositionX = positionX;
            this.PositionY = positionY;
        }
    }
}
