using Common;
using Common.Coordinates;

namespace _2024.Days
{
    public class Day08 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var map = new Dictionary<Coordinate, string>();
            for (var y = 0; y < input.Count(); y++)
            {
                var row = input.ElementAt(y);
                for (var x = 0; x < row.Length; x++)
                {
                    map.Add(new Coordinate(x, y), row[x].ToString());
                }
            }

            var partOneSet = new HashSet<Coordinate>();
            var partTwoSet = new HashSet<Coordinate>();
            var antennas = map.Where(x => !x.Value.Equals(".")).GroupBy(x => x.Value);
            var mapCoordinates = map.Select(x => x.Key).ToHashSet();

            foreach (var antennaGroup in antennas)
            {
                var index = 0;
                while (index < antennaGroup.Count())
                {
                    var gorderedGroup = antennaGroup.OrderByDescending(x => x.Key.Y).ThenByDescending(x => x.Key.X).ToList();
                    var current = gorderedGroup.ElementAt(index);

                    for (var i = index + 1; i < gorderedGroup.Count; i++)
                    {
                        var next = gorderedGroup.ElementAt(i);
                        var nodes = FindAnitNodes(current.Key, next.Key, mapCoordinates, false);
                        var nodesTwo = FindAnitNodes(current.Key, next.Key, mapCoordinates, true);

                        nodes.Take(2).ToList().ForEach(x => partOneSet.Add(x));
                        nodesTwo.ForEach(x => partTwoSet.Add(x));
                    }
                    index++;
                }
            }

            var partOne = partOneSet.Count;
            var partTwo = partTwoSet.Count;

            Print(map, partOneSet);
            Print(map, partTwoSet);

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static List<Coordinate> FindAnitNodes(Coordinate c1, Coordinate c2, HashSet<Coordinate> map, bool findAll)
        {
            var collection = new List<Coordinate>();

            var dx = (c2.X - c1.X);
            var dy = (c2.Y - c1.Y);

            Coordinate first, second;
            first = new Coordinate(c1.X - dx, c1.Y - dy);
            second = new Coordinate(c2.X + dx, c2.Y + dy);

            if(!findAll)
            {
                AddIfInbound(first, map, collection);
                AddIfInbound(second, map, collection);

            } else
            {
                collection.Add(c1);
                collection.Add(c2);
                while (map.Contains(first))
                {
                    AddIfInbound(first, map, collection);
                    first = new Coordinate(first.X - dx, first.Y - dy);
                }

                while (map.Contains(second))
                {
                    AddIfInbound(second, map, collection);
                    second = new Coordinate(second.X + dx, second.Y + dy);
                }
            }

            return collection;
        }

        private static void AddIfInbound(Coordinate coordinate, HashSet<Coordinate> map, List<Coordinate> collection) 
        {
            if (map.Contains(coordinate))
            {
                collection.Add(coordinate);
            }
        }

        public static void Print(Dictionary<Coordinate, string> coords, HashSet<Coordinate> set)
        {
            Console.WriteLine();

            var xMin = coords.Min(x => x.Key.X);
            var yMin = coords.Min(x => x.Key.Y);
            var xMax = coords.Max(x => x.Key.X);
            var yMax = coords.Max(x => x.Key.Y);

            for (var y = yMin; y <= yMax; y++)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    var current = new Coordinate(x, y);
                    if (set.Contains(current))
                    {
                        Console.Write("#");
                    }
                    else
                    {
                        Console.Write(coords[current]);
                    }
                }
                Console.WriteLine();
            }
        }
    }
}