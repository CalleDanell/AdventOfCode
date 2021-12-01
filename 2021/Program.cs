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
                new Day01()
            };

            var solver = new Solver();
            solver.AddProblems(days);
            await solver.SolveAll();
        }
    }
}
