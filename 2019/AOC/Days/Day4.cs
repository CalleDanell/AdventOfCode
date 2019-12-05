using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day4 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByDashSeparationAsync(day);

            var intInput = input.Select(int.Parse).ToList();
            var (result1, result2) = GetNumberOfPasswordCombinations(intInput.ElementAt(0), intInput.ElementAt(1));

            return (result1.ToString(), result2.ToString());
        }

        private static (int, int) GetNumberOfPasswordCombinations(int startValue, int endValue)
        {
            var count = (0, 0);
            for (var i = startValue; i <= endValue; i++)
            {
                var digits = (startValue++).ToString().Select(Convert.ToInt32).ToArray();
                var (result1, result2) = ValidateCombination(digits);
                count = (count.Item1 + result1, count.Item2 + result2);
            }

            return count;
        }

        private static (int, int) ValidateCombination(int[] digits)
        {
            if (digits[0] <= digits[1] && 
                digits[1] <= digits[2] && 
                digits[2] <= digits[3] && 
                digits[3] <= digits[4] && 
                digits[4] <= digits[5] && 
                FindPairs(digits))
            {
                return FindOnlyOnePair(digits) ? (1, 1) : (1, 0);
            }

            return (0, 0);
        }

        private static bool FindOnlyOnePair(IEnumerable<int> integers)
        {
            return integers.GroupBy(i => i).Where(x => x.Count() == 2).ToList().Any();
        }

        private static bool FindPairs(int[] integers)
        {
            for (var i = 0; i < integers.Length - 1; i++)
            {
                if (integers[i] + integers[i + 1] == integers[i] * 2)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
