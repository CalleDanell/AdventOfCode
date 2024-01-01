using Common;
using Common.Coordinates;

namespace _2023.Days
{
    public class Day16 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day16));
            var coordiantes = new Dictionary<Coordinate, char>();

            for (var i = 0; i < input.Count(); i++)
            {
                for (var j = 0; j < input.ElementAt(i).Length; j++)
                {
                    var value = input.ElementAt(i)[j];
                    coordiantes.Add(new Coordinate(j, i), value);
                }
            }

            //part one
            var startDirection = 4;
            var startCoord = new Coordinate(-1, 0);
            var visited = GetEnergizedTiles(coordiantes, startCoord, startDirection);

            //part two
            var xMax = coordiantes.Max(x => x.Key.X);
            var yMax = coordiantes.Max(x => x.Key.Y);

            var startWest = coordiantes.Where(x => x.Key.X == 0);
            var startNorth = coordiantes.Where(x => x.Key.Y == 0);
            var startEast = coordiantes.Where(x => x.Key.X == xMax);
            var startSouth = coordiantes.Where(x => x.Key.Y == yMax);

            var best = 0;
            foreach(var start in startWest)
            {
                var energized = GetEnergizedTiles(coordiantes, new Coordinate(start.Key.X -1, start.Key.Y), 4).Count;
                if (energized >= best) best = energized;
            }

            foreach (var start in startNorth)
            {
                var energized = GetEnergizedTiles(coordiantes, new Coordinate(start.Key.X, start.Key.Y - 1), 3).Count;
                if (energized >= best) best = energized;
            }

            foreach (var start in startSouth)
            {
                var energized = GetEnergizedTiles(coordiantes, new Coordinate(start.Key.X, yMax + 1), 1).Count;
                if (energized >= best) best = energized;
            }

            foreach (var start in startEast)
            {
                var energized = GetEnergizedTiles(coordiantes, new Coordinate(xMax + 1, start.Key.Y), 2).Count;
                if (energized >= best) best = energized;
            }


            return (nameof(Day16), visited.Count.ToString(), best.ToString());
        }

        private static HashSet<Coordinate> GetEnergizedTiles(Dictionary<Coordinate, char> coordiantes, Coordinate start, int startDirection)
        {
            var visited = new HashSet<Coordinate>();
            var rays = new Queue<(Coordinate, int)>();
            var loop = new HashSet<(int, int, int)>();
            rays.Enqueue((start, startDirection));

            while (rays.TryDequeue(out var ray))
            {
                var currentDirection = ray.Item2;
                var next = ray.Item1.GetNextCoordinate(currentDirection);

                while (coordiantes.ContainsKey(next))
                {
                    visited.Add(next);
                    switch (coordiantes[next])
                    {
                        case '.':
                            next = next.GetNextCoordinate(currentDirection);
                            break;
                        case '|':
                            if (currentDirection == 2 || currentDirection == 4)
                            {
                                if (loop.Add((next.X, next.Y, currentDirection)))
                                {
                                    rays.Enqueue((next, 1));
                                    rays.Enqueue((next, 3));
                                }

                                next = new Coordinate(-10, -10);
                            }
                            else
                            {
                                next = next.GetNextCoordinate(currentDirection);
                            }
                            break;
                        case '-':
                            if (currentDirection == 1 || currentDirection == 3)
                            {
                                if (loop.Add((next.X, next.Y, currentDirection)))
                                {
                                    rays.Enqueue((next, 2));
                                    rays.Enqueue((next, 4));
                                }

                                next = new Coordinate(-10, -10);
                            }
                            else
                            {
                                next = next.GetNextCoordinate(currentDirection);
                            }
                            break;
                        case '/':
                            if (currentDirection == 1) { currentDirection = 4; next = next.GetNextCoordinate(currentDirection); break; }
                            if (currentDirection == 2) { currentDirection = 3; next = next.GetNextCoordinate(currentDirection); break; }
                            if (currentDirection == 3) { currentDirection = 2; next = next.GetNextCoordinate(currentDirection); break; }
                            if (currentDirection == 4) { currentDirection = 1; next = next.GetNextCoordinate(currentDirection); break; }
                            break;
                        case '\\':
                            if (currentDirection == 1) { currentDirection = 2; next = next.GetNextCoordinate(currentDirection); break; }
                            if (currentDirection == 2) { currentDirection = 1; next = next.GetNextCoordinate(currentDirection); break; }
                            if (currentDirection == 3) { currentDirection = 4; next = next.GetNextCoordinate(currentDirection); break; }
                            if (currentDirection == 4) { currentDirection = 3; next = next.GetNextCoordinate(currentDirection); break; }
                            break;
                    }
                }
            }

            return visited;
        }

        private void Print(HashSet<Coordinate> visited, Dictionary<Coordinate, char> coords)
        {
            Console.WriteLine();
            var xMax = coords.Max(x => x.Key.X);
            var yMax = coords.Max(x => x.Key.Y);
            for (var i = 0; i <= xMax; i++)
            {
                for (var j = 0; j <= yMax; j++)
                {
                    var current = new Coordinate(j, i);
                    if (visited.Contains(current))
                    {
                        Console.Write('#');
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
