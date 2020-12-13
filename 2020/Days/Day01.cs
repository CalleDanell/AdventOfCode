using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day01 : IDay
    {
        private const int Result = 2020;

        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day01));
            var intInput = input.Select(int.Parse).ToArray();

            var resultPartOne = FindTwo(intInput);
            var resultPartTwo = FindThree(intInput);
            
            return (nameof(Day01), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static int FindTwo(int [] values)
        {
            for(var i = 0; i < values.Length; i++) 
                for(var j = 0; j < values.Length; j++)
                {
                    if(values[i] + values[j] == Result)
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
                        if (values[i] + values[j] + values[k] == Result)
                        {
                            return values[i] * values[j] * values[k];
                        }
                    }

            return 0;
        }
    }
}