using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    class Resolver
    {
        public Resolver() { }
        public bool checkIfValueIsMisplaced(Coordinate relativeCoordinate, Grid grid) 
        { 
            return (this.checkIfValueIsMisplacedInRow(relativeCoordinate, grid) || this.checkIfValueIsMisplacedInColumn(relativeCoordinate, grid)) || checkIfValueIsMisplacedInRegion(relativeCoordinate, grid);
        }

        public bool checkIfValueIsMisplacedInRow(Coordinate relativeCoordinate, Grid grid)
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

        public bool checkIfValueIsMisplacedInColumn(Coordinate relativeCoordinate, Grid grid)
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

        public bool checkIfValueIsMisplacedInRegion(Coordinate relativeCoordinate, Grid grid)
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
                    if (grid.Cells[i, j].Value == 0) continue;
                    if (grid.Cells[i, j].Value == grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

       
        // Regions
        /*public List<Coordinate> getReplicateValueCoordinateForEachRegions(Grid grid)
        {
            List<Coordinate> replicateValueCoordinate = new List<Coordinate>();
            int numberOfRegionRow = (int)Math.Sqrt(grid.Opposite.Row - grid.Origin.Row + 1) - 1;
            int numberOfRegionColumn = (int)Math.Sqrt(grid.Opposite.Column - grid.Origin.Column + 1) - 1;
            for (int regionRowNumber = 0; regionRowNumber <= numberOfRegionRow; regionRowNumber++)
            {
                for (int regionColumnNumber = 0; regionColumnNumber <= numberOfRegionColumn; regionColumnNumber++)
                {
                    replicateValueCoordinate = replicateValueCoordinate.Union(getReplicateValueCoordinateForAGivenRegion(grid, regionRowNumber, regionColumnNumber)).ToList();
                }
            }
            return replicateValueCoordinate;
        }


        public List<Coordinate> getReplicateValueCoordinateForAGivenRegion(Grid grid, int regionRowNumber, int regionColumnNumber)
        {

            List<Coordinate> replicateValueCoordinate = new List<Coordinate>();
            int numberOfRegionRow = (int)Math.Sqrt(grid.Opposite.Row - grid.Origin.Row + 1) - 1;
            int numberOfRegionColumn = (int)Math.Sqrt(grid.Opposite.Column - grid.Origin.Column + 1) - 1;
            for (int currentRowIndex = 0; currentRowIndex <= numberOfRegionRow; currentRowIndex++)
            {
                for (int currentColumnIndex = 0; currentColumnIndex <= numberOfRegionColumn; currentColumnIndex++)
                {
                    if (grid.Cells[currentRowIndex, currentColumnIndex].Value == 0) continue;
                    for (int comparedRowIndex = currentRowIndex; comparedRowIndex <= numberOfRegionRow; comparedRowIndex++)
                    {
                        if (comparedRowIndex == currentRowIndex)
                        {
                            for (int comparedColumnIndex = currentColumnIndex + 1; comparedColumnIndex <= numberOfRegionColumn; comparedColumnIndex++)
                            {
                                if(grid.Cells[currentRowIndex, currentColumnIndex].Value == grid.Cells[comparedRowIndex, comparedColumnIndex].Value)
                                {
                                    replicateValueCoordinate.Add(new Coordinate(regionRowNumber * numberOfRegionRow + currentRowIndex, regionColumnNumber * numberOfRegionColumn + currentColumnIndex));
                                    replicateValueCoordinate.Add(new Coordinate(regionRowNumber * numberOfRegionRow + comparedRowIndex, regionColumnNumber * numberOfRegionColumn + comparedColumnIndex));
                                }
                            }
                        }
                        if(comparedRowIndex != currentRowIndex)
                        {
                            for (int j = 0; j <= numberOfRegionColumn; j++)
                            {
                                if (grid.Cells[currentRowIndex, currentColumnIndex].Value == grid.Cells[comparedRowIndex, j].Value)
                                {
                                    replicateValueCoordinate.Add(new Coordinate(regionRowNumber * numberOfRegionRow + currentRowIndex, regionColumnNumber * numberOfRegionColumn + currentColumnIndex));
                                    replicateValueCoordinate.Add(new Coordinate(regionRowNumber * numberOfRegionRow + comparedRowIndex, regionColumnNumber * numberOfRegionColumn + j));
                                }
                            }
                        }
                        
                    }
                }
            }
            return replicateValueCoordinate;
        }*/
    }
}
