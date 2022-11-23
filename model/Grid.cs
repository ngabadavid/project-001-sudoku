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
        public Grid(int tabId, int width, int height, Cell[,] cells, Coordinate coordinate )
        {
            Random random = new Random(DateTime.UtcNow.Millisecond);
            this.GridId = tabId;
            this.Width = width;
            this.Height = height;
            this.Cells = cells;
            this.Coordinate = coordinate;
            for (int positionX = 0; positionX < this.Width; positionX++)
            {
                for (int positionY = 0; positionY < this.Height; positionY++)
                {
                    //List<CellCoordinate> cellCoordinates = new List<CellCoordinate>();
                    //cellCoordinates.Add(new CellCoordinate(new Coordinate(positionX, positionY), this.GridId));
                    this.Cells[positionX, positionY] = new Cell(new CellCoordinate(new Coordinate(positionX, positionY), this.GridId), random.Next(1, this.Width));
                }
            }
        }

        public void print()
        {
            for(int positionX = 0 ; positionX < this.Width; positionX++)
            {
                Console.WriteLine("---------------------------");
                for(int positionY = 0 ; positionY < this.Height; positionY++)
                {
                    Console.Write("|");
                    Console.Write(this.Cells[positionX,positionY].Value);
                    Console.Write("|");
                }
                Console.WriteLine("");
            }
        }

         public Cell getCellByCoordinate(Coordinate coordinate)
        {
            return this.Cells[coordinate.PositionX, coordinate.PositionY];
        }

        public void insert(Coordinate coordinate, int value)
        {
            this.getCellByCoordinate(coordinate).Value = value;
        }

        public Rectangle GetIntersection(Grid tab)
        {
            int orgX = Math.Max(this.Coordinate.PositionX, tab.Coordinate.PositionX);
            int orgY = Math.Max(this.Coordinate.PositionY, tab.Coordinate.PositionY);
            Coordinate origin = new Coordinate(orgX, orgY);

            int oppX = Math.Min(this.Coordinate.PositionX + this.Width - 1, tab.Coordinate.PositionX + tab.Width - 1);
            int oppY = Math.Min(this.Coordinate.PositionY + this.Height - 1, tab.Coordinate.PositionY + tab.Height - 1);
            Coordinate opposite = new Coordinate(oppX, oppY);

            return new Rectangle(origin, opposite);
        }

        public void link(Grid tab)
        {
            Rectangle rectangle = this.GetIntersection(tab);
            for(int i = rectangle.Origin.PositionX; i<= rectangle.Opposite.PositionX; i++)
            {
                for (int j = rectangle.Origin.PositionY; j <= rectangle.Opposite.PositionY; j++)
                {
                    this.Cells[i - this.Coordinate.PositionX, j - this.Coordinate.PositionX].link(tab.Cells[i - tab.Coordinate.PositionX, j - tab.Coordinate.PositionX]);
                }
            }

        }

       
    }
}
