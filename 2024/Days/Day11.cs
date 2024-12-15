using Common;

namespace _2024.Days
{
    public class Day11 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetFullInput(day);

            var stones = input.Split(" ").Select(long.Parse).ToDictionary(x => x, y => 1L);

            var partOne = Blink(stones, 25);
            var partTwo = Blink(stones, 75);

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static long Blink(Dictionary<long, long> stones, int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                var newStones = new Dictionary<long, long>();
                foreach (var stone in stones)
                {
                    if (stone.Key == 0)
                    {
                        newStones.TryAdd(1, 0);
                        newStones[1] += stone.Value;
                        continue;
                    }

                    var strNum = stone.Key.ToString();
                    if (strNum.Length % 2 == 0)
                    {
                        newStones.TryAdd(long.Parse(strNum[..(strNum.Length / 2)]), 0);
                        newStones[long.Parse(strNum[..(strNum.Length / 2)])] += stone.Value;
                        newStones.TryAdd(long.Parse(strNum[(strNum.Length / 2)..]), 0);
                        newStones[long.Parse(strNum[(strNum.Length / 2)..])] += stone.Value;
                        continue;
                    }

                    newStones.TryAdd(stone.Key * 2024, 0);
                    newStones[stone.Key * 2024] += stone.Value;
                }

                stones = newStones;
            }

            return stones.Select(x => x.Value).Sum();
        }
    }
}