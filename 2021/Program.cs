using System.Threading.Tasks;
using _2021.Days;
using Common;

namespace _2021
{
    internal class Program
    {
        private static async Task Main()
        {
            var solver = new Solver(10);
            await solver.Solve(new Day08());
        //    await solver.Solve(
        //        new Day01(), new Day02(), new Day03(),
        //        new Day04(), new Day05(), new Day06(),
        //        new Day07(), new Day08(), new Day09(),
        //        new Day10(), new Day11(), new Day12(),
        //        new Day13(), new Day14(), new Day17(),
        //        new Day20(), new Day21(), new Day22()
        //        );
        }
    }
}
