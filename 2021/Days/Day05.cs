using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day05));

            var lines = input.Select(x => new Line(x.Replace(" -> ", ",").Split(','))).ToList();

            var maxX = lines.Max(x => x.X2) + 1;
            var maxY = lines.Max(x => x.Y2) + 1;

            var map = BuildMap(maxX, maxY);

            DrawLines(lines.Where(x => x.Direction().Equals(Line.Vertical) || x.Direction().Equals(Line.Horizontal)), map);
            var resultPartOne = map.Count(x => x.Value >= 2);

            DrawLines(lines.Where(x => x.Direction().Equals(Line.Diagonal)), map);
            var resultPartTwo = map.Count(x => x.Value >= 2);

            return (nameof(Day05), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static void DrawLines(IEnumerable<Line> lines, Dictionary<Coordinate, int> map)
        {
            foreach (var coordinate in lines.SelectMany(line => line.GetCoordinates()))
            {
                map[coordinate]++;
            }
        }

        private static Dictionary<Coordinate, int> BuildMap(int maxX, int maxY)
        {
            var map = new Dictionary<Coordinate, int>();
            for (var i = 0; i <= maxX; i++)
            {
                for (var j = 0; j <= maxY; j++)
                {
                    map.TryAdd(new Coordinate(i, j), 0);
                }
            }

            return map;
        }
    }

    public class Coordinate
    {
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

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
    }

    public class Line
    {
        public const string Diagonal = "diagonal";
        public const string Horizontal = "horizontal";
        public const string Vertical = "vertical";

        public Line(IReadOnlyList<string> coordinates)
        {
            X1 = int.Parse(coordinates[0]);
            Y1 = int.Parse(coordinates[1]);
            X2 = int.Parse(coordinates[2]);
            Y2 = int.Parse(coordinates[3]);
        }

        public int X1 { get; }
        public int Y1 { get; }
        public int X2 { get; }
        public int Y2 { get;  }

        public string Direction()
        {
            if (X1 == X2)
            {
                return Horizontal;
            }

            return Y1 == Y2 ? Vertical : Diagonal;
        }

        public List<Coordinate> GetCoordinates()
        {
            var coords = new List<Coordinate>();

            var minX = X1 < X2 ? X1 : X2;
            var maxX = X1 < X2 ? X2 : X1;
            var minY = Y1 < Y2 ? Y1 : Y2;
            var maxY = Y1 < Y2 ? Y2 : Y1;

            if (Y1 == Y2)
            {
                while (minX <= maxX)
                {
                    coords.Add(new Coordinate(minX, Y1));
                    minX++;
                }
            }
            else if(X1 == X2)
            {
                while (minY <= maxY)
                {
                    coords.Add(new Coordinate(X1, minY));
                    minY++;
                }
            }
            else
            {
                var incrementX = true;
                var incrementY = true;

                var tempX = X1;
                var tempY = Y1;

                if (X1 > X2)
                {
                    incrementX = false;
                }

                if (Y1 > Y2)
                {
                    incrementY = false;
                }

                while (tempX != X2 && tempY != Y2)
                {
                    coords.Add(new Coordinate(tempX, tempY));

                    if (incrementX)
                        tempX++;
                    else
                    {
                        tempX--;
                    }

                    if (incrementY)
                        tempY++;
                    else
                    {
                        tempY--;
                    }
                }

                coords.Add(new Coordinate(tempX, tempY));
            }

            return coords;
        }
    }
}