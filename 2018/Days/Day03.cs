using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day03));

            var resultPartOne = string.Empty;

            var resultPartTwo = string.Empty;

            return (nameof(Day03), resultPartOne, resultPartTwo);
        }
    }
}
