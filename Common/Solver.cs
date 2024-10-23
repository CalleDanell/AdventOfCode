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

        /// <summary>
        /// Solves days concurrently. Time out in seconds.
        /// </summary>
        /// <param name="timeOutPerDay">In seconds</param>
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
                var result = await day.Solve();
                watch.Stop();
                Console.WriteLine($"{result.Item1}: {result.Item2}  {result.Item3}  - {watch.ElapsedMilliseconds} ms");

                // Mark as no timeout
                shouldTimeout = false;
            }),

                Task.Run(async () =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(timeout));
                    if (shouldTimeout)
                    {
                        Console.WriteLine($"{day.GetType().Name} took longer than {timeout} seconds and timed out...");
                    }
                })
            );

            tasks.Add(task);
        }
    }
}