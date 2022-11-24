using System;
using System.Collections.Generic;

namespace Sudoku.model
{
    public class Grid
    {
        public int GridId { get; set; }
        public Coordinate Coordinate { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Cell[,] Cells { get; set; }
        public Grid(int gridId, int width, int height, Cell[,] cells, Coordinate coordinate )
        {
            Random random = new Random(DateTime.UtcNow.Millisecond);
            this.GridId = gridId;
            this.Width = width;
            this.Height = height;
            this.Cells = cells;
            this.Coordinate = coordinate;

            //For coding purpose
            for (int positionX = 0; positionX < this.Width; positionX++)
            {
                for (int positionY = 0; positionY < this.Height; positionY++)
                {
                    List<CellCoordinate> cellCoordinates = new List<CellCoordinate>();
                    cellCoordinates.Add(new CellCoordinate(this.GridId, new Coordinate(positionX, positionY)));
                    this.Cells[positionX, positionY] = new Cell(cellCoordinates, random.Next(1, this.Width));
                }
            }
        }

        /*public void link(Grid grid)
        {
            Rectangle rectangle = this.GetIntersection(grid);
            for (int i = rectangle.Origin.PositionX; i <= rectangle.Opposite.PositionX; i++)
            {
                for (int j = rectangle.Origin.PositionY; j <= rectangle.Opposite.PositionY; j++)
                {
                    this.Cells[i - this.Coordinate.PositionX, j - this.Coordinate.PositionX].link(grid.Cells[i - grid.Coordinate.PositionX, j - grid.Coordinate.PositionX]);
                }
            }

        }

        public Rectangle GetIntersection(Grid grid)
        {
            int orgX = Math.Max(this.Coordinate.PositionX, grid.Coordinate.PositionX);
            int orgY = Math.Max(this.Coordinate.PositionY, grid.Coordinate.PositionY);
            Coordinate origin = new Coordinate(orgX, orgY);

            int oppX = Math.Min(this.Coordinate.PositionX + this.Width - 1, grid.Coordinate.PositionX + grid.Width - 1);
            int oppY = Math.Min(this.Coordinate.PositionY + this.Height - 1, grid.Coordinate.PositionY + grid.Height - 1);
            Coordinate opposite = new Coordinate(oppX, oppY);

            return new Rectangle(origin, opposite);
        }*/

        public void print()
        {
            for (int positionX = 0; positionX < this.Width; positionX++)
            {
                for (int positionY = 0; positionY < this.Height; positionY++)
                {
                    Console.Write("|");
                    Console.Write(this.Cells[positionX, positionY].Value);
                    Console.Write("|");
                }
                Console.WriteLine("");
            }
        }

    }
}
