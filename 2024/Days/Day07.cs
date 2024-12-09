using Common;

namespace _2024.Days
{
    public class Day07 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var dict = new Dictionary<long, List<long>>();
            foreach (var row in input)
            {
                var parts = row.Split(':');
                dict[long.Parse(parts[0])] = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToList();
            }

            var partOne = FindSumOfValid(dict, false);
            var partTwo = FindSumOfValid(dict, true);

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private long FindSumOfValid(Dictionary<long, List<long>> dict, bool withConcatOperator)
        {
            long sum = 0;
            foreach (var kvp in dict)
            {
                var results = new List<long>();
                FindPermutations(kvp.Value, kvp.Key, kvp.Value[0], 0, results, withConcatOperator);
                if (results.Contains(kvp.Key))
                {
                    sum += kvp.Key;
                }
            }

            return sum;
        }

        private void FindPermutations(List<long> numbers, long stop, long current, int index, List<long> results, bool withConcatOperator) 
        {
            if(current > stop)
            {
                return; 
            }
            if (numbers.Count == index + 1)
            {
                results.Add(current);
                return;
            }
            index++;
            FindPermutations(numbers, stop, current * numbers[index], index, results, withConcatOperator);
            FindPermutations(numbers, stop, current + numbers[index], index, results, withConcatOperator);
            if(withConcatOperator)
            {
                var concat = long.Parse(current.ToString() + numbers[index].ToString());
                FindPermutations(numbers, stop, concat, index, results, withConcatOperator);
            }
        }
    }
}