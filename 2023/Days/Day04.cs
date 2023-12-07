using Common;

namespace _2023.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day04));
            var cards = input.Select(x => new Card(x)).ToDictionary(x => x.Id, x => x);

            var points = GetPointsForAllCards(cards.Values);

            var numberOfCards = cards.Count();
            numberOfCards += GetNumberOfCopies(cards);

            return (nameof(Day04), points.ToString(), numberOfCards.ToString());
        }

        private static int GetNumberOfCopies(Dictionary<int, Card> cards)
        {
            int count = 0;
            foreach (var currentCard in cards.Values)
            {
                var copies = GetCopies(cards, currentCard);

                while (copies.TryDequeue(out var copy))
                {
                    count++;
                    foreach (var c in GetCopies(cards, copy))
                    {
                        copies.Enqueue(c);
                    }
                }
            }

            return count;
        }

        private static Queue<Card> GetCopies(Dictionary<int,Card> cards, Card currentCard)
        {
            var winningNumbers = currentCard?.Numbers.Intersect(currentCard.Winners);
            var copies = new Queue<Card>();
            for (var i = 1; i < winningNumbers?.Count() + 1; i++)
            {
                copies.Enqueue(cards[currentCard.Id + i]);
            }

            return copies;
        }

        private static int GetPointsForAllCards(IEnumerable<Card> cards)
        {
            int points = 0;
            foreach (var card in cards)
            {
                var intersection = card.Numbers.Intersect(card.Winners);
                var cardPoints = intersection.Count() == 0 ? 0 : 1;
                for (var i = 1; i < intersection.Count(); i++)
                {
                    cardPoints *= 2;
                }

                points += cardPoints;
            }

            return points;
        }
    }

    public class Card
    {
        public Card(string input) 
        {
            var parts = input.Split(':');
            Id = int.Parse(parts[0].Replace("Card ", string.Empty));
            var sequences = parts[1].Split("|");

            Winners.AddRange(sequences[0].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse));
            Numbers.AddRange(sequences[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse));
        }

        public int Id { get; private set; }
        public List<int> Winners { get; private set; } = new List<int>();
        public List<int> Numbers { get; private set; } = new List<int>();
    }
}