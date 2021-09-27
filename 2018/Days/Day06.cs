using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day06 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day06));

            var pointsOfInterest = input.Select((x,i) =>
            {
                var parts = x.Split(',');
                return new Coordinate(int.Parse(parts[0]), int.Parse(parts[1].Trim())) { Id = i };
            }).ToList();

            var coordinateSystem = GenerateCoordinateSystem(pointsOfInterest);

            var areasBySize = coordinateSystem
                .GroupBy(x => x.Value)
                .Where(x => !x.Any(y => y.Key.IsInfinite))
                .Select(x => x.Count())
                .OrderByDescending(x => x);

            var resultPartOne = areasBySize.First().ToString();
            var resultPartTwo = coordinateSystem.Count(x => x.Key.IsCloseToAllPoints).ToString();

            return (nameof(Day06), resultPartOne, resultPartTwo);
        }

        private Dictionary<Coordinate, int> GenerateCoordinateSystem(List<Coordinate> points)
        {
            var coordinateSystem = points.ToDictionary(point => point, point => point.Id);

            var maxX = points.Max(x => x.X);
            var maxY = points.Max(x => x.Y);

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    var coord = new Coordinate(x, y);

                    var orderedPoints = points.Select(coordinate => new 
                    { 
                        Distance = coordinate.Distance(coord), coordinate.Id
                    }).OrderBy(arg => arg.Distance).ToList();

                    var totalDistance = orderedPoints.Sum(arg => arg.Distance);

                    if (totalDistance < 10000)
                    {
                        coord.IsCloseToAllPoints = true;
                    }
                    var value = orderedPoints.First().Id;

                    if (coord.X == 0 || coord.X == maxX || coord.Y == maxY || coord.Y == 0)
                    {
                        coord.IsInfinite = true;
                    }

                    if (orderedPoints.ElementAt(0).Distance == orderedPoints.ElementAt(1).Distance)
                        value = -1;

                    if (!coordinateSystem.TryAdd(coord, value))
                    {
                        // Hack in order to update the coordinate prop. This should be fixed :) 
                        coordinateSystem.Remove(coord);
                        coordinateSystem.Add(coord, value);
                    }
                }
            }

            return coordinateSystem;
        }

        public class Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }
            public int Distance(Coordinate point)
            {
                return Math.Abs(X - point.X) + Math.Abs(Y - point.Y);
            }

            public bool IsInfinite { get; set; }

            public bool IsCloseToAllPoints { get; set; }

            public int Id { get; set; }

            public int X { get; }

            public int Y { get; }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;
                var c = (Coordinate)obj;
                return (X == c.X) && (Y == c.Y);
            }

            public override int GetHashCode()
            {
                return Tuple.Create(X, Y).GetHashCode();
            }
        }
    }
}
