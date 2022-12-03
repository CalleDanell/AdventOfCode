using Common;

namespace _2022.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);
            var backpacks = input.ToList();

            var singleDuplicates = new List<char>();
            foreach (var item in input)
            {
                var offset = item.Length / 2;
                var compartmentOne = item.Substring(0, offset);
                var compartmentTwo = item.Substring(offset, item.Length - offset);
                singleDuplicates.Add(FindDuplicate(compartmentOne, compartmentTwo));
            }

            var trippleDuplicates = new List<char>();
            for (var i = 0; i < backpacks.Count; i+=3)
            {
                var first = backpacks.ElementAt(i);
                var second = backpacks.ElementAt(i + 1);
                var third = backpacks.ElementAt(i + 2);
                trippleDuplicates.Add(FindDuplicate(first, second, third));
            }

            int resultPartOne = singleDuplicates.Select(ConvertChar).Sum();
            int resultPartTwo = trippleDuplicates.Select(ConvertChar).Sum();

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private char FindDuplicate(params string[] backpacks)
        {
            var firstBackpack = backpacks.First().ToList();
            for (var i = 0; i < firstBackpack.Count; i++)
            {
                var current = firstBackpack.ElementAt(i);
                var isDuplicate = true;
                for(var j = 1; j < backpacks.Length; j++)
                {
                    if (!backpacks[j].Contains(current))
                    {
                        isDuplicate = false;
                    }
                }

                if(isDuplicate)
                    return current;
            }

            throw new Exception("No duplicates found");
        }

        private int ConvertChar(char c)
        {
            if(char.IsUpper(c))
            {
                return Convert.ToInt32(c) - 38; // Starts at 27
            } 
            
            return Convert.ToInt32(c) - 96; // Starts at 1

        }
    }
}