using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetFullInput(nameof(Day01));

            var digits = input.Select(x => int.Parse(x.ToString())).ToList();

            var sum1 = 0;
            //for(var i = 0; i < digits.Count(); i++)
            //{
            //    var curr = digits[i];
            //    var next = 0;
            //    if(i != digits.Count - 1)
            //    {
            //        next = digits[i + 1];
            //    } else
            //    {
            //        next = digits[0];
            //    }

            //    if(curr == next)
            //    {
            //        sum1 += curr;
            //    }
            //}

            var sum2 = 0;
            var steps = digits.Count() / 2;
            for (var i = 0; i < digits.Count(); i++)
            {
                var curr = digits[i];
                var nextIndex = i + steps;

                var diff = digits.Count() - nextIndex;
                if (diff < 0)
                {
                    nextIndex = diff;
                } 


                if (curr == digits[nextIndex])
                {
                    sum2 += curr;
                }
            }

            return (nameof(Day01), sum1.ToString(), sum2.ToString());
        }
    }
}
