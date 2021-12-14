using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using _2021.Days;
using Common;

namespace _2021
{
    internal class Program
    {
        private static async Task Main()
        {
            var days = new List<IDay>
            {
                new Day01(), new Day02(), new Day03(), new Day04(),
                new Day05(), new Day06(), new Day07(), new Day08(),
                new Day09(), new Day10(), new Day11(), new Day12(),
                new Day13(), 
            };

            var solver = new Solver();
            solver.AddProblems(days);
            await solver.SolveAll();
        }
    }
}
