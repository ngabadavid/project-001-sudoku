using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.model
{
    public class Board
    {
        public List<Grid> Grids { get; set; }

        public Board() {
            Grids = new List<Grid>();
        }
        public Board(List<Grid> grids)
        {
            this.Grids = grids;
            //this.link(this.Grids);
        }

        public void insert(CellCoordinate cellCoordinate, int value, List<Cell> visitedCells)
        {
            var cell = this.getCellByCellCoordinate(cellCoordinate);
            cell.Value = value;
            visitedCells.Add(cell);
            foreach(CellCoordinate cellCoordinateElement in cell.CellCoordinates)
            {
                var cellElement = this.getCellByCellCoordinate(cellCoordinateElement);
                if (!visitedCells.Contains(cellElement))
                {
                    insert(cellCoordinateElement, value, visitedCells); 
                }
            }
        }

        public Cell getCellByCellCoordinate(CellCoordinate cellCoordinate)
        {
            return this.Grids[cellCoordinate.GridId].Cells[cellCoordinate.Coordinate.PositionX, cellCoordinate.Coordinate.PositionY]; 
        }

        public void link(List<Grid> grids)
        {
            for (int i = 1; i < grids.Count-1; i++)
            {
                linkTwoGrid(grids[0], grids[i]);
            }
            if (grids.Count > 1)
            {
                List<Grid> a = grids;
                a.RemoveAt(0);
                this.link(a);
            }
        }

        public void linkTwoGrid(Grid grid1, Grid grid2)
        {
            Rectangle rectangle = this.GetIntersection(grid1,grid2);
            for (int i = rectangle.Origin.PositionX; i <= rectangle.Opposite.PositionX; i++)
            {
                for (int j = rectangle.Origin.PositionY; j <= rectangle.Opposite.PositionY; j++)
                {
                    linkTwoCell(grid1.Cells[i - grid1.Coordinate.PositionX, j - grid1.Coordinate.PositionX],grid2.Cells[i - grid2.Coordinate.PositionX, j - grid2.Coordinate.PositionX]);
                }
            }
        }

        public void linkTwoCell(Cell cell1, Cell cell2)
        {
            cell1.CellCoordinates = cell1.CellCoordinates.Union(cell2.CellCoordinates).ToList();
            cell2.CellCoordinates =cell1.CellCoordinates;
        }

        public Rectangle GetIntersection(Grid grid1, Grid grid2)
        {
            int orgX = Math.Max(grid1.Coordinate.PositionX, grid2.Coordinate.PositionX);
            int orgY = Math.Max(grid1.Coordinate.PositionY, grid2.Coordinate.PositionY);
            Coordinate origin = new Coordinate(orgX, orgY);

            int oppX = Math.Min(grid1.Coordinate.PositionX + grid1.Width - 1, grid2.Coordinate.PositionX + grid2.Width - 1);
            int oppY = Math.Min(grid1.Coordinate.PositionY + grid1.Height - 1, grid2.Coordinate.PositionY + grid2.Height - 1);
            Coordinate opposite = new Coordinate(oppX, oppY);

            return new Rectangle(origin, opposite);
        }

        public void print()
        {
            foreach (Grid gridElement in this.Grids)
            {
                gridElement.print();
                Console.WriteLine("=====================================");
            }
        }
    }
}
