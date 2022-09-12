using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day14 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day14));

            var polymerInfo = input.ToList();

            var polymerTemplate = new Queue<char>(polymerInfo.ElementAt(0).ToCharArray());
            //var polymerTemplate = polymerInfo.ElementAt(0);
            polymerInfo.RemoveRange(0,2);

            var polymerRules = polymerInfo.Select(x => x.Split(new[] {" -> "}, StringSplitOptions.None)).ToDictionary(x => x[0], x => x[1]);
            var steps = 40;

            for (var i = 0; i < steps; i++)
            {
                var newPolymerTemplate = new Queue<char>();
                while (true)
                {
                    if (polymerTemplate.Count == 1)
                    {
                        newPolymerTemplate.Enqueue(polymerTemplate.Dequeue());
                        break;
                    }

                    var first = polymerTemplate.Dequeue();
                    var second = polymerTemplate.Peek();

                    var pair = new string(new [] { first, second});
                    var element = polymerRules[pair];

                    newPolymerTemplate.Enqueue(first);
                    newPolymerTemplate.Enqueue(element.ToCharArray()[0]);
                }

                polymerTemplate = newPolymerTemplate;
                //var test = new string(polymerTemplate.ToArray());
            }

            var groups = polymerTemplate
                .GroupBy(x => x)
                .Select(group => new { Element = group.Key, Count = group.Count() })
                .OrderByDescending(x => x.Count)
                .ToList();


            var resultPartOne = groups.ElementAt(0).Count - groups.Last().Count;
            var resultPartTwo = 1;

            return (nameof(Day14), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class PolymerInsertionRule
    {
        public PolymerInsertionRule(string input)
        {
            var parts = input.Split(new[] { " -> " }, StringSplitOptions.None);
            Pair = parts[0];
            Element = parts[1];
        }

        public string Pair { get; }
        public string Element { get; }
    }
}