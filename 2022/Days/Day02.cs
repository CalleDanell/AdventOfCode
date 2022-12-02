using Common;

namespace _2022.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var roundsPartOneRules = input.Select(x =>
            {
                return x
                    .Replace("A", "Rock")
                    .Replace("X", "Rock")
                    .Replace("B", "Paper")
                    .Replace("Y", "Paper")
                    .Replace("C", "Scissor")
                    .Replace("Z", "Scissor");
            }).Select(x => x.Split(" "));

            var roundsPartTwoRules = input.Select(x =>
            {
                {
                    var one = x.Substring(0, 1)
                        .Replace("A", "Rock")
                        .Replace("B", "Paper")
                        .Replace("C", "Scissor");
                    var two = x.Substring(2, 1);

                    // Lose
                    if (two.Equals("X"))
                    {
                        switch (one)
                        {
                            case "Rock": two = "Scissor"; break;
                            case "Paper": two = "Rock"; break;
                            case "Scissor": two = "Paper"; break;
                        }
                    }

                    // Win
                    if (two.Equals("Z"))
                    {
                        switch (one)
                        {
                            case "Rock": two = "Paper"; break;
                            case "Paper": two = "Scissor"; break;
                            case "Scissor": two = "Rock"; break;
                        }
                    }

                    // Draw
                    if (two.Equals("Y"))
                    {
                        two = one;
                    }

                    return $"{one} {two}";
                }
            }).Select(x => x.Split(" "));


            int resultPartOne = PlayTheGame(roundsPartOneRules);
            int resultPartTwo = PlayTheGame(roundsPartTwoRules);

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static int PlayTheGame(IEnumerable<string[]> rounds)
        {
            var totalScore = 0;
            foreach (var round in rounds)
            {
                var one = round.ElementAt(0);
                var two = round.ElementAt(1);
                var shapeScore = 0;
                var resultScore = 0;

                if (one.Equals(two))
                {
                    resultScore = 3;
                }

                if (two.Equals("Rock"))
                {
                    shapeScore = 1;
                    switch (one)
                    {
                        case "Paper": resultScore = 0; break;
                        case "Scissor": resultScore = 6; break;
                    }
                }

                if (two.Equals("Paper"))
                {
                    shapeScore = 2;
                    switch (one)
                    {
                        case "Scissor": resultScore = 0; break;
                        case "Rock": resultScore = 6; break;
                    }
                }

                if (two.Equals("Scissor"))
                {
                    shapeScore = 3;
                    switch (one)
                    {
                        case "Rock": resultScore = 0; break;
                        case "Paper": resultScore = 6; break;
                    }
                }

                totalScore += shapeScore + resultScore;
            }

            return totalScore;
        }
    }
}