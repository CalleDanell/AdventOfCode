using Common;

namespace _2022.Days
{
    public class Day14 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var stones = new HashSet<Coord>();
            foreach (var row in input)
            {
                var parts = row.Split("->");
                for (var i = 0; i < parts.Length - 1; i++)
                {
                    var startCoord = new Coord(parts[i]);
                    var endCoord = new Coord(parts[i + 1]);

                    if (startCoord.Column == endCoord.Column)
                    {
                        var start = startCoord.Row > endCoord.Row ? endCoord.Row : startCoord.Row;
                        var end = startCoord.Row <= endCoord.Row ? endCoord.Row : startCoord.Row;
                        Enumerable.Range(start, end - start + 1).ToList().ForEach(x => stones.Add(new Coord(startCoord.Column, x) { Blocked = true, Stone = true }));
                    }
                    else
                    {
                        var start = startCoord.Column > endCoord.Column ? endCoord.Column : startCoord.Column;
                        var end = startCoord.Column <= endCoord.Column ? endCoord.Column : startCoord.Column;
                        Enumerable.Range(start, end - start + 1).ToList().ForEach(x => stones.Add(new Coord(x, startCoord.Row) { Blocked = true, Stone = true }));
                    }
                }
            }

            var minRow = -1;
            var minCol = stones.Min(x => x.Column);

            var maxRow = stones.Max(x => x.Row) + 1;
            var maxCol = stones.Max(x => x.Column);

            var infiniteMap = GenerateMap(stones, minRow, minCol, maxRow, maxCol, false);
            var mapWithFloor = GenerateMap(stones, minRow, minCol - 200, maxRow, maxCol + 200, true);


            int resultPartOne = PourSand(new Coord(0, 500), infiniteMap, minRow, minCol, maxRow, maxCol).Count(x => x.Sand);
            int resultPartTwo = PourSand(new Coord(0, 500), mapWithFloor, minRow, minCol - 200, maxRow, maxCol + 200).Count(x => x.Sand) + 1;

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private HashSet<Coord> GenerateMap(HashSet<Coord> map, int minRow, int minCol, int maxRow, int maxCol, bool withFloor)
        {
            var newMap = new HashSet<Coord>(map);
            for (var i = minRow; i <= maxRow + 1; i++)
            {
                for (var j = minCol; j <= maxCol; j++)
                {
                    var blocked = false;
                    if (i == maxRow + 1 && withFloor)
                    {
                        blocked = true;
                    }
                    newMap.Add(new Coord(i, j) { Blocked = blocked, Stone = blocked });
                }
            }

            return newMap;
        }

        private HashSet<Coord> PourSand(Coord start, HashSet<Coord> startMap, int minRow, int minCol, int maxRow, int maxCol)
        {
            var map = new HashSet<Coord>(startMap);
            var current = start;

            while(current != null)
            {
                var coord = new Coord(current.Row + 1, current.Column);
                var belowExist = map.TryGetValue(coord, out var below);
                if (below != null && !below.Blocked)
                {
                    current = below;
                    continue;
                }

                var coordLeft = new Coord(current.Row + 1, current.Column - 1);
                var belowLeftExist = map.TryGetValue(coordLeft, out var belowLeft);
                if (belowLeft != null && !belowLeft.Blocked)
                {
                    current = belowLeft;
                    continue;
                }

                var coordRight = new Coord(current.Row + 1, current.Column + 1);
                var belowRightExist = map.TryGetValue(coordRight, out var belowRight);
                if (belowRight != null && !belowRight.Blocked)
                {
                    current = belowRight;
                    continue;
                }

                if (!belowRightExist && !belowLeftExist && !belowExist)
                {
                    return map;
                }

                current.Blocked = true;
                current.Sand = true;
                current = start;
           

                if (start.Blocked)
                {
                    current.Blocked = true;
                    current.Sand = true;
                    //PrintMap(minRow, minCol, maxRow, maxCol, map); Console.WriteLine();
                    return map;
                }
            }

            return map;
        }

        private void PrintMap(int minRow, int minCol, int maxRow, int maxCol, HashSet<Coord> coords)
        {
            for (var i = minRow; i <= maxRow; i++)
            {
                for (var j = minCol; j <= maxCol; j++)
                {
                    var c = coords.First(x => x.Row == i && x.Column == j);
                    if (c.Stone)
                    {
                        Console.Write("#");
                    }
                    if (c.Sand)
                    {
                        Console.Write("o");
                    }
                    else
                    {
                        Console.Write(".");
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class Coord
    {
        public Coord(string input)
        {
            var parts = input.Split(',');
            Row = int.Parse(parts[0]);
            Column = int.Parse(parts[1]);
        }

        public Coord(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Blocked { get; set; }
        public bool Stone { get; set; }
        public bool Sand { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public override bool Equals(object? obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var c = (Coord)obj;
            return Row.Equals(c.Row) && Column.Equals(c.Column);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(Row, Column).GetHashCode();
        }
    }
}