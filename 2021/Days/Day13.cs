using System;
using Common;
using Common.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day13 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day13));

            var enumerable = input.ToList();

            var foldInstructionsRaw = enumerable.Where(x => x.StartsWith("fold")).ToList();

            var foldInstructions = foldInstructionsRaw
                .Select(x =>
                {
                    var parts = x.Split('=');
                    var direction = parts[0].Last();
                    var distance = int.Parse(parts[1]);
                    return new FoldInstruction(direction, distance);
                })
                .ToList();

            var coordinates = enumerable.Except(foldInstructionsRaw)
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x =>
                {
                    var parts = x.Split(',');
                    var xCoordinate = int.Parse(parts[0]);
                    var yCoordinate = int.Parse(parts[1]);

                    return new Coordinate(xCoordinate, yCoordinate);
                });

            var grid = BuildPaperGrid(coordinates);

            var numberOfFolds = 0;
            var resultPartOne = 0;

            foreach (var instruction in foldInstructions)
            {
                if(numberOfFolds == 1)
                    resultPartOne = grid.Count(x => x.Value);

                if (instruction.Direction == 'x')
                {
                    grid = FoldLeft(grid, instruction.Distance);
                }
                else
                {
                    grid = FoldUp(grid, instruction.Distance);
                }

                numberOfFolds++;
            }
            
            DrawGrid(grid);
            var resultPartTwo = 1;

            return (nameof(Day13), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static Dictionary<Coordinate, bool> FoldUp(IDictionary<Coordinate, bool> grid, int y)
        {
            var up = grid.Where(x => x.Key.Y < y).ToDictionary(x => x.Key, x => x.Value);
            var down = grid.Where(x => x.Key.Y > y).ToDictionary(x => x.Key, x => x.Value);

            foreach (var coordinate in down)
            {
                var distanceFromFoldLine = coordinate.Key.Y - y;
                if(down[coordinate.Key])
                {
                    var newX = coordinate.Key.X;
                    var newY = coordinate.Key.Y - (2 * distanceFromFoldLine);
                    var foldKey = new Coordinate(newX, newY);
                    up[foldKey] = true;
                }
            }

            return up.ToDictionary(x => x.Key, x => x.Value);
        }

        private static Dictionary<Coordinate, bool> FoldLeft(IDictionary<Coordinate, bool> grid, int x)
        {
            var left = grid.Where(c => c.Key.X < x).ToDictionary(c => c.Key, c => c.Value);
            var right = grid.Where(c => c.Key.X > x).ToDictionary(c => c.Key, c => c.Value);

            foreach (var coordinate in right)
            {
                var distanceFromFoldLine = coordinate.Key.X - x;
                if (right[coordinate.Key])
                {
                    var newX = coordinate.Key.X - (2 * distanceFromFoldLine);
                    var newY = coordinate.Key.Y;
                    var foldKey = new Coordinate(newX, newY);
                    left[foldKey] = true;
                }
            }

            return left.ToDictionary(c => c.Key, c => c.Value);
        }


        // This part is slow, should not keep track of all the coordinates until all of the folding is completed. 
        private static Dictionary<Coordinate, bool> BuildPaperGrid(IEnumerable<Coordinate> markedCoordinates)
        {
            var grid = new Dictionary<Coordinate, bool>();
            var enumerable = markedCoordinates.ToList();
            var maxX = enumerable.Max(x => x.X);
            var maxY = enumerable.Max(x => x.Y);

            for (var y = 0; y <= maxY; y++)
            {
                for (var x = 0; x <= maxX; x++)
                {
                    var marked = false;
                    var coordinate = new Coordinate(x,y);
                    if (enumerable.Contains(coordinate))
                        marked = true;

                    grid.TryAdd(new Coordinate(x, y), marked);
                }
            }

            return grid;
        }

        private static void DrawGrid(Dictionary<Coordinate, bool> grid)
        {
            var maxX = grid.Max(x => x.Key.X);
            var maxY = grid.Max(x => x.Key.Y);

            for (var y = 0; y <= maxY; y++)
            {
                var row = string.Empty;
                for (var x = 0; x <= maxX; x++)
                {
                    var coordinate = new Coordinate(x, y);
                    grid.TryGetValue(coordinate, out var value);
                    if (value)
                    {
                        row += "#";
                    }
                    else
                    {
                        row += "-";
                    }
                }
            }
        }
    }

    public class FoldInstruction
    {
        public char Direction { get; }
        public int Distance { get; }

        public FoldInstruction(char direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }
    }
}