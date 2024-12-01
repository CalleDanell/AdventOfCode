using Common;

namespace _2024.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var listOne = new List<int>();
            var listTwo = new List<int>();
            foreach(var item in input)
            {
                var parts = item.Split("  ");
                listOne.Add(int.Parse(parts[0]));
                listTwo.Add(int.Parse(parts[1]));
            }

            listOne.Sort();
            listTwo.Sort();
            var sum = 0;
            var score = 0;
            for(var i = 0; i < listOne.Count; i++)
            {
                var first = listOne[i];
                var second = listTwo[i];
                sum += Math.Max(first, second) - Math.Min(first, second);
                score += first * listTwo.Count(x => x == first);

            }

            var partOne = sum;
            var partTwo = score;
            return (day, partOne.ToString(), partTwo.ToString());
        }

    }
}