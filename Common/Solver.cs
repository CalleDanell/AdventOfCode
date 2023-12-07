using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Common
{
    public class Solver
    {
        private readonly List<Task> tasks = new List<Task>();
        private int timeout;

        public Solver(int timeOutPerDay)
        {
            this.timeout = timeOutPerDay;
        }

        public async Task Solve(params IDay[] days)
        {
            foreach(var day in days)
            {
                AddTask(day);
            }

            await Task.WhenAll(tasks);
        }

        private void AddTask(IDay day)
        {
            var shouldTimeout = true;

            var task = Task.WhenAny(
                Task.Run(async () =>
                {
                    var watch = Stopwatch.StartNew();
                    watch.Start();
                    var result = await day.Solve();
                    return (watch, result);
                }).ContinueWith(async x =>
                {
                    var result = await x;
                    result.watch.Stop();
                    Console.WriteLine($"{result.result.Item1}: {result.result.Item2}  {result.result.Item3}  - {result.watch.ElapsedMilliseconds} ms");

                    // Completed so no timeout
                    shouldTimeout = false;
                }),

                Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeout));

                    if(shouldTimeout)
                    {
                        Console.WriteLine($"{day.GetType().Name} took longer than {timeout} seconds and timed out...");
                    }
                })
            );

            tasks.Add(task);
        }
    }
}