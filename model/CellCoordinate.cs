namespace Sudoku.model
{
    public class CellCoordinate
    {
        public int GridId { get; set; }
        public Coordinate Coordinate { get; set; }
        public CellCoordinate(int gridId,Coordinate coordinate)
        {
            this.GridId = gridId;
            this.Coordinate = coordinate;
        }
    }
}
