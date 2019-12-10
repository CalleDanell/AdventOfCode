using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AOC.Days;

namespace AOC
{
    class Program
    {
        static async Task Main()
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Console.WriteLine("Results:");
            Console.WriteLine($"{nameof(Day1)}: {await new Day1().Solve(nameof(Day1))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day2)}: {await new Day2().Solve(nameof(Day2))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day3)}: {await new Day3().Solve(nameof(Day3))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day4)}: {await new Day4().Solve(nameof(Day4))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day5)}: {await new Day5().Solve(nameof(Day5))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day6)}: {await new Day6().Solve(nameof(Day6))} { GetExecutionTime(stopWatch) } ms");
            Console.WriteLine($"{nameof(Day7)}: {await new Day7().Solve(nameof(Day7))} { GetExecutionTime(stopWatch) } ms");

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
