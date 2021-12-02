using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day03));

            var resultPartOne = 0;
            var resultPartTwo = 0;

            return (nameof(Day03), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}