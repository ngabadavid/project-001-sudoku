using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    public class Cell
    {
        //public List<CellCoordinate> CellCoordinates { get; set; }
        public CellCoordinate CellCoordinate { get; set; }
        public int Value { get; set; }
        /* public Cell(List<CellCoordinate> cellCoordinates, int value)
         {
             this.CellCoordinates = cellCoordinates;
             this.Value = value;
         }*/
        public Cell(CellCoordinate cellCoordinate, int value)
        {
            this.CellCoordinate = cellCoordinate;
            this.Value = value;
        }
        /*public void link(Cell cell)
        {       
            this.CellCoordinates = this.CellCoordinates.Union(cell.CellCoordinates).ToList();
            cell.CellCoordinates = this.CellCoordinates;
        }*/
    }
}
