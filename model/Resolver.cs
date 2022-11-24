using System;
using System.Collections.Generic;
using System.Linq;

namespace Sudoku.model
{
    class Resolver
    {
        public Resolver() { }
        public List<Coordinate> check_input_grid(Grid grid)
        {
            List<Coordinate> replicateValueCoordinate = new List<Coordinate>();
            replicateValueCoordinate = (List<Coordinate>)replicateValueCoordinate.Union(getReplicateValueCoordinateForEachRows(grid));
            /*if (!check_input_columns()) result = false;
            if (!check_input_sub_squares()) result = false;*/
            return replicateValueCoordinate;
        }

        public List<Coordinate> getReplicateValueCoordinateForEachRows(Grid grid)
        {
            List<Coordinate> replicateValueCoordinate = new List<Coordinate>();
            int rowLength = grid.Opposite.Row - grid.Origin.Row;
            for (int row = 0; row <= rowLength; row++)
            {
                replicateValueCoordinate = (List<Coordinate>)replicateValueCoordinate.Union(getReplicateValueCoordinateForAGivenRow(grid, row));
            }
            return replicateValueCoordinate;
        }


        public List<Coordinate> getReplicateValueCoordinateForAGivenRow(Grid grid, int row)
        {
            List<Coordinate> replicateValueCoordinate = new List<Coordinate>();
            int rowLength = grid.Opposite.Row - grid.Origin.Row;
            for (int i = 0; i <= rowLength; i++)
            {
                if (grid.Cells[row, i].Value == 0) continue;
                for (int j = i + 1; j <= rowLength; j++)
                {
                    if (grid.Cells[row, i].Value == grid.Cells[row, j].Value)
                    {
                        replicateValueCoordinate.Add(new Coordinate(row, i));
                        replicateValueCoordinate.Add(new Coordinate(row, j));
                    }
                }
            }
            return replicateValueCoordinate;
        }
    }
}
