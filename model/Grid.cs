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

        public void print()
        {
            Console.WriteLine("Grid: =====================================");
            for (int row = 0; row <= Opposite.Row - Origin.Row; row++)
            {
                for (int column = 0; column <= Opposite.Column - Origin.Column; column++)
                {
                    Console.Write("|");
                    Console.Write(this.Cells[row, column].Value);
                    Console.Write("|");
                }
                Console.WriteLine("");
            }
        }
    }
}
