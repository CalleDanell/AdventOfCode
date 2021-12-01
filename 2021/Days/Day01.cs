using System.Collections.Generic;
using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day01));
            var intInput = input.Select(int.Parse).ToArray();

            var resultPartOne = 0;
            for (var i = 0; i < intInput.Length - 1; i++)
            {
                if (intInput[i + 1] > intInput[i])
                    resultPartOne++;
            }

            var resultPartTwo = 0;

            var chunks = new List<List<int>>();
            for (var i = 0; i < intInput.Length - 1; i ++)
            {
                chunks.Add(new List<int>( intInput.Skip(i).Take(3)));
            }

            for (var i = 0; i < chunks.Count - 1; i++)
            {
                if (chunks[i + 1].Sum(x => x) > chunks[i].Sum(x => x))
                    resultPartTwo++;
            }

            return (nameof(Day01), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}