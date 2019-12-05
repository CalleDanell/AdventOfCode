using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day2 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(day);
            var intInput = input.Select(int.Parse).ToList();

            var resultOne = IntCodeComputer.GetProgramOutput(new List<int>(intInput), 12, 2);
            var resultTwo = IntCodeComputer.GetNounAndVerbForOutput(new List<int>(intInput), 99, 19690720);

            return (resultOne.ToString(), resultTwo.ToString());
        }
    }
}
