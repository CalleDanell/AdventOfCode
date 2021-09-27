using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day02));
            var enumerable = input as string[] ?? input.ToArray();
            var boxes = enumerable.Select(x => new Box(x)).ToList();

            var boxesWithTwo = GetBoxesByLetterGroup(boxes, 2);
            var boxesWithThree = GetBoxesByLetterGroup(boxes, 3);

            var resultPartOne = boxesWithTwo.Count() * boxesWithThree.Count();

            var matchingBox = GetTheMatchingBox(boxes);
            var resultPartTwo = matchingBox.Code.Remove(matchingBox.LastMissMatchingIndex);

            return (nameof(Day02), resultPartOne.ToString(), resultPartTwo);
        }

        private static IEnumerable<Box> GetBoxesByLetterGroup(IList<Box> boxes, int groupSize)
        {
            var boxesWithTwoOrThree = new List<Box>();
            foreach (var box in boxes)
            {
                var letterCounts = box.LetterGroups.Select(g => new { Letter = g.Key, Count = g.Count() }).ToList();
                if (letterCounts.Any(x => x.Count == groupSize))
                    boxesWithTwoOrThree.Add(box);
            }

            return boxesWithTwoOrThree;
        }

        private static Box GetTheMatchingBox(IList<Box> boxes)
        {
            for (var i = 0; i < boxes.Count; i++)
            {
                for (var j = 0; j < boxes.Count; j++)
                {
                    if (boxes[i].Compare(boxes[j]))
                    {
                        return boxes[i];
                    }
                }
            }

            return null;
        }
    }

    public class Box
    {
        public string Code;
        public Box(string code)
        {
            Code = code;
        }

        public char[] GetCode => Code.ToCharArray();

        public int CodeLength => Code.Length;

        public int LastMissMatchingIndex { get; set; }

        public bool Compare(Box box)
        {
            var missMatches = 0;
            for (var i = 0; i < box.CodeLength; i++)
            {
                if (box.GetCode[i] != Code[i])
                {
                    missMatches++;
                    LastMissMatchingIndex = i;
                }

                if (missMatches > 1)
                    return false;
            }

            return missMatches != 0;
        }

        public IEnumerable<IGrouping<char,char>> LetterGroups => Code.GroupBy(x => x);
    }
}
