using Common;
using Common.Coordinates;

namespace _2023.Days
{
    public class Day10 : IDay
    {
        public enum Direction
        {
            North,
            South,
            West,
            East,
            Stop
        }

        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day10));

            var coordiantes = Utils.GenerateCoordinates(input);

            var currentPosition = coordiantes.First(x => x.Value == 'S').Key;
            var currentDirection = Direction.East;
            var pipePath = new Dictionary<Coordinate, char>();

            while (true)
            {
                currentPosition = Move(coordiantes, currentPosition, currentDirection);
                pipePath.Add(currentPosition, coordiantes[currentPosition]);

                currentDirection = ChangeDirection(coordiantes[currentPosition], currentDirection);

                if (coordiantes[currentPosition].Equals('S'))
                    break;
            }

            var grid = new Dictionary<Coordinate, char>();
            foreach (var c in coordiantes.Keys)
            {
                if (pipePath.ContainsKey(c))
                {
                    grid[c] = '#';
                }
                else
                {
                    grid[c] = '.';
                }
            }

            //Utils.Print(grid);

            var tilesToTest = grid.Where(x => x.Value.Equals('.')).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

            var enclosed = new HashSet<Coordinate>();
            foreach(var tile in tilesToTest.Keys)
            {
                if (!CanEscape(tile, grid))
                    enclosed.Add(tile);
            }

            return (nameof(Day10), (pipePath.Count() / 2).ToString(), enclosed.Count().ToString());
        }

        private static bool CanEscape(Coordinate start, Dictionary<Coordinate, char> grid)
        {
            var visited = new HashSet<Coordinate>();
            return Explore(start, grid, visited);
        }

        private static bool Explore(Coordinate current, Dictionary<Coordinate, char> grid, HashSet<Coordinate> visited)
        {
            if (!grid.ContainsKey(current))
            {
                return true;
            }

            if (grid[current] == '#' || visited.Contains(current))
            {
                return false;
            }

            visited.Add(current);

            if (Explore(current.GetEast(), grid, visited)) return true;
            if (Explore(current.GetSouth(), grid, visited)) return true;
            if (Explore(current.GetWest(), grid, visited)) return true;
            if (Explore(current.GetNorth(), grid, visited)) return true;

            return false;
        }

        private static Coordinate Move(Dictionary<Coordinate, char> coordinates, Coordinate current, Direction currentDirection)
        {
            switch(currentDirection)
            {
                case Direction.North:
                    return coordinates.Keys.First(x => x.X == current.X && x.Y == current.Y - 1);
                case Direction.South:
                    return coordinates.Keys.First(x => x.X == current.X && x.Y == current.Y + 1);
                case Direction.West:
                    return coordinates.Keys.First(x => x.X == current.X - 1 && x.Y == current.Y);
                case Direction.East:
                    return coordinates.Keys.First(x => x.X == current.X + 1 && x.Y == current.Y);
            }

            return current;
        }

        private static Direction ChangeDirection(char currentValue, Direction currentDirection)
        {
            switch (currentValue)
            {
                case '|':
                    if(currentDirection.Equals(Direction.North))
                    {
                        return Direction.North;
                    }
                    return Direction.South;
                
                case '-':
                    if (currentDirection.Equals(Direction.West))
                    {
                        return Direction.West;
                    }
                    return Direction.East;

                case 'L':
                    if (currentDirection.Equals(Direction.South))
                    {
                        return Direction.East;
                    }
                    return Direction.North;

                case 'J':
                    if (currentDirection.Equals(Direction.South))
                    {
                        return Direction.West;
                    }
                    return Direction.North;

                case '7':
                    if (currentDirection.Equals(Direction.North))
                    {
                        return Direction.West;
                    }
                    return Direction.South;

                case 'F':
                    if (currentDirection.Equals(Direction.North))
                    {
                        return Direction.East;
                    }
                    return Direction.South;
            }

            return currentDirection;
        }
    }
}

//| is a vertical pipe connecting north and south.
//- is a horizontal pipe connecting east and west.
//L is a 90-degree bend connecting north and east.
//J is a 90-degree bend connecting north and west.
//7 is a 90-degree bend connecting south and west.
//F is a 90-degree bend connecting south and east.
//. is ground; there is no pipe in this tile.
//S is the starting position of the animal; there is a pipe on this tile, but your sketch doesn't show what shape the pipe has.