using Common;
using Common.Coordinates;

namespace _2024.Days
{
    public class Day06 : IDay
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

            var start = map.First(x => x.Value.Equals("^")).Key;
            (bool loop, HashSet<Coordinate> visited) = FollowThePath(map, start, true);

            var loops = 0;
            foreach (var coord in visited)
            {
                map[coord] = "#";
                (bool isLoop, _) = FollowThePath(map, start, false);
                map[coord] = ".";

                if (isLoop)
                {
                    loops++;
                }
            }

            var partOne = visited.Count;
            var partTwo = loops;

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static (bool, HashSet<Coordinate> visited) FollowThePath(Dictionary<Coordinate,string> map, Coordinate current, bool returnVisited)
        {
            var directions = new Queue<string>(new List<string> { "up", "right", "down", "left" });
            var direction = directions.Dequeue();
            var visited = new HashSet<(Coordinate, string)>() { (current, direction) };

            var loop = false;
            while (!loop)
            {
                (int dx, int dy) = GetDirectionCoords(direction);
                var next = new Coordinate(current.X + dx, current.Y + dy);
                var inBounds = map.TryGetValue(next, out var value);

                if (!inBounds)
                    break;

                if (value.Equals("#"))
                {
                    directions.Enqueue(direction);
                    direction = directions.Dequeue();
                    continue;
                }

                current = next;
                if (!visited.Add((current, direction)))
                {
                    loop = true;
                }
            }

            return (loop, returnVisited ? visited.Select(x => x.Item1).ToHashSet() : new HashSet<Coordinate>());
        }

        private static (int,int) GetDirectionCoords(string direction)
        {
            var dy = 0;
            var dx = 0;
            switch (direction)
            {
                case "up":
                    dy = -1;
                    break;
                case "down":
                    dy = 1;
                    break;
                case "left":
                    dx = -1;
                    break;
                case "right":
                    dx = 1;
                    break;
            }

            return (dx, dy);
        }
    }
}