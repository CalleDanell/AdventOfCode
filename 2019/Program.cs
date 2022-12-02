using _2019.Days;
using Common;
using System.Threading.Tasks;

namespace _2019
{
    public class Program
    {
        public static async Task Main()
        {
            var solver = new Solver(10);
            
            await solver.Solve(
                new Day01(), new Day02(), new Day03(),
                new Day04(), new Day05(), new Day06(),
                new Day07(), new Day08());
        }
    }
}
