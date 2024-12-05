using Common;
using System.Text.RegularExpressions;

namespace _2024.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetFullInput(day);

            var instructions = Regex.Matches(input, "mul\\(\\d{1,3},\\d{1,3}\\)|do\\(\\)|don't\\(\\)");

            var partOne = ExecuteInstructions(instructions, false);
            var partTwo = ExecuteInstructions(instructions, true); 
            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static int ExecuteInstructions(MatchCollection instructions, bool skipFlag)
        {
            var result = 0;
            var skip = false;
            foreach (var match in instructions.ToList())
            {
                if (match.Value.Contains("don't()"))
                {
                    skip = skipFlag; continue;
                }
                if (match.Value.Contains("do()"))
                {
                    skip = false; continue;
                }

                if (!skip)
                {
                    var digits = Regex.Matches(match.Value, "\\d{1,3}");
                    result += int.Parse(digits.ElementAt(0).Value) * int.Parse(digits.ElementAt(1).Value);
                }
            }

            return result;
        }
    }
}