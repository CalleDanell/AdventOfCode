using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Common
{
    public class Solver
    {
        private readonly IList<Task> tasks = new List<Task>();

        public async Task SolveAll()
        {
           await Task.WhenAll(tasks);
        }

        public void AddProblem(IDay day)
        {
            tasks.Add(Task.Run(async () =>
            {
                var watch = Stopwatch.StartNew();
                watch.Start();
                var result = await day.Solve();
                return (watch, result);

            }).ContinueWith(async x => 
            {
                var result = await x;
                result.watch.Stop();
                Console.WriteLine($"{result.result.Item1}: ({result.result.Item2}, {result.result.Item3}) - {result.watch.ElapsedMilliseconds} ms");
            }
            ));
        }

        public void AddProblems(IEnumerable<IDay> days)
        {
            foreach (var day in days)
            {
                AddProblem(day);
            }
        }
    }
}