using Common;
using System.Linq;
using System.Threading.Tasks;

namespace Common.Template
{
    public class Day00 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            int resultPartOne = 0;
            int resultPartTwo = 1;

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}