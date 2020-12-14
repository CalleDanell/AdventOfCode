using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day14 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day14));
            var instructions = new Queue<string>(input.ToList());

            var instructionGroups = GetInstructionGroups(instructions);

            var maskedValues = RunProgram(instructionGroups);
            
            var result1 = maskedValues.Select(x => CharArrayTo36Int(x.Value)).Sum();
            var result2 = string.Empty;

            return (nameof(Day14), result1.ToString(), result2.ToString());
        }

        private static Dictionary<string, List<(int address, string digits)>> GetInstructionGroups(Queue<string> instructions)
        {
            var instructionGroups = new Dictionary<string, List<(int address, string digits)>>();
            var currentMask = string.Empty;
            while (instructions.Count > 0)
            {
                var ins = instructions.Dequeue();
                var parts = ins.Split('=');
                if (ins.StartsWith("mask"))
                {
                    currentMask = parts[1].Trim();
                    instructionGroups.Add(currentMask, new List<(int address, string digits)>());
                }
                else
                {
                    var address = int.Parse(Regex.Match(parts[0], "(?<=\\[).+?(?=\\])").Value);
                    var value = Convert.ToString(long.Parse(parts[1]), 2).PadLeft(36, '0');
                    instructionGroups[currentMask].Add((address, value));
                }
            }

            return instructionGroups;
        }

        private static Dictionary<int, char[]> RunProgram(Dictionary<string, List<(int address, string digits)>> instructionGroups)
        {
            var dict = new Dictionary<int, char []>();
            foreach (var (mask, valueTuples) in instructionGroups)
            {
                var maskArray = mask.ToCharArray();
                foreach (var (address, digits) in valueTuples)
                {
                    var digitArray = digits.ToCharArray();
                    var result = Mask(digitArray, maskArray);
                    if (dict.ContainsKey(address))
                    {
                        dict[address] = result;
                    }
                    else
                    {
                        dict.Add(address, result);
                    }
                }
            }

            return dict;
        }

        private static char[] Mask(char[] digitArray, char[] maskArray)
        {
            var resultArray = new char[digitArray.Length];
            for (var i = 0; i < digitArray.Length; i++)
            {
                if (!maskArray[i].Equals('X') && !digitArray[i].Equals(maskArray[i]))
                {
                    resultArray[i] = maskArray[i];
                }
                else
                {
                    resultArray[i] = digitArray[i];
                }
            }

            return resultArray;
        }

        private static long CharArrayTo36Int(IEnumerable<char> digits)
        {
            long sum = 0;
            var reverse = digits.Reverse().ToList();

            for (var i = 0; i < reverse.Count; i++)
            {
                if (reverse[i].Equals('1'))
                {
                    sum += (long) Math.Pow(2, i);
                }
            }

            return sum;
        }
    }
}