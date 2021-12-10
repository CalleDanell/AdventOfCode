using System.Linq;
using Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using Common.Coordinates;

namespace _2021.Days
{
    public class Day09 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day09));

            var system = BuildCoordinateSystem(input);
            var lowPoints = system.Where(x => IsLowPoint(system, x.Key)).ToDictionary(x => x.Key, x => x.Value);
            var riskLevels = lowPoints.Select(x => x.Value).Sum(x => x + 1);

            var visited = new HashSet<Coordinate>();

            var basins = lowPoints.Select(lowPoint => GetNumberOfAdjacent(system, lowPoint.Key, visited)).ToList();
            var basinsBySize = basins.OrderByDescending(x => x).ToList();

            var resultPartOne = riskLevels;
            var resultPartTwo = basinsBySize.ElementAt(0) * basinsBySize.ElementAt(1) * basinsBySize.ElementAt(2);

            return (nameof(Day09), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        public static Dictionary<Coordinate, int> BuildCoordinateSystem(IEnumerable<string> input)
        {
            var enumerable = input.ToList(); 
            var map = new Dictionary<Coordinate, int>();
            
            for (var i = 0; i < enumerable.Count; i++)
            {
                var charsPerRow = enumerable[i].ToCharArray();
                for (var j = 0; j < charsPerRow.Length; j++)
                {
                    var value = charsPerRow[j] - '0';
                    map.TryAdd(new Coordinate(i, j), value);
                }
            }

            return map;
        }

        private static int GetNumberOfAdjacent(Dictionary<Coordinate, int> system, Coordinate coordinate, HashSet<Coordinate> visited)
        {
            var basinSize = 0;
            var toBeChecked = new Queue<Coordinate>();
            toBeChecked.Enqueue(coordinate);

            while (toBeChecked.Any())
            {
                var current = toBeChecked.Dequeue();
                var up = new Coordinate(current.X, current.Y - 1);
                var down = new Coordinate(current.X, current.Y + 1);
                var left = new Coordinate(current.X - 1, current.Y);
                var right = new Coordinate(current.X + 1, current.Y);

                var coordinates = new List<Coordinate> { up, down, left, right, current };

                foreach (var adjacentCoordinate in coordinates)
                {
                    if (visited.Contains(adjacentCoordinate))
                    {
                        continue;
                    }

                    if (system.TryGetValue(adjacentCoordinate, out var value))
                    {
                        if (value != 9)
                        {
                            basinSize++;
                            toBeChecked.Enqueue(adjacentCoordinate);
                        }
                    }

                    visited.Add(adjacentCoordinate);
                }
            }

            return basinSize;
        }

        private static bool IsLowPoint(IReadOnlyDictionary<Coordinate, int> system, Coordinate coordinate)
        {
            var up = new Coordinate(coordinate.X, coordinate.Y - 1);
            var down = new Coordinate(coordinate.X, coordinate.Y + 1);
            var left = new Coordinate(coordinate.X - 1, coordinate.Y);
            var right = new Coordinate(coordinate.X + 1, coordinate.Y);

            var upValue = system.TryGetValue(up, out var upv) ? upv : int.MaxValue;
            var downValue = system.TryGetValue(down, out var downv) ? downv : int.MaxValue;
            var leftValue = system.TryGetValue(left, out var leftv) ? leftv : int.MaxValue;
            var rightValue = system.TryGetValue(right, out var rightv) ? rightv : int.MaxValue;

            if (system[coordinate] < upValue &&
                system[coordinate] < downValue &&
                system[coordinate] < leftValue &&
                system[coordinate] < rightValue)
            {
                return true;
            }

            return false;
        }
    }
}