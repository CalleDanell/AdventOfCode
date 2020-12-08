using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day07 : IDay
    {
        const string MyBag = "shiny gold";

        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var bags = input.ToDictionary(x => ProcessInput(x).Item1, x => ProcessInput(x).Item2);

            var bagsWithMyBag = bags.Where(x => x.Value.Select(bag => bag.Color).Contains(MyBag)).Select(x => x.Key).ToList();
            var total = bagsWithMyBag;
            while (bagsWithMyBag.Any())
            {
                var nextBags = bags.Where(x => x.Value.Select(bag => bag.Color).Intersect(bagsWithMyBag).Any()).ToList();
                bagsWithMyBag = nextBags.Select(x => x.Key).ToList();
                total = total.Concat(bagsWithMyBag).ToList();
            }

            var resultPartOne = total.Distinct().Count();

            var resultPartTwo = CalculateCount(bags.GetValueOrDefault(MyBag), bags);

            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static int CalculateCount(IEnumerable<Bag> bagRules, IReadOnlyDictionary<string, IEnumerable<Bag>> bags)
        {
            var enumerable = bagRules.ToList();
            var sum = enumerable.Sum(x => x.Count);
            if(enumerable.Any(x => x.Color == "no other"))
            {
                return sum + 0;
            }

            return sum + enumerable.Sum(x => CalculateCount(bags.GetValueOrDefault(x.Color), bags) * x.Count);
        }

        private (string,IEnumerable<Bag>) ProcessInput(string input)
        {
            input = Regex.Replace(input, "(bag)[\\w]?", string.Empty).Replace(".", string.Empty);
            var parts = input.Split(new[] { "contain" }, StringSplitOptions.None);
            var color = parts[0].Trim();

            var childBags = parts[1].Split(',').Select(x => new Bag
            {
                Color = x.RemoveIntegers().Trim(),
                Count = int.TryParse(Regex.Match(x, @"\d+").Value, out int result) ? result : 0
            });

            return (color, childBags);
        }
    }

    public class Bag
    {
        public string Color { get; set; }
        public int Count { get; set; }
    }
}