using Common;
using Common.Coordinates;
using System.Text.RegularExpressions;

namespace _2022.Days
{
    public class Day16 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var partOne = 0;
            var partTwo = 1;
            return (day, partOne.ToString(), partTwo.ToString());
        }
    }
}