using Sudoku.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku
{
    class Program
    {
        static void Main(string[] args)
        {
            Coordinate origin1 = new Coordinate(0, 0);
            Coordinate opposite1 = new Coordinate(3, 3);
            Cell[,] cells1 = new Cell[4,4];
            Grid grid1 = new Grid(origin1, opposite1, cells1);

            Coordinate origin2 = new Coordinate(2, 2);
            Coordinate opposite2 = new Coordinate(5, 5);
            Cell[,] cells2 = new Cell[4, 4];
            Grid grid2 = new Grid(origin2, opposite2, cells2);

            List<Grid> grids = new List<Grid>();
            grids.Add(grid1);
            grids.Add(grid2);

            Board board = new Board(grids);
        }
    }
}
