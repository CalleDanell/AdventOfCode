using _2020.Days;
using System.Threading.Tasks;
using Common;

namespace _2020
{
    internal class Program
    {
        private static async Task Main()
        {
            var solver = new Solver(10);
            await solver.Solve(
                new Day01(), new Day02(), new Day03(),
                new Day04(), new Day05(), new Day06(),
                new Day07(), new Day08(), new Day09(),
                new Day10(), new Day11(), new Day12(),
                new Day13(), new Day14(), new Day15(),
                new Day16(), new Day17(), new Day18(),
                new Day19()
                );
        }
    }
}