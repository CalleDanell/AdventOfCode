using Common;
using Common.Coordinates;

namespace _2024.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var wordMap = new Dictionary<Coordinate, string>();
            for(var y = 0; y < input.Count(); y++)
            {
                var row = input.ElementAt(y);
                for(var x = 0; x < row.Length; x++)
                {
                    wordMap.Add(new Coordinate(x,y), row[x].ToString());
                }
            }

            var xmasCount = 0;
            foreach(var x in wordMap.Where(x => x.Value.Equals("X")))
            {
                var key = x.Key;
                xmasCount += CheckDirectionForXMas(wordMap, -1, 0, key); // left
                xmasCount += CheckDirectionForXMas(wordMap, 1, 0, key); // right
                xmasCount += CheckDirectionForXMas(wordMap, 0, 1, key); // up
                xmasCount += CheckDirectionForXMas(wordMap, 0, -1, key); // down
                xmasCount += CheckDirectionForXMas(wordMap, -1, 1, key); // leftup
                xmasCount += CheckDirectionForXMas(wordMap, 1, 1, key); // rightup
                xmasCount += CheckDirectionForXMas(wordMap, -1, -1, key); // leftdown
                xmasCount += CheckDirectionForXMas(wordMap, 1, -1, key); // rightdown
            }

            var x_masCount = wordMap.Where(x => x.Value.Equals("A")).Sum(x => CheckForXDashMas(wordMap, x.Key));

            var partOne = xmasCount;
            var partTwo = x_masCount;

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static int CheckForXDashMas(Dictionary<Coordinate, string> wordMap, Coordinate start)
        {
            string one, two, three, four;
            one = wordMap.TryGetValue(new Coordinate(start.X - 1, start.Y + 1), out var first) ? first : string.Empty; //leftup
            two = wordMap.TryGetValue(new Coordinate(start.X + 1, start.Y - 1), out var second) ? second : string.Empty; //rightdown
            three = wordMap.TryGetValue(new Coordinate(start.X + 1, start.Y + 1), out var third) ? third : string.Empty; //rightup
            four = wordMap.TryGetValue(new Coordinate(start.X - 1, start.Y - 1), out var fourth) ? fourth : string.Empty; //leftdown

            var xDashmasOne = string.Join(string.Empty, one, wordMap[start], two);
            var xDashmasTwo = string.Join(string.Empty, three, wordMap[start], four);

            var statement = (xDashmasOne.Equals("MAS") || xDashmasOne.Equals("SAM")) && (xDashmasTwo.Equals("MAS") || xDashmasTwo.Equals("SAM")) ? 1 : 0;

            return statement;
        }

        private static int CheckDirectionForXMas(Dictionary<Coordinate, string> wordMap, int dx, int dy, Coordinate start)
        {
            string one, two, three;
            one = wordMap.TryGetValue(new Coordinate(start.X + (1 * dx), start.Y + (1 * dy)), out var first) ? first : string.Empty;
            two = wordMap.TryGetValue(new Coordinate(start.X + (2 * dx), start.Y + (2 * dy)), out var second) ? second : string.Empty;
            three = wordMap.TryGetValue(new Coordinate(start.X + (3 * dx), start.Y + (3 * dy)), out var third) ? third : string.Empty;

            var xmas = string.Join(string.Empty, wordMap[start], one, two, three);
            return (xmas.Equals("XMAS") || xmas.Equals("SAMX")) ? 1 : 0;
        }
    }
}