using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    class Resolver
    {
        int counter = 0;
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
            int regionId = grid.RegionMap[relativeCoordinate.Row, relativeCoordinate.Column];
            List<Coordinate> listOfCoordinateInsideRegion = grid.Regions[regionId - 1].InnerCellsCoordinates;
            foreach(Coordinate coordinate in listOfCoordinateInsideRegion)
            {
                if (coordinate.Equals(relativeCoordinate)) continue;
                if (grid.Cells[coordinate.Row, coordinate.Column].Value == 0) continue;
                if (grid.Cells[coordinate.Row, coordinate.Column].Value == grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value)
                {
                    return true;
                }
            }
            return false;
        }

        // Check that the grid is fully completed
        public bool CheckGrid(Grid grid)
        {
            int gridSideLength = grid.Opposite.Row - grid.Origin.Row;
            for (int i =0; i <= gridSideLength; i++)
            {
                for (int j = 0; j <= gridSideLength; j++)
                {
                    if (grid.Cells[i,j].Value == 0)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public bool TestValueAtCoordinate(int value, Coordinate relativeCoordinate, Grid grid)
        {
            return (this.TestValueAtCoordinateRow(value, relativeCoordinate, grid) || this.TestValueAtCoordinateColumn(value, relativeCoordinate, grid)) || TestValueAtCoordinateRegion(value, relativeCoordinate, grid);
        }

        public bool TestValueAtCoordinateRow(int value, Coordinate relativeCoordinate, Grid grid)
        {
            int maxColumnIndex = grid.Opposite.Column - grid.Origin.Column;
            for (int currentIndex = 0; currentIndex <= maxColumnIndex; currentIndex++)
            {
                if (currentIndex == relativeCoordinate.Column) continue;
                if (grid.Cells[relativeCoordinate.Row, currentIndex].Value == 0) continue;
                if (grid.Cells[relativeCoordinate.Row, currentIndex].Value == value)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestValueAtCoordinateColumn(int value, Coordinate relativeCoordinate, Grid grid)
        {
            int maxRowIndex = grid.Opposite.Row - grid.Origin.Row;
            for (int currentIndex = 0; currentIndex <= maxRowIndex; currentIndex++)
            {
                if (currentIndex == relativeCoordinate.Row) continue;
                if (grid.Cells[currentIndex, relativeCoordinate.Column].Value == 0) continue;
                if (grid.Cells[currentIndex, relativeCoordinate.Column].Value == value)
                {
                    return true;
                }
            }
            return false;
        }

        public bool TestValueAtCoordinateRegion(int value, Coordinate relativeCoordinate, Grid grid)
        {
            int regionId = grid.RegionMap[relativeCoordinate.Row, relativeCoordinate.Column];
            List<Coordinate> listOfCoordinateInsideRegion = grid.Regions[regionId - 1].InnerCellsCoordinates;
            foreach (Coordinate coordinate in listOfCoordinateInsideRegion)
            {
                if (coordinate.Equals(relativeCoordinate)) continue;
                if (grid.Cells[coordinate.Row, coordinate.Column].Value == 0) continue;
                if (grid.Cells[coordinate.Row, coordinate.Column].Value == value)
                {
                    return true;
                }
            }
            return false;
        }

        // Backtracking/recursive function to check all possible combinations of numbers until a solution is found
        public bool Solver(int gridSeed, Grid grid)
        {
            Random random = new Random(gridSeed);
            // Find next empty cell
            int gridSideLength = grid.Opposite.Row - grid.Origin.Row + 1;
            List<int> values = new List<int>();
            for (int value = 1; value <= gridSideLength; value++)
            {
                values.Add(value);
            }
            for (int i = 0; i < grid.Cells.Length; i++)
            {
                int row = i / gridSideLength;
                int column = i % gridSideLength;
                if (grid.Cells[row, column].Value == 0)
                {
                    values = values.OrderBy(x => random.Next()).ToList();
                    foreach (int value in values)
                    {
                        // Check that this value has not already be used on row, column or region
                        if (!TestValueAtCoordinate(value, new Coordinate(row, column), grid))
                        {
                            grid.Cells[row, column].Value = value;
                            grid.Print();
                            if (CheckGrid(grid))
                            {
                                counter += 1;
                                return true;
                            }
                            else
                            {
                                if (Solver(gridSeed, grid))
                                {
                                    return true;
                                }
                                grid.Cells[row, column].Value = 0;
                            }
                        }
                    }
                    break;
                }
            }
            return false;
        }
    }
}
