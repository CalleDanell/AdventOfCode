using Common;

namespace _2023.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day02));

            var games = input.Select(x => new Game(x));
            var selectedMaxGames = games.Where(x => 
                x.GetMaxCount("red")  <= 12 &&
                x.GetMaxCount("green") <= 13 &&
                x.GetMaxCount("blue") <= 14
            );

            var sumOfMins = games.Sum(x =>
                x.GetMaxCount("red") *
                x.GetMaxCount("green") *
                x.GetMaxCount("blue")
            );

            return (nameof(Day02), selectedMaxGames.Sum(x => x.Id).ToString(), sumOfMins.ToString());
        }
    }


    public class Game
    {
        public Game(string input)
        {
            var parts = input.Split(':');
            var bags = parts[1].Split(";");

            Id = int.Parse(parts[0].Trim().Replace("Game", string.Empty).Trim());
            Revealed = bags.Select(x => new Revealed(x.Trim())).ToList();
        }

        public List<Revealed> Revealed { get; set; } = new List<Revealed>();

        public int GetMaxCount(string color)
        {
            return Revealed.Max(x => x.Counts[color]);
        }

        public int Id { get; set; }
    }

    public class Revealed
    {
        public Revealed(string input)
        {
            var parts = input.Split(',');

            foreach(var part in parts)
            {
                var secondParts = part.Trim().Split(" ");
                Counts[secondParts[1]] = int.Parse(secondParts[0]);
            }
        }

        public Dictionary<string, int> Counts { get; set; } = new Dictionary<string, int>() { { "red", 0 }, { "blue", 0 }, { "green", 0 } };

    }
}