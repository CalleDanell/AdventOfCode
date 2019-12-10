using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day5 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(day);
            var intInput1 = input.Select(int.Parse).ToList();
            var intInput2 = new List<int>(intInput1);

            var result1 = IntCodeComputer.GetProgramOutputByInput(intInput1, 1);
            var result2 = IntCodeComputer.GetProgramOutputByInput(intInput2, 5);

            return (result1.ToString(), result2.ToString());
        }
    }
}
