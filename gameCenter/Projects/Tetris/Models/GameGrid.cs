using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.Tetris.Models
{
    public class GameGrid
    {
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        public GameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }


        public bool IsInside(int r, int c) //will check if the given cols and rows is inside the grid or not
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }



        public bool IsEmpty(int r, int c)  //method that check if the given cell is empty or not

        {
            return IsInside(r, c) && grid[r, c] == 0;
        }

        //method which check if entire rows is full

        public bool IsRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;

        }

        //method which check if Rows is empty

        public bool IsRowEmpty(int r)
        {
            for(int c = 0;c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        //method which clear the Row

        private void ClearRow(int r)
        {
            for(int c=0; c< Columns; c++)
            {
                grid[r, c] = 0;
            }
        }
        //method which move the row down when make aclear process

        private void MoveRowDown(int r,int numRows)
        {
            for(int c=0;c< Columns; c++)
            {
                grid[r+numRows, c] = grid[r,c];
                grid[r,c] = 0;
            }
        }

        //method wich meake clear full rows

        public int ClearFullRows()
        {
            int cleared = 0;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (IsRowFull(r))
                {
                    ClearRow(r);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(r, cleared);
                }
            }

            return cleared;
        }

    }
}
