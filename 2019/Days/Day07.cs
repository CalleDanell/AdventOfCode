using Common;
using Common.Days;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019.Days
{
    public class Day07 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var instructions = await InputHandler.GetInputByCommaSeparationAsync(day);
            var program = instructions.Select(int.Parse).ToList();

            int[] validNumbersOne = { 0, 1, 2, 3, 4 };
            int[] validNumbersTwo = { 5, 6, 7, 8, 9 };

            var list = new List<IEnumerable<int>>();
            list.Add(new List<int> { 9, 8, 7, 6, 5 });
            //var result1 = GetMaxOutput(GetPermutations(validNumbersTwo, 5).ToList(), program);
            var result1 = GetMaxOutput(list, program);;
            //var result1 = string.Empty;
            //var result2 = GetMaxOutputWithFeedbackLoop(GetPermutations(validNumbersTwo, 5).ToList(), program);

            return (result1, string.Empty);
        }

        private static string GetMaxOutput(List<IEnumerable<int>> combinations, List<int> program)
        {
            var maxOutput = 0;
            while (combinations.Count > 0)
            {
                var programOutput = 0;
                var setting = combinations.ElementAt(0);

                for (var j = 0; j < 5; j++)
                {
                    var computer = new IntCodeComputer(program);
                    computer.Input(setting.ElementAt(j), programOutput);
                    programOutput = computer.Run();
                }

                if (maxOutput < programOutput)
                {
                    maxOutput = programOutput;
                }

                combinations.Remove(setting);
            }

            return maxOutput.ToString();
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1).SelectMany(t => list.Where(o => !t.Contains(o)),
                (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
