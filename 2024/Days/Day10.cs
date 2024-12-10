using Common;
using Common.Coordinates;

namespace _2024.Days
{
    public class Day10 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var map = new Dictionary<Coordinate, int>();
            for (var y = 0; y < input.Count(); y++)
            {
                var row = input.ElementAt(y);
                for (var x = 0; x < row.Length; x++)
                {
                    map.Add(new Coordinate(x, y), int.Parse(row[x].ToString()));
                }
            }

            var startingPositions = map.Where(x => x.Value.Equals(0)).ToList();

            var uniquePaths = 0;
            var numberOfEndNodes = 0;
            foreach (var start in startingPositions)
            {
                var paths = new List<Coordinate>();
                
                FindPaths(map, start.Key, paths);
                var endNodes = new HashSet<Coordinate>(paths);

                uniquePaths += paths.Count;
                numberOfEndNodes += endNodes.Count;
            }

            var partOne = numberOfEndNodes;
            var partTwo = uniquePaths;

            return (day, partOne.ToString(), partTwo.ToString());
        }


        private static void FindPaths(Dictionary<Coordinate, int> map, Coordinate current, List<Coordinate> paths)
        {
            var currentValue = map[current];
            if (currentValue == 9)
            {
                paths.Add(current);
                return;
            }

            var north = map.TryGetValue(current.GetNorth(), out var northValue) ? northValue : -1;
            var south = map.TryGetValue(current.GetSouth(), out var southValue) ? southValue : -1;
            var west = map.TryGetValue(current.GetWest(), out var westValue) ? westValue : -1;
            var east = map.TryGetValue(current.GetEast(), out var eastValue) ? eastValue : -1;


            if (north != -1 && (north - currentValue == 1))
            {
                FindPaths(map, current.GetNorth(), paths);
            }

            if (south != -1 && (south - currentValue == 1))
            {
                FindPaths(map, current.GetSouth(), paths);
            }

            if (west != -1 && (west - currentValue == 1))
            {
                FindPaths(map, current.GetWest(), paths);
            }

            if (east != -1 && (east - currentValue == 1))
            {
                FindPaths(map, current.GetEast(), paths);
            }
        }
    }
}