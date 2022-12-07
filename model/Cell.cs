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
        public override bool Equals(object obj)
        {
            return obj is Cell cell &&
                   Value == cell.Value;
        }
        public override int GetHashCode()
        {
            return -1937169414 + Value.GetHashCode();
        }
    }
}
