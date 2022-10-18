using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day21 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day21));

            var player1 = new Player { Id = 1, Position = int.Parse(input.ElementAt(0)[input.ElementAt(0).Length - 1].ToString()) };
            var player2 = new Player { Id = 2, Position = int.Parse(input.ElementAt(1)[input.ElementAt(1).Length - 1].ToString()) };

            var players = new List<Player> { player1, player2 };

            var dice = new Dice(100);
            var board = new Board(10);

            var resultPartOne = Play(players, dice, board);

            return (nameof(Day21), resultPartOne.ToString(), string.Empty);
        }

        public int Play(List<Player>  players, Dice dice, Board board)
        {
            while(players.Any(p => p.Score < 1000))
            {
                foreach(var player in players)
                {
                    var diceRoll = dice.Roll(3);
                    var newPosition = board.Move(player.Position, diceRoll);
                    player.Position = newPosition;
                    player.Score += newPosition;

                    if (player.Score >= 1000)
                        return dice.Rolls * players.First(x => x.Id != player.Id).Score;
                }
            }

            return 0;
        }
    }

    public class Dice
    {
        private Queue<int> options = new Queue<int>();
        public int Rolls = 0;
        public Dice(int size)
        {
            for(var i = 1; i <= size; i++)
            {
                options.Enqueue(i);
            }
        }

        public int Roll(int times)
        {
            var result = 0;
            for (var i = 0; i < times; i++)
            {
                var value = options.Dequeue();
                result += value;
                options.Enqueue(value);
                Rolls++;
            }
            
            return result;
        }
    }

    public class Player
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public int Score { get; set; } = 0;
    }

    public class Board
    {
        private LinkedList<int> squares = new LinkedList<int>();

        public Board(int size)
        {
            for(var i = 1; i <= size; i++)
            {
                squares.AddLast(i);
            }
        }

        public int Move(int startValue, int steps)
        {
            var current = squares.First;
            while (current.Value != startValue)
            {
                current = current.Next;
            }

            while(steps > 0)
            {
                current = current.Next ?? current.List.First;
                steps--;
            }

            return current.Value;
        }
    }
}