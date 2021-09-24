using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Common;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace _2020.Days
{

    /// <summary>
    /// Got stuck so took similar (but better) solution from here:
    /// https://github.com/encse/adventofcode/blob/master/2020/Day18/Solution.cs
    /// </summary>
    public class Day19 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day19));
            
            var enumerable = input.ToList();
            var rules = enumerable.Where(x => !string.IsNullOrEmpty(x) && char.IsDigit(x.First()));

            var ruleTable = rules.ToDictionary(
                    x => int.Parse(x.Split(':')[0]), 
                    y => new List<string>(y.Split(':')[1].Trim().Split(" "))
                );


            var combinations = new List<string>();
            FindCombinations(0, ruleTable, combinations);

            var messages = enumerable.Where(x => !string.IsNullOrEmpty(x) &&  char.IsLetter(x.First()));


            var result1 = string.Empty;
            var result2 = string.Empty;

            return (nameof(Day19), result1.ToString(), result2.ToString());
        }


        private void FindCombinations(int key,  Dictionary<int,List<string>> ruleTable, List<string> combinations)
        {
            var list = ruleTable[key];
            if (list.First().Contains("a") || list.First().Contains("b"))
            {
                combinations.Add(string.Join("", list.First()));
                return;
            }

            foreach (var next in list)
            {
                if (next.Equals("|"))
                {
                    // "Fork the string that is building"
                }
                else
                {
                    FindCombinations(int.Parse(next), ruleTable, combinations);
                }
            }
        }

        private static void GenerateAllCombinations(string address, List<char[]> output)
        {
            if (address.All(x => x != 'X'))
            {
                output.Add(new List<char>(address).ToArray());
                return;
            }

            var lastIndex = address.LastIndexOf('X');

            GenerateAllCombinations(address.Remove(lastIndex, 1).Insert(lastIndex, "0"), output);
            GenerateAllCombinations(address.Remove(lastIndex, 1).Insert(lastIndex, "1"), output);
        }
    }
}