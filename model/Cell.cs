using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    public class Cell
    {
        public int Value { get; set; }
        public Cell(int value)
        {
            this.Value = value;
        }
    }
}
