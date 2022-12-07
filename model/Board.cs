using System;
using System.Collections.Generic;

namespace Sudoku.model
{
    public class Board
    {
        public List<Grid> Grids { get; set; }
        public List<Coordinate> MisplacedValuesCoordinates { get; set; }

        public Board(List<Grid> grids)
        {
            this.Grids = grids;
            this.MisplacedValuesCoordinates = new List<Coordinate>();
        }
        
        public void InsertValueAtGivenCoordinate(int value, Coordinate absoluteCoordinate)
        {
            bool isMisplaced = false;
            foreach(Grid grid in Grids)
            {
                if (CheckIfGridContainCellAtGivenCoordinate(absoluteCoordinate, grid))
                {
                    Coordinate relativeCoordinate = TransformAbsoluteCoordinateToRelativeCoordinate(absoluteCoordinate, grid);
                    grid.Cells[relativeCoordinate.Row, relativeCoordinate.Column].Value = value;
                    grid.Print();
                    if(new Resolver().isMisplaced(relativeCoordinate, grid)){
                        isMisplaced = true;
                        MisplacedValuesCoordinates.Add(absoluteCoordinate);
                        break;
                    }
                }
            }
            if(!isMisplaced)
            {
                List<Coordinate> MisplacedValuesCoordinatestmp = MisplacedValuesCoordinates.FindAll(coordinate => coordinate.Row != absoluteCoordinate.Row && coordinate.Column != absoluteCoordinate.Column);
                MisplacedValuesCoordinates = MisplacedValuesCoordinatestmp;
            }
        }

        public bool CheckIfGridContainCellAtGivenCoordinate(Coordinate absoluteCoordinate, Grid grid)
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

        public Coordinate TransformAbsoluteCoordinateToRelativeCoordinate(Coordinate absoluteCoordinate, Grid grid)
        {
            return new Coordinate(absoluteCoordinate.Row - grid.Origin.Row, absoluteCoordinate.Column - grid.Origin.Column);
        }

        public void Print()
        {
            Console.WriteLine("Board: =====================================");
            foreach (Grid gridElement in this.Grids)
            {
                gridElement.Print();
            }
        }
    }
}
