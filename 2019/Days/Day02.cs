using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2019.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(day);
            var intInput = input.Select(int.Parse).ToList();

            var resultOne = GetOutputFromNounAndVerb(intInput, 12, 2);
            var resultTwo = FindNounAndVerb(intInput);

            return (resultOne, resultTwo);
        }

        private static string GetOutputFromNounAndVerb(List<int> intInput, int noun, int verb)
        {
            var computer = new IntCodeComputer(intInput);
            computer.SetNounAndVerb(noun, verb);
            return computer.Run().ToString();
        }

        private static string FindNounAndVerb(List<int> intInput)
        {
            var result = 0;
            foreach (var noun in Enumerable.Range(0, 99))
            foreach (var verb in Enumerable.Range(0, 99))
            {
                var testComputer = new IntCodeComputer(intInput);
                testComputer.SetNounAndVerb(noun, verb);
                if (testComputer.Run() == 19690720)
                {
                    result = 100 * noun + verb;
                    break;
                }
            }

            return result.ToString();
        }
    }
}
