using _2020.Days;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _2020
{
    class Program
    {
        static async Task Main()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("Results:");
            Console.WriteLine($"{nameof(Day01)}: {await new Day01().Solve(nameof(Day01))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day02)}: {await new Day02().Solve(nameof(Day02))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day03)}: {await new Day03().Solve(nameof(Day03))} { GetExecutionTime(stopWatch) } ms");

            stopWatch.Stop();
        }

        static long GetExecutionTime(Stopwatch watch)
        {
            var time = watch.ElapsedMilliseconds;
            watch.Restart();
            return time;
        }
    }
}
