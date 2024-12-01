using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day09 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetFullInput(nameof(Day09));
            var inputParts = input.Split(" ");

            var playerCount = int.Parse(inputParts[0]);
            var endValue = int.Parse(inputParts[6]);

            var playersGameOne = GeneratePlayers(playerCount);
            var playersGameTwo = GeneratePlayers(playerCount);

            PlayTheGame(playerCount, endValue, playersGameOne);
            PlayTheGame(playerCount, endValue * 100, playersGameTwo);

            return (nameof(Day09), playersGameOne.Values.Max().ToString(), playersGameTwo.Values.Max().ToString());
        }

        private static void PlayTheGame(int playerCount, int endValue, Dictionary<int, long> players)
        {
            // Add the first node 
            var circle = new LinkedList<int>();
            circle.AddFirst(0);

            var currentMarble = circle.First;
            var currentPlayerId = 1;
            var currentMarbleValue = 1;
            
            while (currentMarbleValue < endValue)
            {
                if ((double)currentPlayerId / playerCount > 1) currentPlayerId = 1;

                if (currentMarbleValue % 23 == 0)
                {
                    for (var i = 0; i < 7; i++)
                    {
                        currentMarble = currentMarble.Previous;

                        if (currentMarble == null)
                        {
                            currentMarble = circle.Last;
                        }
                    }

                    players[currentPlayerId] += currentMarble.Value + currentMarbleValue;
                    var temp = currentMarble.Next;
                    circle.Remove(currentMarble);
                    currentMarble = temp;
                }
                else
                {
                    var next = currentMarble.Next;
                    if (next == null)
                    {
                        next = circle.First;
                    }

                    currentMarble = circle.AddAfter(next, currentMarbleValue);
                }

                currentMarbleValue++;
                currentPlayerId++;
            }
        }

        private static Dictionary<int, long> GeneratePlayers(int playerCount)
        {
            var players = new Dictionary<int, long>();
            for (var i = 1; i <= playerCount; i++)
            {
                players.Add(i, 0);
            }

            return players;
        }
    }
}
