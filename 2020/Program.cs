using _2020.Days;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;

namespace _2020
{
    internal class Program
    {
        private static async Task Main()
        {
            var days = new List<IDay>
            {
                new Day01(), new Day02(), new Day03(),
                new Day04(), new Day05(), new Day06(),
                new Day07(), new Day08(), new Day09(),
                new Day10(), new Day11(), new Day12(),
                new Day13(), new Day14()
            };

            var solver = new Solver();
            solver.AddProblems(days);
            await solver.SolveAll();
        }
    }
}