using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day7 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var instructions = await InputHandler.GetInputByCommaSeparationAsync(day);
            var program = instructions.Select(int.Parse).ToList();

            int[] validNumbers = { 0, 1, 2, 3, 4 };
            var combinations = GetPermutations(validNumbers, 5).ToList();

            var maxOutput = 0;
            while (combinations.Count > 0)
            {
                var programOutput = 0;
                var setting = combinations.ElementAt(0);
                for (var j = 0; j < 5; j++)
                {
                   programOutput = IntCodeComputer.GetProgramOutputByInput(new List<int>(program), setting.ElementAt(j), programOutput);
                }

                if (maxOutput < programOutput)
                {
                    maxOutput = programOutput;
                }
                
                combinations.Remove(setting);
            }

            var result1 = maxOutput.ToString();
            var result2 = string.Empty;

            return (result1, result2.ToString());
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
