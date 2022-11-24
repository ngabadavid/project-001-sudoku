using System;
using System.Collections.Generic;

namespace Sudoku.model
{
    public class Board
    {
        public List<Grid> Grids { get; set; }

        public Board(List<Grid> grids)
        {
            this.Grids = grids;
        }
        
        public List<Grid> insertValueAtGivenCoordinate(int value, Coordinate absoluteCoordinate)
        {
            List<Grid> gridsContainingCellAtGivenCoordinate = new List<Grid>();
            foreach(Grid grid in Grids)
            {
                if (checkIfGridContainCellAtGivenCoordinate(absoluteCoordinate, grid))
                {
                    gridsContainingCellAtGivenCoordinate.Add(grid);
                    Coordinate relativeCoordinate = transformAbsoluteCoordinateToRelativeCoordinate(absoluteCoordinate, grid);
                    grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value = value;
                }
            }
            return gridsContainingCellAtGivenCoordinate;
        }

        public bool checkIfGridContainCellAtGivenCoordinate(Coordinate absoluteCoordinate, Grid grid)
        {
            if (grid.Origin.Row <= absoluteCoordinate.Row && absoluteCoordinate.Row <= grid.Opposite.Row)
            {
                if (grid.Origin.Column <= absoluteCoordinate.Column && absoluteCoordinate.Column <= grid.Opposite.Column)
                {
                    return true;
                }
            }
            return false;
        }

        public Coordinate transformAbsoluteCoordinateToRelativeCoordinate(Coordinate absoluteCoordinate, Grid grid)
        {
            return new Coordinate(absoluteCoordinate.Row - grid.Origin.Row, absoluteCoordinate.Column - grid.Origin.Column);
        }

        public void print()
        {
            Console.WriteLine("Board: =====================================");
            foreach (Grid gridElement in this.Grids)
            {
                gridElement.print();
            }
        }
    }
}
