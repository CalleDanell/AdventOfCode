using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);
            var intInput = input.Select(int.Parse).ToArray();

            var resultPartOne = intInput.Sum(CalculateFuel);
            var resultPartTwo = intInput.Sum(CalculateTotalWeight);

            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static int CalculateFuel(int value)
        {
            return value / 3 - 2;
        }

        private static int CalculateTotalWeight(int value)
        {
            var fuel = value / 3 - 2;
            if (fuel <= 0) return 0;
            return fuel + CalculateTotalWeight(fuel);
        }
    }
}
