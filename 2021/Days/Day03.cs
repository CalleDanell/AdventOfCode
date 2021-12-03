using System;
using System.Collections.Generic;
using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day03));

            var binaries = input.Select(x => x.ToCharArray()).ToList();

            var oxygen = GetOxygenRating(new List<char[]> (binaries));
            var co2 = GetCo2Rating(new List<char[]>(binaries));

            var (gamma, epsilon) = GetGammaAndEpsilonRates(binaries);

            var resultPartOne = gamma * epsilon;
            var resultPartTwo = oxygen * co2;

            return (nameof(Day03), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private int GetOxygenRating(List<char[]> binaries)
        {
            for (var j = 0; j < binaries.First().Length; j++)
            {
                var binariesOnSameIndex = binaries.Select(x => x[j]);
                var groups = binariesOnSameIndex.GroupBy(x => x)
                    .Select(group => new { Binary = group.Key, Count = group.Count()})
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var leastCommonBinary = groups.Last().Binary;
                if (groups[0].Count == groups[1].Count)
                {
                    binaries.RemoveAll(x => x.ElementAt(j).Equals('0'));
                } 
                else
                {
                    binaries.RemoveAll(x => x.ElementAt(j).Equals(leastCommonBinary));
                }

                if(binaries.Count == 1)
                    break;
            }

            return Convert.ToInt32(new string(binaries[0]), 2);
        }

        private int GetCo2Rating(List<char[]> binaries)
        {
            for (var j = 0; j < binaries.First().Length; j++)
            {
                var binariesOnSameIndex = binaries.Select(x => x[j]);
                var groups = binariesOnSameIndex.GroupBy(x => x)
                    .Select(group => new { Binary = group.Key, Count = group.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                var mostCommonBinary = groups.First().Binary;
                if (groups[0].Count == groups[1].Count)
                {
                    binaries.RemoveAll(x => x.ElementAt(j).Equals('1'));
                }
                else
                {
                    binaries.RemoveAll(x => x.ElementAt(j).Equals(mostCommonBinary));
                }

                if (binaries.Count == 1)
                    break;
            }

            return Convert.ToInt32(new string(binaries[0]), 2);
        }


        private (int, int) GetGammaAndEpsilonRates(List<char[]> binaries)
        {
            var gammaRateBinary = string.Empty;
            var epsilonRateBinary = string.Empty;

            for (var j = 0; j < binaries.First().Length; j++)
            {
                var binariesOnSameIndex = binaries.Select(x => x[j]);
                var groups = binariesOnSameIndex.GroupBy(x => x)
                    .Select(group => new { Binary = group.Key, Count = group.Count() })
                    .OrderByDescending(x => x.Count)
                    .ToList();

                gammaRateBinary += groups.First().Binary;
                epsilonRateBinary += groups.Last().Binary;
            }

            return (Convert.ToInt32(gammaRateBinary, 2), Convert.ToInt32(epsilonRateBinary, 2));
        }
    }
}