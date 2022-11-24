using System;

namespace Sudoku.model
{
    class Resolver
    {
        public static bool check_input_grid(Grid grid)
        {
            if (!check_input_rows(grid))
            {
                return false;
            }
            /*if (!check_input_columns()) result = false;
            if (!check_input_sub_squares()) result = false;*/
            return true;
        }

        public static bool check_input_rows(Grid grid)
        {
            int rowLength = grid.Opposite.Row - grid.Origin.Row;
            for (int row = 0; row <= rowLength; row++)
            {
                if (!check_input_a_row(grid,row)) return false;
            }
            return true;
        }


        public static bool check_input_a_row(Grid grid, int row)
        {
           int rowLength = grid.Opposite.Row - grid.Origin.Row;
           for (int i = 0; i <= rowLength ; i++)
           {
              if (grid.Cells[row,i].Value == 0) continue;
              for (int j = i + 1; j <= rowLength; j++)
              {
                 if (grid.Cells[row, i].Value == grid.Cells[row, j].Value)
                 {
                    Console.WriteLine("Error : ");
                        new Coordinate(row, i).print();
                        new Coordinate(row, j).print();
                    return false;
                 }
              }
           }
           return true;
        }
    }
}
