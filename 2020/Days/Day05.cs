using Common;
using Common.Days;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day05 : IDay
    {
        const int PlaneRows = 128;
        const int PlaneColumns = 8;

        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var boardingPasses = input.Select(x => new BoardingPass(x, PlaneRows, PlaneColumns));

            var resultPartOne = boardingPasses.Max(x => x.GetSeatId());

            var missingRow = boardingPasses.GroupBy(x => x.Row).Where(x => x.Count() == 7).FirstOrDefault().Key;
            var missingColumn = Enumerable.Range(0, PlaneColumns).Except(boardingPasses.Where(x => x.Row == missingRow).Select(x => x.Column)).FirstOrDefault();
                        
            var resultPartTwo = missingRow * 8 + missingColumn;

            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class BoardingPass
    {
        public BoardingPass(string input, int planeRows, int planeColumns)
        {
            Row = FindSeat(new Queue(input.Take(7).ToList()), planeRows);
            Column = FindSeat(new Queue(input.TakeLast(3).ToList()), planeColumns);
        }

        public int Row { get; }

        public int Column { get; }

        public int GetSeatId()
        {
            return Row * 8 + Column;
        }

        private int FindSeat(Queue spacePartitioning, int spaces)
        {
            var spaceArray = Enumerable.Range(0, spaces).ToArray();
            while (spacePartitioning.Count > 0)
            {
                var back = spaceArray.Skip(spaceArray.Length / 2);
                var front = spaceArray.Take(spaceArray.Length / 2);
                var ins = spacePartitioning.Dequeue();
                if (ins.Equals('F') || ins.Equals('L'))
                {
                    spaceArray = front.ToArray();
                }
                else if (ins.Equals('B') || ins.Equals('R'))
                {
                    spaceArray = back.ToArray();
                }
            }
            return spaceArray[0];
        }
    }
}