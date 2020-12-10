using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;
using System;

namespace _2020.Days
{
    public class Day10 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var outputJoltages = input.Select(int.Parse).ToList();
            var deviceRating = outputJoltages.Max() + 3;
            const int outletRating = 0;

            outputJoltages.Add(deviceRating);
            outputJoltages.Add(outletRating);

            var orderedJoltages = outputJoltages.OrderBy(x => x).ToList();
            
            var result1 = FindJoltAdapterChain(orderedJoltages, 1, 3);
            var result2 = FindNumberOfPaths(orderedJoltages);

            return (result1.ToString(), result2.ToString());
        }

        private static long FindNumberOfPaths(IReadOnlyList<int> orderedJoltages)
        {
            var segments = new Dictionary<int, int>();
            var segmentLength = 1;
            for (var i = 0; i < orderedJoltages.Count - 1; i++)
            {
                var diff = orderedJoltages[i + 1] - orderedJoltages[i];
                if (diff == 3)
                {
                    segments.Add(i, segmentLength);
                    segmentLength = 1;
                }
                else if(diff == 1)
                {
                    segmentLength++;
                }
            }

            var combinationPerSegment = segments.Select(x => GetCombinationsForSequenceLength(x.Value));
            long totalCombinations = 1;
            foreach(var combination in combinationPerSegment)
            {
                totalCombinations *= Convert.ToInt64(combination);
            }

            return totalCombinations;
        }

        private static int GetCombinationsForSequenceLength(int length)
        {
            switch (length)
            {
                case 5:
                    return 7;
                case 4:
                    return 4;
                case 3:
                    return 2;
                default:
                    return 1;
            }
        }

        private static int FindJoltAdapterChain(IReadOnlyList<int> orderedJoltages, int firstDiff, int secondDiff)
        {
            var differences = new Dictionary<int, int>();
            for (var i = 0; i < orderedJoltages.Count - 1; i++)
            {
                var diff = orderedJoltages[i + 1] - orderedJoltages[i];
                differences.TryGetValue(diff, out var currentCount);
                differences[diff] = currentCount + 1;
            }

            return differences[firstDiff] * differences[secondDiff];
        }
    }
}