using System.Linq;
using Common;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace _2021.Days
{
    public class Day08 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day08));

            var displays = input.Select(x => new Display(x)).ToList();

            var uniqueSegments = displays.SelectMany(x => x.Output.Where(s =>
                s.Digit == 1 ||
                s.Digit == 4 ||
                s.Digit == 7 ||
                s.Digit == 8
                ));

            var resultPartOne = uniqueSegments.Count();

            var resultPartTwo = 0;

            return (nameof(Day08), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class Display
    {
        public Display(string input)
        {
            var parts = input.Split('|');
            SignalPattern = new List<SignalPattern>(parts[0].Split(' ').Select(DecodeSignalPattern).ToList());
            Output = new List<SignalPattern>(parts[1].Split(' ').Select(DecodeSignalPattern).ToList());
        }

        public List<SignalPattern> Output { get; }

        public List<SignalPattern> SignalPattern { get; }

        private SignalPattern DecodeSignalPattern(string pattern)
        {
            var digit = 0;
            switch (pattern.Length)
            {
                case 2:
                    digit = 1;
                    break;
                case 3:
                    digit = 7;
                    break;
                case 4:
                    digit = 4;
                    break;
                case 7:
                    digit = 8;
                    break;
//                case 6:
                    // 
            }

            return new SignalPattern
            {
                Digit = digit,
                Pattern = pattern
            };
        }
    }

    public class SignalPattern
    {
        public int Digit { get; set; }

        public string Pattern { get; set; }
    }
}