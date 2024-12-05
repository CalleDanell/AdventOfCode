using Common;

namespace _2024.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);
            (Dictionary<int, List<int>> dict, List<List<int>> sequences) = ParseInput(input);
            
            
            var wasSorted = new List<List<int>>();
            var sorted = new List<List<int>>();

            foreach (var sequence in sequences)
            {
                var orderedSequence = new List<int>();
                var ordered = true;
                for (var i = 0; i < sequence.Count - 1; i++)
                {
                    var current = sequence[i];
                    var isAfter = sequence.GetRange(i, sequence.Count - i);

                    List<int> isBefore;
                    isBefore = dict.TryGetValue(current, out var value) ? value : new List<int>();
                    var shouldBeBeforeCurrent = isAfter.Intersect(isBefore);

                    if (shouldBeBeforeCurrent.Any())
                    {
                        ordered = false;
                        sequence.RemoveAt(i);
                        sequence.Insert(sequence.Count, current);
                        i--;
                    }
                    else
                    {
                        orderedSequence.Add(current);
                    }
                }

                if (ordered)
                {
                    wasSorted.Add(orderedSequence);
                }
                else
                {
                    sorted.Add(orderedSequence);
                }
            }

            var partOne = wasSorted.Select(x => x[x.Count / 2]).Sum(x => x);
            var partTwo = sorted.Select(x => x[x.Count / 2]).Sum(x => x);

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static (Dictionary<int, List<int>>,List<List<int>>) ParseInput(IEnumerable<string> input)
        {
            var dict = new Dictionary<int, List<int>>();
            var sequences = new List<List<int>>();
            var isQueue = true;

            foreach (var inputItem in input)
            {
                if (inputItem.Equals(string.Empty))
                {
                    isQueue = false; continue;
                }

                if (isQueue)
                {
                    var parts = inputItem.Split('|').Select(int.Parse).ToList();

                    if (!dict.ContainsKey(parts[1]))
                    {
                        dict[parts[1]] = new List<int>() { parts[0] };
                    }
                    else
                    {
                        dict[parts[1]].Add(parts[0]);
                    }
                }
                else
                {
                    sequences.Add(inputItem.Split(",").Select(int.Parse).ToList());
                }
            }

            return (dict, sequences);
        }
    }
}