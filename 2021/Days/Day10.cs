using System.Linq;
using Common;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Globalization;

namespace _2021.Days
{
    public class Day10 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day10));
            var subsystems = input.ToList();

            var corruptedScore = 0;
            var incompleteScores = new List<decimal>();
            foreach (var subsystem in subsystems)
            {
                var expected = new Stack<char>();
                var incomplete = true;
                foreach (var c in subsystem.ToCharArray())
                {
                    if (c == '(')
                    {
                        expected.Push(')');
                    }
                    else if (c == '[')
                    {
                        expected.Push(']');
                    }
                    else if (c == '{')
                    {
                        expected.Push('}');
                    } 
                    else if (c == '<')
                    {
                        expected.Push('>');
                    }
                    else
                    {
                        if (expected.Peek() != c)
                        {
                            corruptedScore += GetSyntaxErrorScore(c);
                            incomplete = false;
                            break;
                        }

                        expected.Pop();
                    }
                }

                if (incomplete)
                {
                    decimal incompleteScore = 0;
                    while (expected.TryPop(out var c))
                    {
                        incompleteScore = incompleteScore * 5 + GetIncompleteScore(c);
                    }

                    incompleteScores.Add(incompleteScore);
                }
            }

            var resultPartOne = corruptedScore;
            var resultPartTwo = incompleteScores.OrderByDescending(x => x).ElementAt(incompleteScores.Count / 2);

            return (nameof(Day09), resultPartOne.ToString(), resultPartTwo.ToString(CultureInfo.InvariantCulture));
        }

        private static int GetIncompleteScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 1;
                case ']':
                    return 2;
                case '}':
                    return 3;
                case '>':
                    return 4;
                default:
                    return 0;
            }
        }

        private static int GetSyntaxErrorScore(char c)
        {
            switch (c)
            {
                case ')':
                    return 3;
                case ']':
                    return 57;
                case '}':
                    return 1197;
                case '>':
                    return 25137;
                default:
                    return 0;
            }
        }
    }
}