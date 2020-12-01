using Common;
using Common.Days;
using System.Threading.Tasks;

namespace _2019.Days
{
    public class Day11 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var instructions = await InputHandler.GetInputByCommaSeparationAsync(day);

            return (string.Empty, string.Empty);
        }
    }
}
