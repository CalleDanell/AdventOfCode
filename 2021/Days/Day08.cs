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

            var uniqueSegments = displays.SelectMany(x => x.OutputCodes.Where(s =>
                s.Length == 2 ||
                s.Length == 3 ||
                s.Length == 4 ||
                s.Length == 7
                ));

            var resultPartOne = uniqueSegments.Count();
            var resultPartTwo = displays.Sum(d => d.CalculateOutput());

            return (this.GetType().Name, resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class Display
    {
        private string[] codes;
        private string[] outputCodes;

        public Display(string input)
        {
            var parts = input.Split('|');
            codes = DecodeSignalPattern(parts[0]);
            outputCodes = parts[1].Trim().Split(' ');
        }

        public int CalculateOutput()
        {
            string result = string.Empty;
            foreach (var output in outputCodes)
            {
                for(var i = 0; i < codes.Length; i++)
                {
                    var orderedOutput = new string(output.OrderBy(x => x).ToArray());
                    var orderedCode = new string(codes[i].OrderBy(x => x).ToArray());
                    if(orderedOutput.Equals(orderedCode))
                    {
                        result += i;
                    }
                }
            }

            return int.Parse(result);
        }

        public string[] OutputCodes => outputCodes;

        private string[] DecodeSignalPattern(string pattern)
        {
            var codes = new string[10];

            var fiveCountCodeList = new List<string>();
            var sixCountCodeList = new List<string>();

            foreach (var code in pattern.Split(' '))
            {
                switch (code.Length)
                {
                    case 2:
                        codes[1] = code;
                        break;
                    case 3:
                        codes[7] = code;
                        break;
                    case 4:
                        codes[4] = code;
                        break;
                    case 7:
                        codes[8] = code;
                        break;
                    case 5: //2,3,5
                        fiveCountCodeList.Add(code);
                        break;
                    case 6: //0,6,9
                        sixCountCodeList.Add(code);
                        break;
                }
            }

            foreach (var fiveLen in fiveCountCodeList)
            {
                if (fiveLen.Except(codes[1]).Count() == 3)
                {
                    codes[3] = fiveLen;
                }
                else if (codes[8].Except(fiveLen).Except(codes[4]).Count() == 1)
                {
                    codes[5] = fiveLen;
                } 
                else
                {
                    codes[2] = fiveLen;
                }
            }

            foreach (var sixLen in sixCountCodeList)
            {
                if (sixLen.Except(codes[1]).Count() == 5)
                {
                    codes[6] = sixLen;
                }
                else if (sixLen.Except(codes[4]).Count() == 2)
                {
                    codes[9] = sixLen;
                }
                else
                {
                    codes[0] = sixLen;
                }
            }

            return codes;
        }
    }
}