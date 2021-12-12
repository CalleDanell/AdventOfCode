using Common;
using Common.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day11 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day11));

            var grid = BuildGrid(input);

            var step = 0;
            var numberOfFlashesAfter100Steps = 0;

            while (true)
            {
                foreach (var squid in grid.Keys.ToList())
                {
                    grid[squid]++;
                }

                var readyToFlash = new Stack<Coordinate>(grid.Where(x => x.Value >= 10).Select(x => x.Key));
                var hasFlashed = new HashSet<Coordinate>();
                var flashesPerStep = 0;

                while (readyToFlash.Any())
                {
                    var current = readyToFlash.Pop();
                    var adjacent = GetAdjacent(current);

                    if (hasFlashed.Contains(current))
                    {
                        continue;
                    }

                    grid[current] = 0;

                    if(step < 100)
                        numberOfFlashesAfter100Steps++;

                    flashesPerStep++;
                    hasFlashed.Add(current);

                    foreach (var adjacentCoordinate in adjacent)
                    {

                        if (grid.ContainsKey(adjacentCoordinate) && !hasFlashed.Contains(adjacentCoordinate))
                        {
                            grid[adjacentCoordinate]++;

                            if (grid[adjacentCoordinate] >= 10 && !readyToFlash.Contains(adjacentCoordinate))
                            {
                                readyToFlash.Push(adjacentCoordinate);
                            }
                        }
                    }
                }

                if (flashesPerStep == 100)
                {
                    break;
                }

                step++;
            }

            var resultPartOne = numberOfFlashesAfter100Steps;
            var resultPartTwo = step + 1;

            return (nameof(Day09), resultPartOne.ToString(), resultPartTwo.ToString());
        }


        private static List<Coordinate> GetAdjacent(Coordinate coordinate)
        {
            var up = new Coordinate(coordinate.X, coordinate.Y - 1);
            var down = new Coordinate(coordinate.X, coordinate.Y + 1);
            var left = new Coordinate(coordinate.X - 1, coordinate.Y);
            var right = new Coordinate(coordinate.X + 1, coordinate.Y);

            var upLeft = new Coordinate(coordinate.X - 1, coordinate.Y - 1);
            var upRight = new Coordinate(coordinate.X + 1, coordinate.Y - 1);
            var downLeft = new Coordinate(coordinate.X - 1, coordinate.Y + 1);
            var downRight = new Coordinate(coordinate.X + 1, coordinate.Y + 1);

            return new List<Coordinate> { up, down, left, right, upLeft, upRight, downLeft, downRight };
        }

        private static Dictionary<Coordinate, int> BuildGrid(IEnumerable<string> input)
        {
            var grid = new Dictionary<Coordinate, int>();
            var enumerable = input.ToList();
            for (var y = 0; y < enumerable.Count; y++)
            {
                var line = enumerable[y].ToCharArray();
                for (var x = 0; x < line.Length; x++)
                {
                    grid.TryAdd(new Coordinate(x, y), line[x] - '0');
                }
            }

            return grid;
        }
    }
}