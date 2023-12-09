using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.Tetris.Models
{
    public abstract class BlockShape
    {
        protected abstract Position[][] Tiles { get; }  
        protected abstract Position StartOffset { get; } //decide where the block bone in the grid
        public abstract int Id { get; }

        private int rotationState;
        private Position offset;

        public BlockShape()
        {
            offset = new Position(StartOffset.Row, StartOffset.Column);
        }

        public IEnumerable<Position> TilePositions()  //this method loop over the tile position in the current rotation state and the rows offset and the colomn offset
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        public void RotateCW() //this method rotate the block 90 degrees with clock wise
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        public void RotateCCW()//this method to rotate count the clock wise
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        public void Move(int rows, int columns) //this method move the block by given number of rows and coloumns
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        public void Reset() //method which reset the rotation and position
        {
            rotationState = 0;
            offset.Row = StartOffset.Row;
            offset.Column = StartOffset.Column;
        }
    }
}
