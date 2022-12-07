﻿using Sudoku.model;
using System;
using System.Collections.Generic;

namespace Sudoku.Model
{
    public class Region
    {
        public int NumberOfCell { get; set; }
        public List<Coordinate> InnerCellsCoordinates { get; set; }
        public List<Coordinate> SurroundingCellsCoordinates { get; set; }

        public Region()
        {
            this.NumberOfCell = 0;
            this.InnerCellsCoordinates = new List<Coordinate>();
            this.SurroundingCellsCoordinates = new List<Coordinate>();
        }
    }
}
