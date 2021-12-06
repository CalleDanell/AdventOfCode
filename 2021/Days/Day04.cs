using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day04));

            var enumerable = input as List<string> ?? input.Where(x => !string.IsNullOrEmpty(x)).ToList();
            var draw = new Queue<int>(enumerable.ElementAt(0).Split(',').Select(int.Parse));

            enumerable.RemoveAt(0);

            var boards = new List<BingoBoard>();

            for (var i = 0; i < enumerable.Count; i +=5)
            {
                boards.Add(new BingoBoard(enumerable.Skip(i).Take(5).ToList()));
            }

            var winner = PlayToWin(draw, boards);
            var resultPartOne = winner.Sum * draw.Peek();

            var loser = FindLastWinner(draw, boards);
            var resultPartTwo = loser.Sum * draw.Peek();

            return (nameof(Day04), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static BingoBoard PlayToWin(Queue<int> draw, List<BingoBoard> boards)
        {
            BingoBoard winner = null;
            while (draw.TryPeek(out var number))
            {
                boards.ForEach(x => x.Mark(number));
                winner = boards.FirstOrDefault(x => x.Evaluate());
                if (winner != null)
                    break;

                draw.Dequeue();
            }

            return winner;
        }

        private static BingoBoard FindLastWinner(Queue<int> draw, List<BingoBoard> boards)
        {
            while(draw.TryPeek(out var number))
            {
                boards.ForEach(x => x.Mark(number));

                if (boards.Count == 1)
                {
                    var loser = boards.First();
                    loser.Mark(number);
                    if (loser.Evaluate())
                    {
                        return loser;
                    }
                }
                
                boards.RemoveAll(x => x.Evaluate());
                draw.Dequeue();
            }

            return null;
        }
    }

    public class BingoBoard
    {
        public BingoBoard(List<string> rows)
        {
            for (var i = 0; i < rows.Count; i++)
            {
                var row = rows[i].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToList();
                for (var j = 0; j < row.Count; j++)
                {
                    BingoSpots.Add(
                        new BingoSpot
                        {
                            X = j,
                            Y = i,
                            Value = int.Parse(row[j]),
                            Marked = false
                        }
                    );
                }
            }
        }

        public void Mark(int drawnNumber)
        {
            var match = BingoSpots.FirstOrDefault(x => x.Value == drawnNumber);
            if (match != null)
                match.Marked = true;
        }

        public bool Evaluate()
        {
            for (var i = 0; i < 5; i++)
            {
                var bingoRow = BingoSpots.Count(x => x.X == i && x.Marked) == 5;
                var bingoColumn = BingoSpots.Count(x => x.Y== i && x.Marked) == 5;

                if(bingoColumn || bingoRow)
                    return true;
            }

            return false;
        }

        public List<BingoSpot> BingoSpots { get; } = new List<BingoSpot>();

        public int Sum => BingoSpots.Where(x => !x.Marked).Sum(x => x.Value);
    }

    public class BingoSpot
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Value { get; set; }
        public bool Marked { get; set; }
    }
}