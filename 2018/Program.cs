using System.Collections.Generic;
using System.Threading.Tasks;
using _2018.Days;
using Common;

namespace _2018
{
    public class Program
    {
        public static async Task Main()
        {
            var days = new List<IDay>
            {
                new Day01(), new Day02(), new Day03(), new Day04()
            };

            var solver = new Solver();
            solver.AddProblems(days);
            await solver.SolveAll();
        }
    }
}
