using _2019.Days;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _2019
{
    public class Program
    {
        public static async Task Main()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("Results:");
            Console.WriteLine($"{nameof(Day01)}: {await new Day01().Solve(nameof(Day01))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day02)}: {await new Day02().Solve(nameof(Day02))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day03)}: {await new Day03().Solve(nameof(Day03))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day04)}: {await new Day04().Solve(nameof(Day04))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day05)}: {await new Day05().Solve(nameof(Day05))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day06)}: {await new Day06().Solve(nameof(Day06))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day07)}: {await new Day07().Solve(nameof(Day07))} { GetExecutionTime(stopWatch) } ms");

            stopWatch.Stop();
        }

        public static long GetExecutionTime(Stopwatch watch)
        {
            var time = watch.ElapsedMilliseconds;
            watch.Restart();
            return time;
        }
    }
}
