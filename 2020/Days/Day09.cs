using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day09 : IDay
    {
        private const int PreambleLength = 25;

        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);
            var numberSeries = input.Select(long.Parse).ToList();

            var erroneousNumber = FindErroneousNumber(numberSeries);
            var resultPartTwo = FindEncryptionWeakness(numberSeries, erroneousNumber);

            return (erroneousNumber.ToString(), resultPartTwo.ToString());
        }

        private static long FindEncryptionWeakness(IReadOnlyCollection<long> numberSeries, long erroneousNumber)
        {
            long sum = 0;
            var sequence = new List<long>();
            for (var i = 0; i < numberSeries.Count; i++)
            {
                var number = numberSeries.ElementAt(i);
                sequence.Add(number);
                sum += number;
                if (sum == erroneousNumber)
                {
                    break;
                }
                if (sum > erroneousNumber)
                {
                    i -= sequence.Count -1;
                    sum = 0;
                    sequence.Clear();
                }
            }

            return sequence.Min() + sequence.Max();
        }


        private static long FindErroneousNumber(IReadOnlyCollection<long> numberSeries)
        {
            for (var i = 0; i < numberSeries.Count; i++)
            {
                var numbersToCheck = numberSeries.Skip(i).ToList();
                var preamble = numbersToCheck.Take(PreambleLength).ToList();
                var numberToValidate = numbersToCheck.ElementAt(PreambleLength);

                var found = false;
                foreach (var number in preamble)
                {
                    if (preamble.Any(n => number + n == numberToValidate))
                    {
                        found = true;
                    }
                }

                if (!found)
                {
                    {
                        return numberToValidate;
                    }
                }
            }

            return 0;
        }
    }
}