using Common;
using Common.Coordinates;

namespace _2023.Days
{
    public class Day11 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day11));
            var universe = input.Select(x => x.ToCharArray());

            var coordiantes = GenerateGrid(universe.ToList());
            var expanded = ExpandUniverse(coordiantes, 1000000 - 1);

            long sumOne = GetDistanceBetweenAllGalaxies(new Queue<Coordinate>(expanded));

            return (nameof(Day11), sumOne.ToString(), 0.ToString());
        }

        private static long GetDistanceBetweenAllGalaxies(Queue<Coordinate> galax)
        {
            long sum = 0;
            while(galax.TryDequeue(out var g))
            {
                foreach (var neighbour in galax)
                {
                    var manhattanDistance = Math.Abs(g.X - neighbour.X) + Math.Abs(g.Y - neighbour.Y);
                    sum += manhattanDistance;
                }
            }

            return sum;
        }

        private static IEnumerable<Coordinate> ExpandUniverse(Dictionary<Coordinate, char> start, int factor)
        {
            var stars = start.Where(x => x.Value.Equals('#')).Select(x => x.Key);

            var maxX = start.Max(x => x.Key.X);
            var maxY = start.Max(x => x.Key.Y);

            for (var i = maxX; i >= 0; i--)
            {
                var currentLine = start.Where(x => x.Key.X == i);
                if (currentLine.All(x => x.Value.Equals('.')))
                {
                    stars.Where(x => x.X > i).ToList().ForEach(x => x.Move(factor, 0));
                }
            }

            for (var i = maxY; i >= 0; i--)
            {
                var currentLine = start.Where(x => x.Key.Y == i);
                if (currentLine.All(x => x.Value.Equals('.')))
                {
                    stars.Where(x => x.Y > i).ToList().ForEach(x => x.Move(0, factor));
                }
            }

            return stars;
        }


        private static Dictionary<Coordinate, char> GenerateGrid(List<char[]> universe)
        {
            var coordiantes = new Dictionary<Coordinate, char>();

            for (var i = 0; i < universe.Count(); i++)
            {
                for (var j = 0; j < universe.ElementAt(i).Count(); j++)
                {
                    coordiantes.Add(new Coordinate(j, i), universe.ElementAt(i)[j]);
                }
            }

            return coordiantes;
        }
    }
}
