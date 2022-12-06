using Common;
using System.Text.RegularExpressions;

namespace _2022.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(day, Environment.NewLine);

            var crateInput = input.ElementAt(0).Split(Environment.NewLine).Skip(1).ToList();

            var instructionInput = input.ElementAt(1).Split(Environment.NewLine).Skip(1).ToList();
            var instructions = instructionInput.Select(x => new Instruction(x));

            var resultPartOne = new string(ReArrange(GetCrates(crateInput), instructions, "single").Select(x => x.Value.Peek()).ToArray());
            var resultPartTwo = new string(ReArrange(GetCrates(crateInput), instructions, "multiple").Select(x => x.Value.Peek()).ToArray());

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }

        public Dictionary<int, Stack<char>> ReArrange(Dictionary<int, Stack<char>> stacks, IEnumerable<Instruction> instructions, string mode)
        {
            var newStacks = new Dictionary<int, Stack<char>>(stacks);
            foreach (var instruction in instructions)
            {
                switch (mode)
                {
                    case "single":
                        for (var i = 0; i < instruction.Capaciity; i++)
                        {
                            var crateToMove = newStacks[instruction.From].Pop();
                            newStacks[instruction.To].Push(crateToMove);
                        }
                        break;
                    case "multiple":
                        var multiple = new List<char>();
                        for (var i = 0; i < instruction.Capaciity; i++)
                        {
                            var crateToMove = newStacks[instruction.From].Pop();
                            multiple.Add(crateToMove);
                        }

                        multiple.Reverse();
                        foreach (var crate in multiple)
                        {
                            newStacks[instruction.To].Push(crate);
                        }
                        break;
                }
            }
            
            return newStacks;
        }

        private Dictionary<int, Stack<char>> GetCrates(IEnumerable<string> input)
        {
            var stacks = new Dictionary<int, Stack<char>>();

            foreach (var inputItem in input.Reverse())
            {
                var index = 1;
                for(var i = 1; i < inputItem.Length; i+=4)
                {
                    if(char.IsDigit(inputItem[i]))
                    {
                        stacks.Add(index, new Stack<char>());
                    }

                    if (char.IsLetter(inputItem[i]))
                    {
                        stacks[index].Push(inputItem[i]);
                    }

                    index++;
                }
            }

            return stacks;
        }
    }

    public class Instruction
    {
        public Instruction(string input)
        {
            var matches = Regex.Matches(input, @"\d+");

            To = int.Parse(matches.ElementAt(2).Value);
            From = int.Parse(matches.ElementAt(1).Value);
            Capaciity = int.Parse(matches.ElementAt(0).Value);
        }

        public int Capaciity { get; }
        public int From { get; }
        public int To { get; }
    }
}