using Common;
using System.Globalization;
using System.IO;

namespace _2022.Days
{
    public class Day12 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var map = input.Select((x, i) =>
            {
                var array = x.ToCharArray();
                return array.Select((x, j) => new KeyValuePair<Square, int>(new Square(i, j), x - '0'));
            }).SelectMany(x => x).ToDictionary(x => x.Key, y => y.Value);

            var endKey = map.First(x => x.Value == 21).Key;
            var startKey = map.First(x => x.Value == 35).Key;
            map[endKey] = 'z' - '0';
            map[startKey] = 'a' - '0';

            var shortestPath = FindShortestPath(map, endKey, startKey);

            var starts = map.Where(x => x.Value == 'a' - '0');
            var possiblePaths = new List<List<Square>>();
            foreach(var start in starts)
            {
                possiblePaths.Add(FindShortestPath(map, endKey, start.Key));
            }

            int resultPartOne = shortestPath.Count - 1;
            int resultPartTwo = possiblePaths.Where(x => x.Count > 0).OrderBy(x => x.Count).First().Count - 1;

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private List<Square> FindShortestPath(Dictionary<Square, int> map, Square endSquare, Square startSquare)
        {
            var paths = new Queue<List<Square>>();
            paths.Enqueue(new List<Square>() { startSquare });
            var explored = new HashSet<Square>();
            while (paths.Any())
            {
                var path = paths.Dequeue();
                var current = path.Last();
                var neighbours = current.GetValidNeighbours(map);

                if (current.Equals(endSquare))
                {
                    return path;
                }

                foreach (var neighbour in neighbours)
                {
                    if (!explored.Contains(neighbour))
                    {
                        var newPath = path.ToList();
                        newPath.Add(neighbour);
                        explored.Add(neighbour);
                        paths.Enqueue(newPath);
                    }
                }
            }

            return new List<Square>();
        }
    }

    public class Square
    {
        public Square(int column, int row)
        {
            Row = row;
            Column = column;
        }

        public int Row { get; }
        public int Column { get; }
        public IList<Square> GetValidNeighbours(Dictionary<Square, int> map)
        {
            var neighbours = new List<Square>();
            map.TryGetValue(new Square(Column, Row), out int height);

            if (map.TryGetValue(new Square(Column, Row + 1), out int up) && height + 1 >= up) neighbours.Add((new Square(Column, Row + 1)));
            if (map.TryGetValue(new Square(Column, Row - 1), out int down) && height + 1 >= down) neighbours.Add((new Square(Column, Row - 1)));
            if (map.TryGetValue(new Square(Column + 1, Row), out int right) && height + 1 >= right) neighbours.Add((new Square(Column + 1, Row)));
            if (map.TryGetValue(new Square(Column - 1, Row), out int left) && height + 1 >= left) neighbours.Add((new Square(Column - 1, Row)));

            return neighbours;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var c = (Square)obj;
            return Row.Equals(c.Row) && Column.Equals(c.Column);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Row, Column).GetHashCode();
        }
    }
}