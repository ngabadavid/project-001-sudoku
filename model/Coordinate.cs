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

        public override bool Equals(object obj)
        {
            return obj is Coordinate coordinate &&
                   Row == coordinate.Row &&
                   Column == coordinate.Column;
        }

        public void print()
        {
            Console.WriteLine("("+ Row +","+ Column +")");
        }

    }
}
