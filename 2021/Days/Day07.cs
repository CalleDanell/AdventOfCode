using System.Linq;
using Common;
using System.Threading.Tasks;
using System;

namespace _2021.Days
{
    public class Day07 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(nameof(Day07));

            var initialPositions = input.Select(int.Parse).ToList();

            var min = initialPositions.Min();
            var max = initialPositions.Max();
            
            var basicFuelCost = int.MaxValue;
            var advancedFuelCost = int.MaxValue;
            for (var i = min; i < max; i++)
            {
                var cost = initialPositions.Select(x =>
                {
                    var basic = Math.Abs(x - i);
                    var advanced = basic * (basic + 1) / 2;
                    return (basic, advanced);
                }).ToList();

                var currentBasicFuelCost = cost.Select(x => x.basic).Sum();
                var currentAdvancedFuelCost = cost.Select(x => x.advanced).Sum();

                if (currentBasicFuelCost < basicFuelCost)
                    basicFuelCost = currentBasicFuelCost;

                if (currentAdvancedFuelCost < advancedFuelCost)
                    advancedFuelCost = currentAdvancedFuelCost;
            }

            var resultPartOne = basicFuelCost;
            var resultPartTwo = advancedFuelCost;

            return (nameof(Day07), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}