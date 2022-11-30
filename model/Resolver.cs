using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    class Resolver
    {
        public Resolver() { }
        public bool isMisplaced(Coordinate relativeCoordinate, Grid grid) 
        { 
            return (this.isMisplacedByRow(relativeCoordinate, grid) || this.isMisplacedByColumn(relativeCoordinate, grid)) || isMisplacedByRegion(relativeCoordinate, grid);
        }

        public bool isMisplacedByRow(Coordinate relativeCoordinate, Grid grid)
        {
            int maxColumnIndex = grid.Opposite.Column - grid.Origin.Column;
            for (int currentIndex = 0; currentIndex <= maxColumnIndex; currentIndex++)
            {
                if (currentIndex == relativeCoordinate.Column) continue;
                if (grid.Cells[relativeCoordinate.Row, currentIndex].Value == 0) continue;
                if (grid.Cells[relativeCoordinate.Row, currentIndex].Value == grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isMisplacedByColumn(Coordinate relativeCoordinate, Grid grid)
        {
            int maxRowIndex = grid.Opposite.Row - grid.Origin.Row;
            for (int currentIndex = 0; currentIndex <= maxRowIndex; currentIndex++)
            {
                if (currentIndex == relativeCoordinate.Row) continue;
                if (grid.Cells[currentIndex, relativeCoordinate.Column].Value == 0) continue;
                if (grid.Cells[currentIndex, relativeCoordinate.Column].Value == grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isMisplacedByRegion(Coordinate relativeCoordinate, Grid grid)
        {
            int numberOfRegionRow = (int)Math.Sqrt(grid.Opposite.Row - grid.Origin.Row + 1);
            int numberOfRegionColumn = (int)Math.Sqrt(grid.Opposite.Column - grid.Origin.Column + 1);

            int a = relativeCoordinate.Row / numberOfRegionRow;
            int b = relativeCoordinate.Column / numberOfRegionColumn;

            int regionHeight = (grid.Opposite.Row - grid.Origin.Row) / numberOfRegionRow;
            int regionWidth = (grid.Opposite.Column - grid.Origin.Column) / numberOfRegionColumn;

            for(int i = a*regionHeight; i<= (a+1) * regionHeight; i++)
            {
                for (int j = b * regionWidth; j <= (b + 1) * regionWidth; j++)
                {
                    if (i == relativeCoordinate.Row && j == relativeCoordinate.Column) continue;
                    Console.WriteLine("test "+ i + " " + j);
                    if (grid.Cells[i, j].Value == 0) continue;
                    if (grid.Cells[i, j].Value == grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
