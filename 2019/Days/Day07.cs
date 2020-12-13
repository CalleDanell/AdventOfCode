using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019.Days
{
    public class Day07 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var instructions = await InputHandler.GetInputByCommaSeparationAsync(nameof(Day07));
            var program = instructions.Select(int.Parse).ToList();

            int[] validNumbersOne = { 0, 1, 2, 3, 4 };
            int[] validNumbersTwo = { 5, 6, 7, 8, 9 };

            var result1 = GetMaxOutput(GetPermutations(validNumbersOne, 5).ToList(), program);
            
            var result2 = GetMaxOutputWithFeedbackLoop(GetPermutations(validNumbersTwo, 5).ToList(), program);

            return (nameof(Day07), result1, result2);
        }

        private static string GetMaxOutputWithFeedbackLoop(List<IEnumerable<int>> combinations, List<int> program)
        {
            var maxOutput = 0;
            while (combinations.Count > 0)
            {
                var programOutput = 0;
                var setting = combinations.ElementAt(0);

                var computers = new List<IntCodeComputer>
                {
                    new IntCodeComputer(program),
                    new IntCodeComputer(program),
                    new IntCodeComputer(program),
                    new IntCodeComputer(program),
                    new IntCodeComputer(program)
                };
                
                // Start the 5 computers
                for (var j = 0; j < computers.Count; j++)
                {
                    computers[j].Input(setting.ElementAt(j), programOutput);
                    programOutput = computers[j].Run();
                }

                // Feedback loop
                while (computers[4].ComputerState != ComputerState.Stopped)
                {
                    for (var j = 0; j < computers.Count; j++)
                    {
                        computers[j].Input(programOutput);
                        programOutput = computers[j].Run();
                    }
                }

                if (maxOutput < programOutput)
                {
                    maxOutput = programOutput;
                }

                combinations.Remove(setting);
            }

            return maxOutput.ToString();
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