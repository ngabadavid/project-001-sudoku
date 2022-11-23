using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sudoku.model
{
    public class Board
    {
        public Grid[] Tabs { get; set; }

        public Board(int tabNumber) {
            Tabs = new Grid[tabNumber];
        }
        public Board(Grid[] tabs)
        {
            this.Tabs = tabs;
        }

        public void insert(CellCoordinate cellCoordinate, int value, List<Cell> visitedCells)
        {
            Coordinate coordinate = new Coordinate(cellCoordinate.Coordinate.PositionX, cellCoordinate.Coordinate.PositionY);
            
            var tab = this.Tabs[cellCoordinate.TabId];
            tab.insert(coordinate, value);

            var cell = tab.getCellByCoordinate(coordinate);
            visitedCells.Add(cell);

            foreach(CellCoordinate cellCoordinateElement in cell.CellCoordinates)
            {
                Coordinate coordinateElement = new Coordinate(cellCoordinateElement.Coordinate.PositionX, cellCoordinateElement.Coordinate.PositionY);
                var tabElement = this.Tabs[cellCoordinateElement.TabId];
                var cellElement = tabElement.getCellByCoordinate(coordinateElement);
                if (!visitedCells.Contains(cellElement))
                {
                    insert(cellCoordinateElement, value, visitedCells); 
                }
            }
        }

        public Cell getCellByCellCoordinate(CellCoordinate cellCoordinate)
        {
            return this.Tabs[cellCoordinate.TabId].getCellByCoordinate(cellCoordinate.Coordinate); 
        }

        public void print()
        {
            foreach(Grid tabElement in this.Tabs)
            {
                Console.WriteLine("========================================="+ tabElement.GridId);
                tabElement.print();
            }
        }

        public void link(int i)
        {
            for(int j= i+1; j < this.Tabs.Length-1; j++)
            {
                Tabs[i].link(Tabs[j]);
            }

            if(i + 1 < Tabs.Length)
            {
                this.link(i + 1);
            }
        }
    }
}
