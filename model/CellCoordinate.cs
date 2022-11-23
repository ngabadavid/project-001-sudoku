namespace Sudoku.model
{
    public class CellCoordinate
    {
        public int TabId { get; set; }
        public Coordinate Coordinate { get; set; }
        public CellCoordinate(Coordinate coordinate, int tabId)
        {
            this.TabId = tabId;
            this.Coordinate = coordinate;
        }
    }
}
