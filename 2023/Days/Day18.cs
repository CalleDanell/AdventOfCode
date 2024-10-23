using Common;
using Common.Coordinates;

namespace _2023.Days
{
    public class Day18 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day18));
            var digIns = input.Select(x => new DigInstructions(x));
            var current = new Coordinate(0, 0);
            var digSite = new Dictionary<Coordinate, char>() { { current, '#' } };
            foreach (var digs in digIns) 
            {
                var direction = 0;
                switch(digs.Direction)
                {
                    case "R":
                        direction = 4;
                        break;
                    case "L":
                        direction = 2;
                        break;
                    case "U":
                        direction = 1;
                        break;
                    case "D":
                        direction = 3;
                        break;
                }

                for(var i = 0; i < digs.Distance; i++) 
                {
                    current = current.GetNextCoordinate(direction);
                    digSite.TryAdd(current, '#');
                }
            }

            var xMax = digSite.Max(x => x.Key.X);
            var yMax = digSite.Max(x => x.Key.Y);

            var xMin = digSite.Min(x => x.Key.X);
            var yMin = digSite.Min(x => x.Key.Y);

            for (var y = yMin; y <= yMax; y++)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    var value = '.';
                    //if (y == 0 || y == yMax || x == 0 || x == xMax) value = '#';

                    digSite.TryAdd(new Coordinate(x, y), value);
                }
            }

            //Utils.Print(digSite);

            foreach (var empty in digSite.Where(x => x.Value.Equals('.')))
            {
                bool inBoundsNorth = CheckInBounds(digSite, empty.Key, 1);
                bool inBoundsSouth = CheckInBounds(digSite, empty.Key, 3);
                bool inBoundsEast = CheckInBounds(digSite, empty.Key, 4);
                bool inBoundsWest = CheckInBounds(digSite, empty.Key, 2);

                if(inBoundsNorth && 
                    inBoundsEast &&
                    inBoundsSouth && 
                    inBoundsWest) 
                {
                    digSite[empty.Key] = '#';
                }
            }

            Utils.Print(digSite);

            return (nameof(Day18), digSite.Count(x => x.Value.Equals('#')).ToString(), 0.ToString());
        }

        private static bool CheckInBounds(Dictionary<Coordinate, char> digSite, Coordinate current, int direction)
        {
            var inBounds = true;
            var path = new List<char>();

            switch (direction)
            {
                case 1:
                    while (digSite.ContainsKey(current))
                    {
                        path.Add(digSite[current]);
                        current = current.GetNorth();
                    }

                    if (path.All(x => x.Equals('.')))
                    {
                        inBounds = false;
                    }
                    break;
                case 2:
                    while (digSite.ContainsKey(current))
                    {
                        path.Add(digSite[current]);
                        current = current.GetWest();
                    }

                    if (path.All(x => x.Equals('.')))
                    {
                        inBounds = false;
                    }
                    break;
                case 3:
                    while (digSite.ContainsKey(current))
                    {
                        path.Add(digSite[current]);
                        current = current.GetSouth();
                    }

                    if (path.All(x => x.Equals('.')))
                    {
                        inBounds = false;
                    }
                    break;
                case 4:
                    while (digSite.ContainsKey(current))
                    {
                        path.Add(digSite[current]);
                        current = current.GetEast();
                    }

                    if (path.All(x => x.Equals('.')))
                    {
                        inBounds = false;
                    }
                    break;
            }

            return inBounds;
        }
    }

    public class DigInstructions
    {
        public DigInstructions(string input)
        {
            var parts = input.Split(new[] { ' ', '(' }, StringSplitOptions.RemoveEmptyEntries);
            Direction = parts[0];
            Distance = int.Parse(parts[1]);
            Code = parts[2].Replace(")", string.Empty);
        }

        public string Direction { get; private set; }
        public int Distance { get; private set; }
        public string Code { get; private set; } 
    }
}
