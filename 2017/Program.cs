using System.Collections.Generic;
using System.Threading.Tasks;
using _2018.Days;
using Common;

namespace _2017
{
    public class Program
    {
        public static async Task Main()
        {
            var solver = new Solver(10);
            await solver.Solve(
                new Day01());
        }
    }
}
