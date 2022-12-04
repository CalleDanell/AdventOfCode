using Common;

namespace _2022.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var pairs = input.Select(x =>
                {
                    var parts = x.Split(',');
                    return new Pair((parts[0], parts[1]));
                });

            int resultPartOne = pairs.Count(x => x.Contains());
            int resultPartTwo = pairs.Count(x => x.AnyOverlaps());

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class Pair
    {
        public Pair((string,string) input)
        {
            First = GetRange(input.Item1);
            Second = GetRange(input.Item2);
        }

        public bool Contains()
        {
            var both = First.Concat(Second);
            var uniqueCount = new HashSet<int>(both).Count;

            if(uniqueCount == First.Count() || uniqueCount == Second.Count())
            {
                return true;
            }

            return false;
        }

        public bool AnyOverlaps()
        {
            if (First.Intersect(Second).Any())
            {
                return true;
            }

            return false;
        }


        public IEnumerable<int> First { get; }
        public IEnumerable<int> Second { get; }

        private static IEnumerable<int> GetRange(string section)
        {
            var parts = section.Split('-');
            var start = int.Parse(parts[0]);
            var end = int.Parse(parts[1]);
            return Enumerable.Range(start, end - start + 1);
        }
    }
}