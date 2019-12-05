using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day5 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(day);
            var intInput = input.Select(int.Parse).ToList();


            return (string.Empty, string.Empty);
        }
    }
}
