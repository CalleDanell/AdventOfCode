using Common;

namespace _2022.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            int resultPartOne = 0;
            int resultPartTwo = 1;

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}