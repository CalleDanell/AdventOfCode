using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AOC.Days;

namespace AOC
{
    class Program
    {
        static async Task Main()
        {
            var results = new Dictionary<string, (string, string)>
            {
                {nameof(Day1), await new Day1().Solve(nameof(Day1))},
                {nameof(Day2), await new Day2().Solve(nameof(Day2))},
                {nameof(Day3), await new Day3().Solve(nameof(Day3))},
                {nameof(Day4), await new Day4().Solve(nameof(Day4))},
                {nameof(Day5), await new Day5().Solve(nameof(Day5))},
                {nameof(Day6), await new Day6().Solve(nameof(Day6))}
            };

            Console.WriteLine("Results:");
            foreach (var (key, (solutionOne, solutionTwo)) in results)
            {
                Console.WriteLine($"{key}: {solutionOne}, {solutionTwo}");
            }
        }
    }
}
