using System.Collections.Generic;
using System.Data;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day13 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day13));
            var inputList = input.ToList();
            var earliestTimestamp = long.Parse(inputList.ElementAt(0));

            var buses = inputList.ElementAt(1).Split(',')
                .Select((x, i) => x != "x" ? 
                    new Bus { Name = int.Parse(x), TimeOffset = i } : null)
                .Where(x => x != null)
                .ToList();

            var result1 = FindEarliestBus(buses, earliestTimestamp);
            //var result2 = FindEarliestTimestamp(buses);
            var result2 = "It is too slow...";

            return (nameof(Day13), result1.ToString(), result2);
        }

        private static long FindEarliestTimestamp(IReadOnlyCollection<Bus> buses)
        {
            long earliestTimestamp = 147358165332000;
            var max = buses.Max(x => x.Name - x.TimeOffset);
            while (true)
            {
                if (earliestTimestamp > 0 && buses.All(b => (earliestTimestamp + b.TimeOffset) % b.Name == 0))
                {
                    break;
                }

                earliestTimestamp += max;
            }

            return earliestTimestamp;
        }


        private static long FindEarliestBus(IReadOnlyCollection<Bus> buses, long earliestTimestamp)
        {
            var nextTimestamp = earliestTimestamp;
            Bus bus;
            while (true)
            {
                bus = buses.FirstOrDefault(b => nextTimestamp % b.Name == 0);
                if (bus != null)
                {
                    break;
                }
                nextTimestamp++;
            }

            return (nextTimestamp - earliestTimestamp) * bus.Name;
        }

        public class Bus
        {
            public int Name { get; set; }

            public int TimeOffset { get; set; }
        }
    }
}