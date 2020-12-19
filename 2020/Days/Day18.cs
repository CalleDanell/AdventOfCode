using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{

    /// <summary>
    /// Got stuck so took similar (but better) solution from here:
    /// https://github.com/encse/adventofcode/blob/master/2020/Day18/Solution.cs
    /// </summary>
    public class Day18 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day18));

            var enumerable = input.ToList();
            var result1 = DoMath(enumerable, true);
            var result2 = DoMath(enumerable, false);

            return (nameof(Day18), result1.ToString(), result2.ToString());
        }

        private long DoMath(IEnumerable<string> input, bool part1)
        {
            long sum = 0;
            foreach (var line in input)
            {
                var opStack = new Stack<char>();
                var valStack = new Stack<long>();

                opStack.Push('(');

                foreach (var ch in line)
                {
                    switch (ch)
                    {
                        case ' ':
                            break;
                        case '*':
                            Evaluate("(", opStack, valStack);
                            opStack.Push('*');
                            break;
                        case '+':
                            if (part1)
                            {
                                Evaluate("(", opStack, valStack);
                            }
                            else
                            {
                                Evaluate("(*", opStack, valStack);
                            }
                            opStack.Push('+');
                            break;
                        case '(':
                            opStack.Push('(');
                            break;
                        case ')':
                            Evaluate("(", opStack, valStack);
                            opStack.Pop();
                            break;
                        default:
                            valStack.Push(long.Parse(ch.ToString()));
                            break;
                    }
                }

                Evaluate("(", opStack, valStack);

                sum += valStack.Single();
            }

            return sum;
        }


        private void Evaluate(string ops, Stack<char> opStack, Stack<long> valStack)
        {
            while (!ops.Contains(opStack.Peek()))
            {
                if (opStack.Pop() == '+')
                {
                    valStack.Push(valStack.Pop() + valStack.Pop());
                }
                else
                {
                    valStack.Push(valStack.Pop() * valStack.Pop());
                }
            }
        }
    }
}