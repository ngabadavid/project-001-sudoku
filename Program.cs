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
            // Test insertion : Success
            /*Coordinate origin1 = new Coordinate(0, 0);
            Coordinate opposite1 = new Coordinate(1, 1);
            Cell cell100 = new Cell(0);
            Cell cell101 = new Cell(1);
            Cell cell110 = new Cell(2);
            Cell cell111 = new Cell(3);
            Cell[,] cells1 = { { cell100 , cell101 },
                               { cell110 , cell111 } };
            Grid grid1 = new Grid(origin1, opposite1, cells1);
            grid1.print();

            Coordinate origin2 = new Coordinate(1, 1);
            Coordinate opposite2 = new Coordinate(2, 2);
            Cell cell211 = new Cell(4);
            Cell cell212 = new Cell(5);
            Cell cell221 = new Cell(6);
            Cell cell222 = new Cell(7);
            Cell[,] cells2 = { { cell211 , cell212 },
                               { cell221 , cell222 } };
            Grid grid2 = new Grid(origin2, opposite2, cells2);
            grid2.print();

            List<Grid> grids = new List<Grid>();
            grids.Add(grid1);
            grids.Add(grid2);

            Board board = new Board(grids);
            board.print();

            board.insertValueAtGivenCoordinate(8, new Coordinate(0, 0));
            board.print();
            board.insertValueAtGivenCoordinate(9, new Coordinate(1, 1));
            board.print();
            board.insertValueAtGivenCoordinate(0, new Coordinate(2, 2));
            board.print();*/

            Coordinate origin = new Coordinate(0, 0);
            Coordinate opposite = new Coordinate(3, 3);
            Cell cell00 = new Cell(1);
            Cell cell01 = new Cell(0);
            Cell cell02 = new Cell(3);
            Cell cell03 = new Cell(0);
            Cell cell10 = new Cell(0);
            Cell cell11 = new Cell(0);
            Cell cell12 = new Cell(0);
            Cell cell13 = new Cell(0);
            Cell cell20 = new Cell(2);
            Cell cell21 = new Cell(0);
            Cell cell22 = new Cell(0);
            Cell cell23 = new Cell(0);
            Cell cell30 = new Cell(0);
            Cell cell31 = new Cell(0);
            Cell cell32 = new Cell(0);
            Cell cell33 = new Cell(0);
            Cell[,] cells = { 
                { cell00 , cell01, cell02, cell03 },
                { cell10 , cell11, cell12, cell13 },
                { cell20 , cell21, cell22, cell23 },
                { cell30 , cell31, cell32, cell33 },
            };
            Grid grid = new Grid(origin, opposite, cells);
            grid.print();

            List<Grid> grids = new List<Grid>();
            grids.Add(grid);

            Board board = new Board(grids);
            board.print();

            board.insertValueAtGivenCoordinate(1, new Coordinate(1, 1));
            board.insertValueAtGivenCoordinate(3, new Coordinate(3, 2));
            board.insertValueAtGivenCoordinate(2, new Coordinate(2, 3));
            foreach (Coordinate coordinate in board.MisplacedValuesCoordinates)
            {
                coordinate.print();
            }
        }
    }
}
