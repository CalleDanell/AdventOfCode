using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.Coordinates;
using System.IO;

namespace _2018.Days
{
    public class Day10 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day10));
            var points = input.Select(x => new Point(x)).ToList();

            for(var i = 0; i < 15000; i++)
            {
                var xMin = points.Min(x => x.Coordinate.X);
                var xMax = points.Max(x => x.Coordinate.X);

                var distance = xMax - xMin;

                if(distance < 75)
                {
                    Print(points, i);
                }

                points.ForEach(x => x.Move(1));
            }

            return (nameof(Day10), "See output file", "See output file");
        }

        private static void Print(IEnumerable<Point> points, int iteration)
        {
            File.AppendAllText("output.txt", Environment.NewLine);
            File.AppendAllText("output.txt", iteration.ToString() + Environment.NewLine);

            var xMin = points.Min(x => x.Coordinate.X);
            var yMin = points.Min(x => x.Coordinate.Y);
            var xMax = points.Max(x => x.Coordinate.X);
            var yMax = points.Max(x => x.Coordinate.Y);

            var coords = points.Select(x => x.Coordinate).ToHashSet();

            for (var y = yMin; y <= yMax; y++)
            {
                var line = string.Empty;
                for (var x = xMin; x <= xMax; x++)
                {
                    var current = new Coordinate(x, y);
                    if (coords.Contains(current)) {
                        line += "#";
                    } else
                    {
                        line += ".";
                    }
                }

                File.AppendAllText("output.txt", line + Environment.NewLine);
            }
        }
    }

    internal class Point
    {
        public Point(string s)
        {
            var clean = s.Replace("position=<", ",").Replace("> velocity=<", ",").Replace(">", string.Empty);
            var parts = clean.Split(",", System.StringSplitOptions.TrimEntries);
            var x = int.Parse(parts[1]);
            var y = int.Parse(parts[2]);
            Coordinate = new Coordinate(x, y);

            XVelocity = int.Parse(parts[3]);
            YVelocity = int.Parse(parts[4]);
        }


        public Coordinate Coordinate { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }

        public void Move (int steps)
        {
            var x = XVelocity + (Coordinate.X * steps);
            var y = YVelocity + (Coordinate.Y * steps);
            Coordinate = new Coordinate(x, y);
        }
    }
}
