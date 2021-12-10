using System.Linq;
using Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace _2021.Days
{
    public class Day06 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(nameof(Day06));

            var startingFishAgeDistribution = Enumerable.Repeat(0, 9).ToList();

            var fish = input.Select(int.Parse);
            foreach (var f in fish)
            {
                startingFishAgeDistribution[f]++;
            }

            var resultPartOne = ProduceFish(startingFishAgeDistribution, 80);
            var resultPartTwo = ProduceFish(startingFishAgeDistribution, 256);

            return (nameof(Day06), resultPartOne.ToString(CultureInfo.InvariantCulture), resultPartTwo.ToString(CultureInfo.InvariantCulture));
        }

        private decimal ProduceFish(IEnumerable<int> startingAges, int days)
        {
            var fishByAge = startingAges.Select(Convert.ToDecimal).ToList();
            for (var i = 0; i < days; i++)
            {
                var readyFish = fishByAge[0];
                fishByAge.RemoveAt(0);
                fishByAge.Add(0);
                fishByAge[6] += readyFish;
                fishByAge[8] = readyFish;
            }

            return fishByAge.Sum();
        }
    }
}