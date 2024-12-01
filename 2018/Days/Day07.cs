using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common;

namespace _2018.Days
{
    public class Day07 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day07));

            var nodes = new Dictionary<string, List<string>>();
            foreach (var line in input)
            {
                var parts = line.Split(' ');
                var before = parts[1];
                var after = parts[7];

                if (!nodes.ContainsKey(after))
                {
                    nodes[after] = new List<string>();
                }

                nodes[after].Add(before);

                if (!nodes.ContainsKey(before))
                {
                    nodes[before] = new List<string>();
                }
            }

            var order = AlphaOrdering(nodes);
            var resultPartOne = string.Concat(order);

            return (nameof(Day07), resultPartOne, "");
        }


        private static List<string> AlphaOrdering(Dictionary<string, List<string>> nodes)
        {
            var order = new List<string>();
            var availableSteps = new SortedSet<string>(nodes.Keys.Where(step => nodes[step].Count == 0));

            while (availableSteps.Any())
            {
                var step = availableSteps.First();
                availableSteps.Remove(step);

                order.Add(step);

                foreach (var kvp in nodes)
                {
                    if (kvp.Value.Remove(step) && kvp.Value.Count == 0)
                    {
                        availableSteps.Add(kvp.Key);
                    }
                }
            }

            return order;
        }

        public class Worker
        {
            public int Count { get; set; }

            public int Decrease ()
            {
                Count--;

                if (Count <= 0) Count = 0;
                
                return Count;
            }
        }
    }
}
