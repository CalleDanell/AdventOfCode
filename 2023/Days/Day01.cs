using Common;

namespace _2023.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day01));

            var sumPartOne = input.ToList().Select(x => new Calibration(x, true)).Sum(x => x.Number);
            var sumPartTwo = input.ToList().Select(x => new Calibration(x, false)).Sum(x => x.Number);

            return (nameof(Day01), sumPartOne.ToString(), sumPartTwo.ToString());
        }
    }

    public class Calibration
    {
        private readonly Dictionary<string,string> numbers = new Dictionary<string, string>
        { 
            { "one", "o1e" },
            { "two", "t2o" },
            { "three", "t3e" },
            { "four", "f4r" },
            { "five", "f5e" },
            { "six", "s6x" },
            { "seven", "s7n" },
            { "eight", "e8t" },
            { "nine", "n9e" },
        };

        public Calibration(string line, bool digits)
        {
            if(!digits)
            {
                line = ReplaceTextWithDigit(line);
            }

            CalibrateDigits(line);
        }

        private string ReplaceTextWithDigit(string line)
        {
            foreach(var text in numbers.Keys)
            {
                if (line.Contains(text))
                {
                    line = line.Replace(text, numbers[text]);
                }
            }

            return line;
        }

        private void CalibrateDigits (string line)
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    if (string.IsNullOrEmpty(First))
                    {
                        First = line[i].ToString();
                    }

                    Last = line[i].ToString();
                }
            }
        }

        private string First { get; set; } = string.Empty;

        private string Last { get; set; } = string.Empty;

        public int Number => int.Parse(First + Last);
    }
}