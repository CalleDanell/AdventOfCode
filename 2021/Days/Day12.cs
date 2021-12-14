using Common;
using Common.Coordinates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day12 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day12));

       
            var resultPartOne = 0;
            var resultPartTwo = 1;

            return (nameof(Day12), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}