using Common;

namespace _2022.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day01));

            var calories = new List<int>();
            var elves = new List<Elf>();
            foreach(var line in input)
            {
                if(string.IsNullOrEmpty(line))
                {
                    elves.Add(new Elf(calories));
                    calories.Clear();
                } 
                else
                {
                    calories.Add(int.Parse(line));
                }
            }

            int resultPartOne = elves.OrderByDescending(elf => elf.Total).First().Total;
            int resultPartTwo = elves.OrderByDescending(elf => elf.Total).Take(3).Sum(elf => elf.Total);

            return (nameof(Day01), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class Elf
    {
        private List<int> calories;

        public Elf(List<int> calories)
        {
            this.calories = new List<int>(calories);
        }

        public int Total => calories.Sum();
    }
}
