using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameCenter.Projects.Tetris.Models
{
    public class GameState
    {
        private BlockShape currentBlock;

        public BlockShape CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);

                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        public GameGrid GameGrid { get; }
        public BlockQueue BlockQueue { get; }
        public bool GameOver { get; private set; }
        public int Score { get; private set; }
        public BlockShape HeldBlock { get; private set; }
        public bool CanHold { get; private set; }

        public GameState()
        {
            GameGrid = new GameGrid(22, 10);
            BlockQueue = new BlockQueue();
            CurrentBlock = BlockQueue.GetAndUpdate();
            CanHold = true;
        }

        private bool BlockFits() //  check if the current block is in a legal position or not
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!GameGrid.IsEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }

            return true;
        }

        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }

            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = BlockQueue.GetAndUpdate();
            }
            else
            {
                BlockShape tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }

            CanHold = false;
        }

        public void RotateBlockCW()  //method which rotate the current block clockwise but also if it possible to do from where he is
        {
            CurrentBlock.RotateCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCCW();
            }
        }

        public void RotateBlockCCW()//method which rotate the current block counterclockwise but also if it possible to do from where he is
        {
            CurrentBlock.RotateCCW();

            if (!BlockFits())
            {
                CurrentBlock.RotateCW();
            }
        }


        //method for moving the block left 
        public void MoveBlockLeft() 
        {
            CurrentBlock.Move(0, -1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }


        //method for moving the block  right
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);

            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }
        private bool IsGameOver() //method wil check if the game is over
        {
            return !(GameGrid.IsRowEmpty(0) && GameGrid.IsRowEmpty(1));
        }

        private void PlaceBlock() //method wil called in case when the current block can not move down 
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                GameGrid[p.Row, p.Column] = CurrentBlock.Id;
            }

            Score += GameGrid.ClearFullRows();

            if (IsGameOver())
            {
                GameOver = true;
            }
            else
            {
                CurrentBlock = BlockQueue.GetAndUpdate();
                CanHold = true;
            }
        }

        public void MoveBlockDown() // move sown method
        {
            CurrentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        private int TileDropDistance(Position p)//method which take a position and return a number of empty cells immeditlly 
        {
            int drop = 0;

            while (GameGrid.IsEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }

            return drop;
        }
        public int BlockDropDistance() //return the number of empty cell we can find up how many rows the current block can be moved down we invoke for every tile in the current block and take the min
        {
            int drop = GameGrid.Rows;

            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }

            return drop;
        }

        public void DropBlock() // move the current block down as many rows is possible and then places it in the grid
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }

}
