using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day15 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByCommaSeparationAsync(nameof(Day15));
            var enumerable = input.ToList();
            var numbers1 = enumerable.Select(long.Parse).ToDictionary(x => x, y => new Queue<long>());
            var numbers2 = enumerable.Select(long.Parse).ToDictionary(x => x, y => new Queue<long>());

            var result1 = PlayTheGame(numbers1, 2020);
            var result2 = PlayTheGame(numbers2, 30000000);

            return (nameof(Day15), result1.ToString(), result2.ToString());
        }

        private long PlayTheGame(Dictionary<long, Queue<long>> numbers, int turns)
        {
            long spokenNumbers = 0;
            long lastSpokenNumber = -1;

            // Play the first round 
            foreach (var (key, _) in numbers)
            {
                numbers[key].Enqueue(spokenNumbers);
                lastSpokenNumber = key;
                spokenNumbers++;
            }

            while (spokenNumbers < turns)
            {
                var history = numbers[lastSpokenNumber];
                if (history.Count == 1)
                {
                    lastSpokenNumber = 0;
                }
                else
                {
                    var first = history.Dequeue();
                    var last = history.Peek();
                    lastSpokenNumber = last - first;
                }

                if (numbers.ContainsKey(lastSpokenNumber))
                {
                    numbers[lastSpokenNumber].Enqueue(spokenNumbers);
                }
                else
                {
                    var queue = new Queue<long>();
                    queue.Enqueue(spokenNumbers);
                    numbers.Add(lastSpokenNumber, queue);
                }

                spokenNumbers++;
            }

            return lastSpokenNumber;
        }
    }
}