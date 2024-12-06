using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Common
{
    public class Solver
    {
        public Solver() {}

        public async Task Solve(IDay day)
        {
            var watch = Stopwatch.StartNew();
            var result = await day.Solve();
            watch.Stop();
            Console.WriteLine($"{result.Item1}: {result.Item2}  {result.Item3}  - {watch.ElapsedMilliseconds} ms");
        }
    }
}