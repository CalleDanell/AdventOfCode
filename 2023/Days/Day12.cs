using Common;
using System.Collections.Immutable;

namespace _2023.Days
{
    public class Day12 : IDay
    {
        private static Dictionary<string, int> cache = new Dictionary<string, int>();

        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day12));
            long sum = 0;
            foreach(var line in input)
            {
                var parts = line.Split(' ');
                var groups = parts[1];

                var unfoldedpattern = Enumerable.Repeat(parts[0], 5).SelectMany(x => x + '?').SkipLast(1).ToArray();
                var unfoldedValid = new string(Enumerable.Repeat(parts[1], 5).SelectMany(x => x + ',').SkipLast(1).ToArray()).Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);

                var permutations = new HashSet<string>();

                // TODO: Cannot generate all the permutations since it is too many combinations to try. Need to find different approach.
                // Dynamic programming and check group by group? 
                GeneratePermutationsRecursive(unfoldedpattern, 0, permutations);
                var numberValid = ValidArrangements(permutations, unfoldedValid);

                sum += numberValid;
            }

            return (nameof(Day12), sum.ToString(), 0.ToString());
        }


        static void GeneratePermutationsRecursive(char[] chars, int currentIndex, HashSet<string> permutations)
        {
            if (currentIndex == chars.Length)
            {
                permutations.Add(new string(chars));
                return;
            }

            if (chars[currentIndex] == '?')
            {
                chars[currentIndex] = '#';
                GeneratePermutationsRecursive(chars, currentIndex + 1, permutations);

                chars[currentIndex] = '.';
                GeneratePermutationsRecursive(chars, currentIndex + 1, permutations);

                // Reset back to '?' for backtracking
                chars[currentIndex] = '?';
            } 
            else
            {
                GeneratePermutationsRecursive(chars, currentIndex + 1, permutations);
            }
        }

        public long ValidArrangements(IEnumerable<string> combinations, IEnumerable<int> validCombinations)
        {
            long sum = 0;
            foreach(var combination in combinations) 
            {
                var groups = combination.Split(".", StringSplitOptions.RemoveEmptyEntries);
                if (groups.Count() != validCombinations.Count()) continue; 
            
                var valid = true;
                for(var i = 0; i < groups.Length; i++)
                {
                    if (!groups[i].Count().Equals(validCombinations.ElementAt(i)))
                    {
                        valid = false;
                        break;
                    }
                }

                if (valid)
                    sum++;
            }

            return sum;
        }
    }
}
