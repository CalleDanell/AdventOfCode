using Common;
using System.Reflection.Metadata.Ecma335;

namespace _2024.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);
            var sequences = input.Select(x =>
            {
                var parts = x.Split(' ');
                return new List<int>(parts.Select(x => int.Parse(x)));
            });

            var safeSequences = new List<List<int>>();
            var unsafeSequences = new List<List<int>>();
            foreach (var sequence in sequences)
            {
                bool safe = SafetyCheck(sequence);
                if (safe)
                {
                    safeSequences.Add(sequence);
                }
                else
                {
                    unsafeSequences.Add(sequence);
                }
            }

            var partOne = safeSequences.Count;

            foreach (var sequence in unsafeSequences)
            {
                var safe = false;
                for (var i = 0; i < sequence.Count; i++)
                {
                    var temp = sequence[i];
                    sequence.RemoveAt(i);
                    safe = SafetyCheck(sequence);
                    sequence.Insert(i, temp);

                    if (safe)
                    {
                        safeSequences.Add(sequence);
                        break;
                    }
                }
            }

            var partTwo = safeSequences.Count;
            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static bool SafetyCheck(List<int> sequence)
        {
            var diffs = new List<int>();
            for (var i = 0; i < sequence.Count - 1; i++)
            {
                diffs.Add(Math.Abs(sequence[i] - sequence[i + 1]));
            }

            var isAscending = sequence.OrderBy(x => x).SequenceEqual(sequence);
            var isDescending = sequence.OrderByDescending(x => x).SequenceEqual(sequence);

            return (isAscending || isDescending) && diffs.All(x => x >= 1 && x <= 3);
        }
            
    }
}