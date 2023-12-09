using System;
using System.Collections.Generic;

namespace gameCenter.Projects.Snake.Models
{
    public class GameState
    {
        public int Rows { get; }
        public int Cols { get; }
        public GridValue[,] Grid { get; }
        public Direction Dir { get; private set; }
        public int Score { get; private set; }
        public bool GameOver { get; private set; }

        private readonly LinkedList<Direction> dirChanges= new LinkedList<Direction>();

        private readonly LinkedList<Position> snakePositions = new LinkedList<Position>(); //i use it because it allow us to add and delete from both side of the list

        private readonly Random random = new Random();

        public GameState(int rows, int cols) 
        {
            Rows = rows;
            Cols = cols;
            Grid = new GridValue[rows, cols];  // initialze the 2 dimention array
            Dir=Direction.Right; // when the game start the snake direction is right

            AddSnake();
            AddFood();
        }
        private void AddSnake()//  add snake to the solumn 1 to 3
        {
            int r = Rows / 2;
            for(int i = 1; i <= 3; i++)
            {
                Grid[r, i] = GridValue.Snake;
                snakePositions.AddFirst(new Position(r, i));
            }
        }

        // method which return all empty Grid position
        private IEnumerable<Position>EmptyPosition()
        {
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    if (Grid[i, j] == GridValue.Empty)
                    {
                        yield return new Position(i, j);
                    }
                }
            }
        }

        private void AddFood()
        {
            List<Position> empty = new List<Position>(EmptyPosition());

            if (empty.Count == 0)
            {
                return;
            }
            Position pos=empty[random.Next(empty.Count)];
            Grid[pos.Row, pos.Col] = GridValue.Food; // to add food.
        }

        //helper methods

        public Position HeadPosition()  //return the position of snake head, we get the position from linkedList
        {
            return snakePositions.First!.Value;
        }

        public Position TailPosition()   //return the position of snake head, we get the position from linkedList
        {
            return snakePositions.Last!.Value;
        }

        //return all snake position as an IEnumerable

        public IEnumerable<Position> SnakePositions()
        {
            return snakePositions;
        }

        //This method usefull for moving the snake
        private void AddHead(Position pos) //it add the given position to the front of the snake making it the new head
        {
            snakePositions.AddFirst(pos);
            Grid[pos.Row, pos.Col] = GridValue.Snake;
        }

        private void RemoveTail() //it remove the tail 
        {
            Position tail = snakePositions.Last!.Value;
            Grid[tail.Row, tail.Col] = GridValue.Empty;
            snakePositions.RemoveLast();
        }
        private Direction GetLastDirection()
        {
            if(dirChanges.Count == 0)
            {
                return Dir;
            }
            else
            {
                return dirChanges.Last.Value;
            }
        }
        private bool CanChangeDirection(Direction newdir)
        {
            if (dirChanges.Count == 2)
            {
                return false;
            }

            Direction lastDir=GetLastDirection();
            return newdir != lastDir && newdir != lastDir.Opposite();
        }
        public void ChangeDirection(Direction dir)
        {
            if(CanChangeDirection(dir))
            {
                dirChanges.AddLast(dir);
            } 
        }

        // method which check if the given position is outside the grid or not

        private bool OutSideGrid(Position pos)
        {
            return pos.Row < 0 || pos.Row >= Rows || pos.Col < 0 || pos.Col >= Cols;
        }

        // method which take a position as aparameter and return what the snake will hit if he move there
        private GridValue WillHit(Position newHeadPos)
        {
            if (OutSideGrid(newHeadPos))
            {
                return GridValue.Outside;
            }
            if(newHeadPos==TailPosition())
            {
                return GridValue.Empty;
            }
            return Grid[newHeadPos.Row, newHeadPos.Col];
        }

        public void Move()
        {
            if (dirChanges.Count>0)
            {
                Dir = dirChanges.First.Value;
                dirChanges.RemoveFirst();
            }

            Position newHeadPos = HeadPosition().Translate(Dir);
            GridValue hit =WillHit(newHeadPos);

            if (hit == GridValue.Outside || hit==GridValue.Snake) 
            {
                GameOver=true;
            }
            else if (hit==GridValue.Empty)
            {
                RemoveTail();
                AddHead(newHeadPos);

            }else if(hit==GridValue.Food) 
            {
                AddHead(newHeadPos);
                Score++;
                AddFood();
            }
        }
    }
}
