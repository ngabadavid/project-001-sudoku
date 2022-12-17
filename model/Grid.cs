using Sudoku.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    public class Grid
    {
        // Randomization depend of 2 factor : the seed and the number of swap.
        // The same seed and same number of swap result to the same region configuration
        private int GridSeed { get; set; }
        private int GridSwap { get; set; }
        public Coordinate Origin { get; set; }
        public Coordinate Opposite { get; set; }
        public Region[] Regions { get; set; }
        public int[,] RegionMap { get; set; }
        public Cell[,] Cells { get; set; }
        public Grid(int gridSeed,int gridSwap, Coordinate origin, Coordinate opposite)
        {
            this.GridSeed = gridSeed;
            this.GridSwap = gridSwap;
            this.Origin = origin;
            this.Opposite = opposite;


            GenerateInitialRegionEdge();
            //RandomizeRegionEdge();
        }

        public void GenerateInitialRegionEdge()
        {
            int gridSideLength = Opposite.Column - Origin.Column + 1;
            int regionSideLength = (int)Math.Sqrt(gridSideLength);

            Regions = new Region[gridSideLength];
            RegionMap = new int[gridSideLength, gridSideLength];

            // Divide the RegionMap in equals sized square regions.
            // Go throught regions horizontally
            for (int i = 0; i < regionSideLength; i++)
            {
                // Go throught regions vertically
                for (int j = 0; j < regionSideLength; j++)
                {
                    // Create a region and add it to the Regions array
                    Regions[i * regionSideLength + j] = new Region();

                    // Go throught the region cells horizontally
                    for (int k = 0; k < regionSideLength; k++)
                    {
                        // Go throught the region cells vertically
                        for (int l = 0; l < regionSideLength; l++)
                        {
                            // Set the value of the region Id in the RegionMap array
                            RegionMap[i * regionSideLength + k, j * regionSideLength + l] = i * regionSideLength + j + 1;

                            // Add the cell coordinate to the inner cells coordinates list of the region.
                            Regions[i * regionSideLength + j].InnerCellsCoordinates.Add(new Coordinate(i * regionSideLength + k, j * regionSideLength + l));

                            // Increment the number of cell of the region
                            Regions[i * regionSideLength + j].NumberOfCell++;
                        }
                    }

                    // Add cell coordinate of region surrounding cells to the surrounding cells list of the region
                    // Check if cell at left side of the region are not outside of the bound
                    if (i * regionSideLength - 1 >= 0)
                    {
                        for (int k = 0; k < regionSideLength; k++)
                        {
                            Regions[i * regionSideLength + j].SurroundingCellsCoordinates.Add(new Coordinate(i * regionSideLength - 1, regionSideLength * j + k));
                        }

                    }

                    // Check if cell at right side of the region are not outside of the bound
                    if (i * regionSideLength + regionSideLength < gridSideLength)
                    {
                        for (int k = 0; k < regionSideLength; k++)
                        {
                            Regions[i * regionSideLength + j].SurroundingCellsCoordinates.Add(new Coordinate(i * regionSideLength + regionSideLength, regionSideLength * j + k));
                        }
                    }

                    // Check if cell at top side of the region are not outside of the bound
                    if (j * regionSideLength - 1 >= 0)
                    {
                        for (int k = 0; k < regionSideLength; k++)
                        {
                            Regions[i * regionSideLength + j].SurroundingCellsCoordinates.Add(new Coordinate(regionSideLength * i + k, j * regionSideLength - 1));
                        }
                    }

                    // Check if cell at bottom side of the region are not outside of the bound
                    if (j * regionSideLength + regionSideLength < gridSideLength)
                    {
                        for (int k = 0; k < regionSideLength; k++)
                        {
                            Regions[i * regionSideLength + j].SurroundingCellsCoordinates.Add(new Coordinate(regionSideLength * i + k, j * regionSideLength + regionSideLength));
                        }
                    }
                }
            }
        }

        // Randomize the edge and form of each initial equal sized square region
        /*
         - Randomly choose a cell in the list of surrounding cell;
         - check if that cell does not devide another region into discountinuous region
         - remove the cell from the list of surrounding cell
         - add the cell in the list of inner cell
         - get the 4 cell perpendiculary surroundig that cell that are not inside the inner list yet
         - add those cell to the list of surrounding cells.
         - do all does step reciprocably to the region that have been amputee of its cell.
        */
        public void RandomizeRegionEdge()
        {
            Random random = new Random(GridSeed);

            int regionSideLength = (int)Math.Sqrt(RegionMap.Length);
            Console.WriteLine(GridSwap);
            for (int swap = 0; swap < GridSwap; swap++)
            {   
                // Randomly choose a region
                int regionAIndexInRegions = random.Next(0, regionSideLength);
                int regionBIndexInRegions = regionAIndexInRegions;
                Region regionA = Regions[regionAIndexInRegions];
                Region regionB = regionA;
                Coordinate finalCellInvadeByRegionA = regionB.InnerCellsCoordinates[0];

                // Go randomly throught the surrounding cell coordinate list of regionA
                bool invasionByASuccessful = false;
                List<Coordinate> regionASurroundingCellsCoordinates = regionA.SurroundingCellsCoordinates;
                // Shuffle the list
                regionASurroundingCellsCoordinates = regionASurroundingCellsCoordinates.OrderBy(x => random.Next()).ToList();
                foreach (Coordinate cellInvadeByRegionA in regionASurroundingCellsCoordinates)
                {
                    if(invasionByASuccessful == false)
                    {
                        // Check if the cell can be invade
                        if (CanBeInvaded(cellInvadeByRegionA))
                        {
                            // If the cell can be invade, remove it from the surrounding cell list and add it to the inner cell list
                            regionA.SurroundingCellsCoordinates.RemoveAll(coordinate => coordinate.Equals(cellInvadeByRegionA));
                            regionA.InnerCellsCoordinates.Add(cellInvadeByRegionA);

                            // Get the information of the invaded region for the reverse invasion
                            regionBIndexInRegions = RegionMap[cellInvadeByRegionA.Row, cellInvadeByRegionA.Column] - 1;
                            regionB = Regions[regionBIndexInRegions];

                            // Change the  region id of the cell in the RegionMap array
                            RegionMap[cellInvadeByRegionA.Row, cellInvadeByRegionA.Column] = regionAIndexInRegions + 1;

                            // Adjust the surrounding cell list of regionA to the new topologie
                            foreach (Coordinate coordinate in GetCoordinatesOfSurroundingCells(cellInvadeByRegionA))
                            {
                                if (!regionA.SurroundingCellsCoordinates.Contains(coordinate) && !regionA.InnerCellsCoordinates.Contains(coordinate))
                                {
                                    regionA.SurroundingCellsCoordinates.Add(coordinate);
                                }
                            }

                            // Remove the cell from the inner cell list of the invaded region and add it to the surrounding cell list
                            regionB.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionA));
                            regionB.SurroundingCellsCoordinates.Add(cellInvadeByRegionA);

                            // Adjust the surrounding cell list of regionB to the new topologie
                            foreach (Coordinate coordinate in GetCoordinatesOfSurroundingCells(cellInvadeByRegionA))
                            {
                                if (!IsConnectedToTheRegion(coordinate, regionB))
                                {
                                    regionB.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                                }
                            }

                            // Confirm that the invasion has been successul
                            invasionByASuccessful = true;
                            finalCellInvadeByRegionA = cellInvadeByRegionA;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

               
                if (invasionByASuccessful)
                {
                    bool invasionByBSuccessful = false;
                    // Get the list of coordinate surrounding region B and that are part of inner cell coordinate of region A
                    List<Coordinate> candidateToInvasionByRegionB = new List<Coordinate>();
                    foreach(Coordinate coordinate in regionB.SurroundingCellsCoordinates)
                    {
                        if (regionA.InnerCellsCoordinates.Contains(coordinate))
                        {
                            candidateToInvasionByRegionB.Add(coordinate);
                        }
                    }
                    // Shuffle the list
                    candidateToInvasionByRegionB = candidateToInvasionByRegionB.OrderBy(x => random.Next()).ToList();
                    foreach (Coordinate cellInvadeByRegionB in candidateToInvasionByRegionB)
                    {
                        if (invasionByBSuccessful == false)
                        {
                            if(cellInvadeByRegionB == finalCellInvadeByRegionA)
                            {
                                continue;
                            }
                            // Check if the cell can be invade
                            if (CanBeInvaded(cellInvadeByRegionB))
                            {
                                // If the cell can be invade, remove it from the surrounding cell list and add it to the inner cell list
                                regionB.SurroundingCellsCoordinates.RemoveAll(coordinate => coordinate.Equals(cellInvadeByRegionB));
                                regionB.InnerCellsCoordinates.Add(cellInvadeByRegionB);

                                // Change the  region id of the cell in the RegionMap array
                                RegionMap[cellInvadeByRegionB.Row, cellInvadeByRegionB.Column] = regionBIndexInRegions + 1;

                                // Adjust the surrounding cell list of regionB to the new topologie
                                foreach (Coordinate coordinate in GetCoordinatesOfSurroundingCells(cellInvadeByRegionB))
                                {
                                    if (!regionB.SurroundingCellsCoordinates.Contains(coordinate) && !regionB.InnerCellsCoordinates.Contains(coordinate))
                                    {
                                        regionB.SurroundingCellsCoordinates.Add(coordinate);
                                    }
                                }

                                // Remove the cell from the inner cell list of the invaded region and add it to the surrounding cell list
                                regionA.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(cellInvadeByRegionB));
                                regionA.SurroundingCellsCoordinates.Add(cellInvadeByRegionB);

                                // Adjust the surrounding cell list of regionA to the new topologie
                                foreach (Coordinate coordinate in GetCoordinatesOfSurroundingCells(cellInvadeByRegionB))
                                {
                                    if (!IsConnectedToTheRegion(coordinate, regionA))
                                    {
                                        regionA.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                                    }
                                }

                                // Reorganize the cells value table
                                foo(finalCellInvadeByRegionA, Cells[cellInvadeByRegionB.Row, cellInvadeByRegionB.Column].Value);

                                // Confirm that the invasion has been successul
                                invasionByBSuccessful = true;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (invasionByBSuccessful == false)
                    {
                        // If the cell can be invade, remove it from the surrounding cell list and add it to the inner cell list
                        regionB.SurroundingCellsCoordinates.RemoveAll(coordinate => coordinate.Equals(finalCellInvadeByRegionA));
                        regionB.InnerCellsCoordinates.Add(finalCellInvadeByRegionA);

                        // Change the  region id of the cell in the RegionMap array
                        RegionMap[finalCellInvadeByRegionA.Row, finalCellInvadeByRegionA.Column] = regionBIndexInRegions + 1;

                        // Adjust the surrounding cell list of regionB to the new topologie
                        foreach (Coordinate coordinate in GetCoordinatesOfSurroundingCells(finalCellInvadeByRegionA))
                        {
                            if (!regionB.SurroundingCellsCoordinates.Contains(coordinate) && !regionB.InnerCellsCoordinates.Contains(coordinate))
                            {
                                regionB.SurroundingCellsCoordinates.Add(coordinate);
                            }
                        }

                        // Remove the cell from the inner cell list of the invaded region and add it to the surrounding cell list
                        regionA.InnerCellsCoordinates.RemoveAll(cell => cell.Equals(finalCellInvadeByRegionA));
                        regionA.SurroundingCellsCoordinates.Add(finalCellInvadeByRegionA);

                        // Adjust the surrounding cell list of regionA to the new topologie
                        foreach (Coordinate coordinate in GetCoordinatesOfSurroundingCells(finalCellInvadeByRegionA))
                        {
                            if (!IsConnectedToTheRegion(coordinate, regionA))
                            {
                                regionA.SurroundingCellsCoordinates.RemoveAll(cell => cell.Equals(coordinate));
                            }
                        }

                        // Confirm that the invasion has been successul
                        invasionByBSuccessful = true;
                    }
                }
            }   
        }


        public List<Coordinate> GetCoordinatesOfSurroundingCells(Coordinate coordinate)
        {
            List<Coordinate> coordinatesOfSurroundingCells = new List<Coordinate>();
            int row = coordinate.Row;
            int column = coordinate.Column;
            int gridSideLength = (int)Math.Sqrt(RegionMap.Length);

            // Checked Left
            if (row - 1 >= 0)
            {
                coordinatesOfSurroundingCells.Add(new Coordinate(row - 1, column));

            }
            // Check Right
            if (row + 1 < gridSideLength)
            {
                coordinatesOfSurroundingCells.Add(new Coordinate(row + 1, column));
            }
            // Checked Up
            if (column - 1 >= 0)
            {
                coordinatesOfSurroundingCells.Add(new Coordinate(row, column - 1));
            }
            // Check Bottom
            if (column + 1 < gridSideLength)
            {
                coordinatesOfSurroundingCells.Add(new Coordinate(row, column + 1));
            }
            return coordinatesOfSurroundingCells;
        }

        public bool IsConnectedToTheRegion(Coordinate coordinate, Region region)
        {
            if (region.InnerCellsCoordinates.Contains(coordinate))
            {
                return true;
            }
            else
            {
                foreach (Coordinate coordinateOfASurroundingCell in GetCoordinatesOfSurroundingCells(coordinate))
                {
                    if (region.InnerCellsCoordinates.Contains(coordinateOfASurroundingCell))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool CanBeInvaded(Coordinate coordinate)
        {
            int row = coordinate.Row;
            int column = coordinate.Column;
            int gridSideLength = (int)Math.Sqrt(RegionMap.Length);
            Region region = Regions[RegionMap[coordinate.Row, coordinate.Column] - 1];
          
            // Check if the cell you want to invade has its perpendicular and diagonal surrounding cells of the same region than the itself continuously connected to each other.
            List<Coordinate> coordinatesOfSurroundingCells = new List<Coordinate>();

            // Checked Left
            if (row - 1 >= 0)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row - 1, column);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Checked Left-Up
            if (row - 1 >= 0 && column - 1 >= 0)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row - 1, column - 1);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Checked Up
            if (column - 1 >= 0)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row, column - 1);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Check Up-Right
            if (row + 1 < gridSideLength && column - 1 >= 0)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row + 1, column - 1);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Check Right
            if (row + 1 < gridSideLength)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row + 1, column);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Check Right-Bottom
            if (row + 1 < gridSideLength && column + 1 < gridSideLength)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row + 1, column + 1);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Check Bottom
            if (column + 1 < gridSideLength)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row, column + 1);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }
            // Check Bottom-Left
            if (row - 1 >= 0 && column + 1 < gridSideLength)
            {
                Coordinate coordinatesOfASurroundingCell = new Coordinate(row - 1, column + 1);
                if (region.InnerCellsCoordinates.Contains(coordinatesOfASurroundingCell))
                {
                    coordinatesOfSurroundingCells.Add(coordinatesOfASurroundingCell);
                }
            }

            // This case should not happen since a cell should always be connected to the body of its region
            // Therefore, it should always be at least one surrounding cell of the same region than the cell we are trying to invade
            if (coordinatesOfSurroundingCells.Count == 0)
            {
                return false;
            }

            return BuildListOfContinuousCellsStartingByCellAtPositionZero(coordinatesOfSurroundingCells[0], coordinatesOfSurroundingCells, new List<Coordinate>()).Count == coordinatesOfSurroundingCells.Count;
        }

        public List<Coordinate> BuildListOfContinuousCellsStartingByCellAtPositionZero(Coordinate coordinateAtPositionZero, List<Coordinate> coordinatesOfSurroundingCells, List<Coordinate> coordinatesOfSurroundingCellsBuilding)
        {
            coordinatesOfSurroundingCellsBuilding.Add(coordinateAtPositionZero);
            foreach (Coordinate c in GetCoordinatesOfSurroundingCells(coordinateAtPositionZero))
            {
                if (coordinatesOfSurroundingCells.Contains(c) && !coordinatesOfSurroundingCellsBuilding.Contains(c))
                {
                    coordinatesOfSurroundingCellsBuilding = coordinatesOfSurroundingCellsBuilding.Union(BuildListOfContinuousCellsStartingByCellAtPositionZero(c, coordinatesOfSurroundingCells,coordinatesOfSurroundingCellsBuilding)).ToList();
                }
            }
            return coordinatesOfSurroundingCellsBuilding;
        }

        public void Print()
        {
            Console.WriteLine("Grid: =====================================");
            for (int row = 0; row <= Opposite.Row - Origin.Row; row++)
            {
                for (int column = 0; column <= Opposite.Column - Origin.Column; column++)
                {
                    Console.Write("|");
                    Console.Write(Cells[row, column].Value);
                    Console.Write("|");
                }
                Console.WriteLine("");
            }
        }

        public void PrintRegionMap()
        {
            Console.WriteLine("Grid: =====================================");
            for (int row = 0; row <= Opposite.Row - Origin.Row; row++)
            {
                for (int column = 0; column <= Opposite.Column - Origin.Column; column++)
                {
                    Console.Write("|");
                    Console.Write(RegionMap[row, column]);
                    Console.Write("|");
                }
                Console.WriteLine("");
            }
        }

        public void foo(Coordinate coordinate, int value)
        {
            int a = Cells[coordinate.Row, coordinate.Column].Value;
            Cells[coordinate.Row, coordinate.Column].Value = value;
            for (int i = 0; i < 9; i++)
            {
                if (Cells[coordinate.Row, i].Value == value && i != coordinate.Column)
                {
                    foo(new Coordinate(coordinate.Row, i), a);
                }
                if (Cells[i, coordinate.Column].Value == value && i != coordinate.Row)
                {
                    foo(new Coordinate(i,coordinate.Column), a);
                }
            }
        }
    }
}
