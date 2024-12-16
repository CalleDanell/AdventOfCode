using Common;

namespace _2024.Days
{
    public class Day16 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetFullInput(day);

            var partOne = 0;
            var partTwo = 0;

            return (day, partOne.ToString(), partTwo.ToString());
        }
    }
}