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
            var input = await InputHandler.GetInputByLineAsync(nameof(Day01));

            var frequencyList = input.Select(x => new Frequency
            {
                Value = int.Parse(x[1..]),
                Operator = x[0].ToString()
            }).ToList();

            var resultPartOne = frequencyList.Where(x => x.Operator.Equals("+")).Select(x => x.Value).Sum() - frequencyList.Where(x => x.Operator.Equals("-")).Select(x => x.Value).Sum();

            var previous = new List<int>();
            
            var resultPartTwo = GetFirstDuplicatedFrequency(frequencyList, previous);

            return (nameof(Day01), resultPartOne.ToString(), resultPartTwo);
        }

        private static string GetFirstDuplicatedFrequency(IEnumerable<Frequency> frequencyList, ICollection<int> previous)
        {
            var linkedFrequencies = new LinkedList<Frequency>(frequencyList);
            var currentNode = linkedFrequencies.First;
            var currentValue = 0;

            while (currentNode != null)
            {
                if (currentNode.Value.Operator.Equals("+"))
                {
                    currentValue += currentNode.Value.Value;
                }
                else
                {
                    currentValue -= currentNode.Value.Value;
                }

                if (previous.Contains(currentValue))
                    break;

                previous.Add(currentValue);

                currentNode = currentNode.Next ?? linkedFrequencies.First;
            }

            return currentValue.ToString();
        }
    }

    public class Frequency
    {
        public int Value { get; set; }

        public string Operator { get; set; }
    }
}
