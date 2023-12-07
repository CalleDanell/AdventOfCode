using Common;

namespace _2023.Days
{
    public class Day06 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day06));
            var parsedInput = input.Select(x => x.Split(':')[1].Trim().Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse));

            var races = new List<Race>();
            for(var i = 0; i < parsedInput.ElementAt(0).Count(); i++)
            {
                races.Add(new Race(parsedInput.ElementAt(0).ElementAt(i), parsedInput.ElementAt(1).ElementAt(i)));
            }

            var time = string.Join(string.Empty, parsedInput.ElementAt(0));
            var record = string.Join(string.Empty, parsedInput.ElementAt(1));

            var longRace = new Race(long.Parse(time), long.Parse(record));


            return (nameof(Day06), races.Aggregate(1, (x, y) => x * y.GetNumberOfWins()).ToString(), longRace.GetNumberOfWins().ToString());
        }

        public class Race
        {
            public Race(long time, long record)
            {
                Time = time;
                Record = record;
            }

            public int GetNumberOfWins()
            {
                var waysToWin = 0;
                for (var j = 0; j < Time; j++)
                {
                    var speed = j;
                    var distanceTravelled = (Time - j) * speed;

                    if (distanceTravelled > Record)
                        waysToWin++;
                }

                return waysToWin;
            }

            public long Time { get; }
            public long Record { get; }
        }
    }
}