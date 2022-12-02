using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day06 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(nameof(Day06), " ");

            var enumerable = input.ToList();
            var noDuplicates = enumerable.Select(x => new HashSet<char>(x.Replace(" ", string.Empty).ToCharArray()));
            var resultPartOne = noDuplicates.Sum(x => x.Count);

            var resultPartTwo = 0;
            foreach(var group in enumerable)
            {
                var trimmedGrp = group.Trim();
                var peopleInGroup = trimmedGrp.Split(' ').Length;
                var yesPerQuestion = trimmedGrp.ToCharArray().GroupBy(x => x).Select(x => x.Count());
                resultPartTwo += yesPerQuestion.Count(x => x == peopleInGroup);
            }

            return (nameof(Day06), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }
}