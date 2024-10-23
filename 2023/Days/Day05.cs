using Common;

namespace _2023.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(nameof(Day05), "@");
            var startValues = GetNumbers(input.ElementAt(0)).ElementAt(0).Split(' ').Select(long.Parse).ToArray();

            var translatedValues = new List<long>();
            foreach (var startNumber in startValues)
            {
                translatedValues.Add(Translate(input, startNumber));
            }

            var partOneRanges = new List<(long, long)>();
            var partTwoRanges = new List<(long, long)>();
            for (var i = 0; i < startValues.Count(); i += 2)
            {
                var start = startValues[i];
                var end = startValues[i] + startValues[i + 1];
                partOneRanges.Add((start, start));
                partOneRanges.Add((startValues[i + 1], startValues[i + 1]));
                partTwoRanges.Add((start, end));
            }


            var smallestOne = await FindSmallestLocaiton(input, partOneRanges);
            var smallestTwo = await FindSmallestLocaiton(input, partTwoRanges);

            return (nameof(Day05), smallestOne.Min().ToString(), smallestTwo.Min().ToString());
        }

        private static async Task<List<long>> FindSmallestLocaiton(IEnumerable<string> input, List<(long, long)> ranges)
        {
            var translated = new List<long>();
            var tasks = new List<Task>();
            foreach (var range in ranges)
            {
                tasks.Add(Task.Run(() =>
                {
                    var current = range.Item1;
                    var min = long.MaxValue;
                    while (current <= range.Item2)
                    {
                        var translatedValue = Translate(input, current);
                        if (translatedValue < min)
                            min = translatedValue;

                        current++;
                    }

                    translated.Add(min);
                }));
            }

            await Task.WhenAll(tasks);

            return translated;
        }

        private static long Translate(IEnumerable<string> input, long startValue)
        {
            var value = startValue;
            for (var i = 1; i < input.Count(); i++)
            {
                var mapRows = GetNumbers(input.ElementAt(i)).ToList();
                mapRows.RemoveAt(0);

                foreach (var row in mapRows)
                {
                    var numbersInRow = row.Split(' ').Select(long.Parse);
                    var destination = numbersInRow.ElementAt(0);
                    var source = numbersInRow.ElementAt(1);
                    var length = numbersInRow.ElementAt(2);

                    if (value >= source && value < source + length)
                    {
                        var shift = destination - source;
                        value = value + shift;
                        break;
                    }
                }
            }

            return value;
        }

        private static IEnumerable<string> GetNumbers(string input) => input.Split(':')[1].Trim().Split('@');
    }
}