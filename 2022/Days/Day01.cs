using Common;

namespace _2022.Days
{
    public class Day01 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(nameof(Day01), " ");

            var calories = new List<int>();
            var elves = input.Select(x => new Elf(x.Trim().Split(" ").Select(int.Parse).ToList()));

            var elvesOrderedByCalories = elves.OrderByDescending(elf => elf.Total);

            int resultPartOne = elvesOrderedByCalories.First().Total;
            int resultPartTwo = elvesOrderedByCalories.Take(3).Sum(elf => elf.Total);

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
