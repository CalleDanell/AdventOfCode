using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(day);
            var intInput = input.Select(int.Parse).ToList();

            var computer = new IntCodeComputer(new List<int>(intInput));

            computer.Input(1);
            var result1 = computer.Run();

            computer.Restore();

            computer.Input(5);
            var result2 = computer.Run();

            return (result1.ToString(), result2.ToString());
        }
    }
}
