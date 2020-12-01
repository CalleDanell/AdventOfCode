using Common;
using Common.Days;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day01 : IDay
    {
        const int result = 2020;

        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);
            var intInput = input.Select(int.Parse).ToArray();

            var resultPartOne = FindTwo(intInput);
            var resultPartTwo = FindThree(intInput);
            
            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static int FindTwo(int [] values)
        {
            for(var i = 0; i < values.Length; i++) 
                for(var j = 0; j < values.Length; j++)
                {
                    if(values[i] + values[j] == result)
                    {
                        return values[i] * values[j];
                    }
                }

            return 0;
        }

        private static int FindThree(int[] values)
        {
            for (var i = 0; i < values.Length; i++)
                for (var j = 0; j < values.Length; j++)
                    for (var k = 0; k < values.Length; k++)
                    {
                        if (values[i] + values[j] + values[k] == result)
                        {
                            return values[i] * values[j] * values[k];
                        }
                    }

            return 0;
        }
    }
}