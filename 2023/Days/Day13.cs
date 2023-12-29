using Common;

namespace _2023.Days
{
    public class Day13 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(nameof(Day13), "@");

            var sumOne = 0;
            var sumTwo = 0;

            foreach (var item in input) 
            {
                var reflectionMap = item.Split('@', StringSplitOptions.RemoveEmptyEntries);

                for(var y = 0;  y < reflectionMap.Length - 1; y++)
                {
                    var first = reflectionMap[y];
                    var second = reflectionMap[y + 1];

                    if(first.Equals(second))
                    {
                        if (IsHorizontalReflection(reflectionMap, y, false)) sumOne += (y + 1) * 100;
                        if (IsHorizontalReflection(reflectionMap, y, true)) sumTwo += (y + 1) * 100;
                    }

                    // Check if the smudge is causing initial reflection
                    if(Compare(new List<string> { first}, new List<string> { second}, true))
                    {
                        if (IsHorizontalReflection(reflectionMap, y, true)) sumTwo += (y + 1) * 100;
                    }
                }


                for (var x = 0; x < reflectionMap.First().Length - 1; x++)
                {
                    var first = new string(reflectionMap.SelectMany(row => row.Skip(x).Take(1)).ToArray());
                    var second = new string(reflectionMap.SelectMany(row => row.Skip(x + 1).Take(1)).ToArray());

                    if (first.Equals(second))
                    {
                        if (IsVerticalReflection(reflectionMap, x, false)) sumOne += (x + 1);
                        if (IsVerticalReflection(reflectionMap, x, true)) sumTwo += (x + 1);
                    }

                    // Check if the smudge is causing initial reflection
                    if (Compare(new List<string> { first }, new List<string> { second }, true))
                    {
                        if (IsVerticalReflection(reflectionMap, x, true)) sumTwo += (x + 1);
                    }
                }
            }
            
            return (nameof(Day13), sumOne.ToString(), sumTwo.ToString());
        }


        private static bool IsHorizontalReflection(string[] reflectionMap, int y, bool withSumdge)
        {
            var up = y;
            var down = y + 1;

            var top = new List<string>();
            var bottom = new List<string>();

            while (up >= 0 && down < reflectionMap.Length)
            {
                top.Add(reflectionMap[up]);
                bottom.Add(reflectionMap[down]);
                up--;
                down++;
            }

            return Compare(top, bottom, withSumdge);
        }

        private static bool IsVerticalReflection(string[] reflectionMap, int x, bool withSumdge)
        {
            var leftIndex = x;
            var rightIndex = x + 1;

            var left = new List<string>();
            var right = new List<string>();

            while (leftIndex >= 0 && rightIndex < reflectionMap.First().Length)
            {
                left.Add(new string(reflectionMap.SelectMany(row => row.Skip(leftIndex).Take(1)).ToArray()));
                right.Add(new string(reflectionMap.SelectMany(row => row.Skip(rightIndex).Take(1)).ToArray()));
                leftIndex--;
                rightIndex++;
            }

            return Compare(left, right, withSumdge);
        }

        private static bool Compare(IEnumerable<string> one, IEnumerable<string> two, bool withSmudge)
        {
            var missmatches = 0;
            for(var i = 0; i < one.Count(); i++)
            for(var j = 0; j < one.ElementAt(i).Length; j++)
            {
                if (!one.ElementAt(i).ElementAt(j).Equals(two.ElementAt(i).ElementAt(j)))
                {
                    missmatches++;
                }
            }

            if (missmatches == 1 && withSmudge) return true;
            if (missmatches == 0 && !withSmudge) return true;

            return false;
        }
    }
}
