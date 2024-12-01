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

        public override string ToString()
        {
            return $"{X}{Y}";
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

        /// <summary>
        /// 1 = North
        /// 2 = West
        /// 3 = South
        /// 4 = East
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Coordinate GetNextCoordinate(int direction)
        {
            Coordinate next = new(0, 0);
            switch (direction)
            {
                case 1:
                    next = GetNorth();
                    break;
                case 2:
                    next = GetWest();
                    break;
                case 3:
                    next = GetSouth();
                    break;
                case 4:
                    next = GetEast();
                    break;
            }

            return next;
        }


        public Coordinate GetNorth()
        {
            return new Coordinate(X, Y - 1);
        }
        public Coordinate GetSouth()
        {
            return new Coordinate(X, Y + 1);
        }

        public Coordinate GetWest()
        {
            return new Coordinate(X - 1, Y);
        }

        public Coordinate GetEast()
        {
            return new Coordinate(X + 1, Y);
        }

        public List<Coordinate> GetAdjacent(bool includeSelfLast)
        {
            var up = new Coordinate(X, Y - 1);
            var down = new Coordinate(X, Y + 1);
            var left = new Coordinate(X - 1, Y);
            var right = new Coordinate(X + 1, Y);

            var upLeft = new Coordinate(X - 1, Y - 1);
            var upRight = new Coordinate(X + 1, Y - 1);
            var downLeft = new Coordinate(X - 1, Y + 1);
            var downRight = new Coordinate(X + 1, Y + 1);

            var res = new List<Coordinate> { up, down, left, right, upLeft, upRight, downLeft, downRight };
            if (includeSelfLast) res.Add(this);

            return res;
        }
    }
}
