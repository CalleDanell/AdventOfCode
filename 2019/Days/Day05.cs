using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2019.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(nameof(Day05));
            var intInput = input.Select(int.Parse).ToList();

            var computer = new IntCodeComputer(new List<int>(intInput));

            computer.Input(1);
            var result1 = 0;
            while(computer.ComputerState != ComputerState.Stopped)
            {
                result1 = computer.Run();
            }
            
            computer.Restore();

            computer.Input(5);
            var result2 = computer.Run();

            return (nameof(Day05), result1.ToString(), result2.ToString());
        }
    }
}
