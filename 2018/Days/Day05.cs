using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day05 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetFullInput(nameof(Day05));

            var units = input.ToList();

            var currentChar = 'a';
            var shortestPolymer = int.MaxValue;
            while (currentChar != '}')
            {
                var unitCopy = new List<char>(units);
                unitCopy.RemoveAll(x => x == char.ToLower(currentChar) || x == char.ToUpper(currentChar));
                var polymer = RemoveReactiveUnits(unitCopy);
                currentChar++;
                if (polymer.Count < shortestPolymer)
                    shortestPolymer = polymer.Count;
            }

            var resultPartOne = RemoveReactiveUnits(units).Count;
            var resultPartTwo = shortestPolymer.ToString();

            return (nameof(Day05), resultPartOne.ToString(), resultPartTwo);
        }

        private IList<char> RemoveReactiveUnits(IList<char> units)
        {
            while (true)
            {
                for (var i = 0; i < units.Count - 1; i++)
                {
                    var current = units.ElementAt(i);
                    var next = units.ElementAt(i + 1);
                    if ((char.IsUpper(current) && char.IsLower(next) && char.ToLower(current) == next) ||
                        (char.IsLower(current) && char.IsUpper(next) && char.ToUpper(current) == next))
                    {
                        units.RemoveAt(i);
                        units.RemoveAt(i);
                        i -= 2;
                        if (i < 0)
                        {
                            i = -1;
                        }
                    }
                }

                return units;
            }
        }
    }
}
