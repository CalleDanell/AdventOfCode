using Common;

namespace _2023.Days
{

    public class Day07 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day07));
            
            var cards = input.Select(x => new Hand(x, false)).ToList();
            var cardsWithJoker = input.Select(x => new Hand(x, true)).ToList();

            var count = GetRankings(cards);
            var countWithJoker = GetRankings(cardsWithJoker);

            return (nameof(Day07), count.ToString(), countWithJoker.ToString());
        }

        private static int GetRankings(List<Hand> cards)
        {
            var sortedCards = cards.OrderBy(x => x.Ranking).ThenBy(x => x.Cards);

            var count = 0;
            for (var i = 1; i < sortedCards.Count() + 1; i++)
            {
                count += sortedCards.ElementAt(i - 1).Bid * i;
            }

            return count;
        }

        public class Hand
        {
            public Hand(string line, bool jokers)
            {
                var parts = line.Split(' ');
                Bid = int.Parse(parts[1]);

                var cards = new List<char>();
                foreach(var card in parts[0])
                {
                    char cardWithCorrectValue;
                    switch(card)
                    {
                        case 'A':
                            cardWithCorrectValue = 'E';
                            break;
                        case 'K':
                            cardWithCorrectValue = 'D';
                            break;
                        case 'Q':
                            cardWithCorrectValue = 'C';
                            break;
                        case 'J':
                            if(jokers)
                            {
                                cardWithCorrectValue = '1';
                            } 
                            else
                            {
                                cardWithCorrectValue = 'B';
                            }
                            break;
                        case 'T':
                            cardWithCorrectValue = 'A';
                            break;
                        default:
                            cardWithCorrectValue = card;
                            break;
                    }

                    cards.Add(cardWithCorrectValue);
                    if(!Pairs.TryAdd(cardWithCorrectValue, 1))
                    {
                        Pairs[cardWithCorrectValue]++;
                    }
                }

                Cards = string.Join(string.Empty, cards);

                if(jokers)
                {
                    IntroduceJoker();
                    SetHandRanking();
                } 
                else
                {
                    SetHandRanking();
                }
            }

            public Dictionary<char, int> Pairs { get; private set; } = new Dictionary<char, int>();
            public string Cards { get; private set; }
            public int Bid { get; }

            public int Ranking { get; private set; }

            private void SetHandRanking()
            {
                var numberOfSingleCards = Pairs.Where(x => x.Value == 1).Select(x => x.Key).Count();
                if (Pairs.Count == 1) { // five of a kind
                    Ranking = 6;
                } 
                else if(Pairs.Count == 2 && numberOfSingleCards == 1) // four of a kind
                {
                    Ranking = 5;
                }
                else if (Pairs.Count == 2 && numberOfSingleCards == 0) // full house
                {
                    Ranking = 4;
                }
                else if (Pairs.Count == 3 && numberOfSingleCards == 2) // three of a kind
                {
                    Ranking = 3;
                }
                else if (Pairs.Count == 3 && numberOfSingleCards == 1) // two pairs
                {
                    Ranking = 2;
                }
                else if (Pairs.Count == 4 && numberOfSingleCards == 3) // one pairs
                {
                    Ranking = 1;
                }
                else // high card
                {
                    Ranking = 0;
                }
            }

            public void IntroduceJoker()
            {
                if(Pairs.TryGetValue('1', out var count))
                {
                    if(count != 5)
                    {
                        Pairs.Remove('1');
                        var largestPairKey = Pairs.Where(x => x.Key != '1').OrderByDescending(x => x.Value).First().Key;
                        Pairs[largestPairKey] += count;
                    }
                }
            }
        }
    }
}