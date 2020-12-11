using _2020.Days;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace _2020
{
    internal class Program
    {
        private static async Task Main()
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
            Console.WriteLine($"{nameof(Day08)}: {await new Day08().Solve(nameof(Day08))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day09)}: {await new Day09().Solve(nameof(Day09))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day10)}: {await new Day10().Solve(nameof(Day10))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day11)}: {await new Day11().Solve(nameof(Day11))} { GetExecutionTime(stopWatch) } ms");

            stopWatch.Stop();
        }

        private static long GetExecutionTime(Stopwatch watch)
        {
            var time = watch.ElapsedMilliseconds;
            watch.Restart();
            return time;
        }
    }
}