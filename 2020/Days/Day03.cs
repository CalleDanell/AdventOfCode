using System.Collections.Generic;
using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day03 : IDay
    {
        public const int GridWidth = 31; 

        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var enumerable = input.ToList();
            var resultPartOne = FindTrees(GetGrid(enumerable), 3, 1);
            var resultPartTwo = FindTotalTrees(enumerable);
            
            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static IEnumerable<Coordinate> GetGrid(IEnumerable<string> input)
        {
            var grid = input.SelectMany((line, index) =>
            {
                var list = new List<Coordinate>();
                for (var j = 0; j < line.Length; j++)
                {
                    var coordinate = new Coordinate(j, index, line[j].Equals('.'));
                    list.Add(coordinate);
                }

                return list;
            }).ToList();

            return grid;
        }

        private static long FindTotalTrees(IList<string> input)
        {
            var pathsToTest = new List<(int, int)>
            {
                (1, 1),
                (3, 1),
                (5, 1),
                (7, 1),
                (1, 2)
            };

            long result = 1;
            foreach (var (x, y) in pathsToTest)
            {
                result *= FindTrees(GetGrid(input), x, y);
            }

            return result;
        }

        private static int FindTrees(IEnumerable<Coordinate> coordinates, int xPath, int yPath)
        {
            var trees = 0;
            var enumerable = coordinates.ToList();
            var current = enumerable.First();
            var end = enumerable.Last().Y;

            while (current?.Y <= end)
            {
                var nextX = current.X + xPath;
                var nextY = current.Y + yPath;
                current = enumerable.SingleOrDefault(cord => cord.X == nextX && cord.Y == nextY);

                if (current == null)
                {
                    // Expand grid x-axis
                    enumerable.ForEach(c => c.X += GridWidth);
                    current = enumerable.SingleOrDefault(cord => cord.X == nextX && cord.Y == nextY);
                }

                if (current != null && !current.Open)
                {
                    trees++;
                }
            }

            return trees;
        }
    }

    public class Coordinate
    {
        public Coordinate(int x, int y, bool open)
        {
            X = x;
            Y = y;
            Open = open;
        }

        public int X { get; set; }

        public int Y { get; }

        public bool Open { get; }
    }
}