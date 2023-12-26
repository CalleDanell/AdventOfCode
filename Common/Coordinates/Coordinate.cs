using System;
using System.Collections.Generic;

namespace Common.Coordinates
{
    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

        public void Move(int dx, int dy)
        {
            X += dx;
            Y += dy;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var c = (Coordinate)obj;
            return X.Equals(c.X) && Y.Equals(c.Y);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(X, Y).GetHashCode();
        }

        public List<Coordinate> GetAdjacent()
        {
            var up = new Coordinate(X, Y - 1);
            var down = new Coordinate(X, Y + 1);
            var left = new Coordinate(X - 1, Y);
            var right = new Coordinate(X + 1, Y);

            var upLeft = new Coordinate(X - 1, Y - 1);
            var upRight = new Coordinate(X + 1, Y - 1);
            var downLeft = new Coordinate(X - 1, Y + 1);
            var downRight = new Coordinate(X + 1, Y + 1);

            return new List<Coordinate> { up, down, left, right, upLeft, upRight, downLeft, downRight };
        }
    }
}
