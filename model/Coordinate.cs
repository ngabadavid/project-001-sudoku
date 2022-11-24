using System;

namespace Sudoku.model
{
    public class Coordinate
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Coordinate(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public void print()
        {
            Console.WriteLine("("+ Row +","+ Column +")");
        }
    }
}
