using Common;

namespace _2023.Days
{
    public class Day08 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day08));

            var instructions = input.First().ToCharArray();
            var map = GenerateMap(input);

            int stepsPartsOne = StepsToEnd(map, instructions);

            var startPosistions = map.Keys.Where(x => x.EndsWith("A"));
            var distances = GetStepsToFirstEnd(map, instructions, startPosistions);
            var stepsPartTwo = CalculateLCM(distances);

            return (nameof(Day08), stepsPartsOne.ToString(), stepsPartTwo.ToString());
        }

        private static int StepsToEnd(Dictionary<string, (string, string)> map, char[] instructions)
        {
            var found = false;
            var steps = 0;
            var currentKey = "AAA";

            while (!found)
            {
                for (var i = 0; i < instructions.Length; i++)
                {
                    currentKey = Move(map, currentKey, instructions[i]);
                    steps++;

                    if (currentKey.Equals("ZZZ"))
                    {
                        found = true;
                        break;
                    }
                }
            }

            return steps;
        }

        private static List<long> GetStepsToFirstEnd(Dictionary<string, (string, string)> map, char[] instructions, IEnumerable<string> startPosistions)
        {
            var distances = new List<long>();

            foreach (var key in startPosistions)
            {
                var currentKey = key;
                var steps = 0;
                var found = false;
                while (!found)
                {
                    for (var i = 0; i < instructions.Length; i++)
                    {
                        currentKey = Move(map, currentKey, instructions.ElementAt(i));
                        steps++;
                        if (currentKey.EndsWith("Z"))
                        {
                            distances.Add(steps);
                            found = true;
                            break;
                        }
                    }
                }
            }

            return distances;
        }

        private static Dictionary<string, (string, string)> GenerateMap(IEnumerable<string> input)
        {
            var map = new Dictionary<string, (string, string)>();
            foreach (var line in input.Skip(2))
            {
                var parts = line.Split(new[] { '=', '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);

                var key = parts[0].Trim();
                var value = (parts[2].Trim(), parts[3].Trim());
                if (!map.TryAdd(key, value))
                {
                    map[key] = value;
                }
            }

            return map;
        }

        private static string Move(Dictionary<string, (string, string)> map, string currentPosition, char direction)
        {
            if (direction.Equals('L'))
            {
                currentPosition = map[currentPosition].Item1;
            }
            else
            {
                currentPosition = map[currentPosition].Item2;
            }

            return currentPosition;
        }

        private static long CalculateLCM(List<long> numbers)
        {
            var lcm = numbers[0];
            for (int i = 1; i < numbers.Count; i++)
            {
                lcm = CalculateLCMOfTwoNumbers(lcm, numbers[i]);
            }

            return lcm;
        }

        private static long CalculateLCMOfTwoNumbers(long a, long b)
        {
            return (a / CalculateGCD(a, b)) * b;
        }

        private static long CalculateGCD(long a, long b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}