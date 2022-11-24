using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    public class Cell
    {
        public List<CellCoordinate> CellCoordinates { get; set; }
        public int Value { get; set; }
        public Cell(List<CellCoordinate> cellCoordinates, int value)
        {
            this.CellCoordinates = cellCoordinates;
            this.Value = value;
        }
    }
}
