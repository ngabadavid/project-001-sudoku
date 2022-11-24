using System;
using System.Collections.Generic;

namespace Sudoku.model
{
    public class Grid
    {
        public Coordinate Origin { get; set; }
        public Coordinate Opposite { get; set; }
        public Cell[,] Cells { get; set; }
        public Grid(Coordinate origin, Coordinate opposite, Cell[,] cells)
        {
            this.Origin = origin;
            this.Opposite = opposite;
            this.Cells = cells;
        }
    }
}
