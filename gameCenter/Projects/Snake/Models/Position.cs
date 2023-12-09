using System;
using System.Collections.Generic;


namespace gameCenter.Projects.Snake.Models
{
    public class Position
    {
        public int Row { get; }
        public int Col { get; }

        public Position(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public Position Translate(Direction dir) // this function return the position we get by moving one step in the given direction
        {
            return new Position(Row + dir.RowOffset, Col + dir.ColOffset);
        }


        //override eqaul and gethashcode 
        public override bool Equals(object? obj)
        {
            return obj is Position position &&
                   Row == position.Row &&
                   Col == position.Col;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }

        public static bool operator ==(Position? left, Position? right)
        {
            return EqualityComparer<Position>.Default.Equals(left, right);
        }

        public static bool operator !=(Position? left, Position? right)
        {
            return !(left == right);
        }
    }
}
