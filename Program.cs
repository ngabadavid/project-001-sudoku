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
            Console.WriteLine("=====================================");

            Cell[,] cells1 = new Cell[4,4];
            Coordinate coordinate1 = new Coordinate(0, 0);
            Grid grid1 = new Grid(1,4,4,cells1,coordinate1);
            grid1.print();

            Console.WriteLine("=====================================");

            Cell[,] cells2 = new Cell[4, 4];
            Coordinate coordinate2 = new Coordinate(2, 2);
            Grid grid2 = new Grid(2, 4, 4, cells2, coordinate2);
            grid2.print();

            Console.WriteLine("=====================================");

            List<Grid> grids = new List<Grid>();
            grids.Add(grid1);
            grids.Add(grid2);

            Board board = new Board(grids);
            board.print();

            while (true)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine("Grid ID : ");
                int gridId = int.Parse(Console.ReadLine());

                Console.WriteLine("Position X : ");
                int positionX = int.Parse(Console.ReadLine());

                Console.WriteLine("Position Y : ");
                int positionY = int.Parse(Console.ReadLine());

                Console.WriteLine("Value : ");
                int value = int.Parse(Console.ReadLine());

                board.insert(new CellCoordinate(gridId, new Coordinate(positionX, positionY)), value, new List<Cell>());
                board.print();
            }
            /*
            var cell0[,] =
            {
                0,0,9,1,0,5,4,0,0,
                8,0,0,2,0,4,0,0,9,

            }
            var grid0 = new Grid(0,9,9,);

            var tab1 = new Grid(1);

            tab0.Cells[1, 1].link(tab1.Cells[1,1]);

            var board = new Board(2);
            board.Tabs[0] = tab0;
            board.Tabs[1] = tab1;

            Console.WriteLine(": board.print() =========================================");
            board.print();

            CellCoordinate cellCoordinate0 = new CellCoordinate(new Coordinate(1,1), 0);
            //CellCoordinate cellCoordinate2 = new CellCoordinate(new Coordinate(1,2), 1);


            Console.WriteLine(": board.insert(cellCoordinate1, 0, new List<Cell>()) =========================================");
            board.insert(cellCoordinate0, 0, new List<Cell>());
            //board.insert(cellCoordinate2, 0, new List<Cell>());

            Console.WriteLine(": board.print() =========================================");
            board.print();
            */
        }
    }
}
