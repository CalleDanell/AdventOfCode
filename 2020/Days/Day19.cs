using Common;
using System.Threading.Tasks;

namespace _2020.Days
{

    /// <summary>
    /// Got stuck so took similar (but better) solution from here:
    /// https://github.com/encse/adventofcode/blob/master/2020/Day18/Solution.cs
    /// </summary>
    public class Day19 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day19));

            var result1 = string.Empty;
            var result2 = string.Empty;

            return (nameof(Day19), result1.ToString(), result2.ToString());
        }
    }
}