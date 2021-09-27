using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day04));
            var observations = input.Select(x => new
                Observation(x.Substring(1, 16),
                    x.Split(']')[1]))
                .OrderBy(x => x.FullDate);

            var guards = GenerateGuards(observations).ToList();

            var sleepiestGuard = guards.OrderByDescending(x => x.GetMinutesAsleep()).First();
            var minuteMostAsleep = sleepiestGuard.GetMinuteMostAsleep();
            
            var consistentSleepScheduleGuard = guards.Select(x =>
            {
                var minutesAsleep = x.GetMinuteMostAsleep();
                return new
                {
                    x.Id,
                    MaxMinAsleep = minutesAsleep.Item1,
                    Minute = minutesAsleep.Item2
                };
            }).OrderByDescending(x => x.MaxMinAsleep).First();

            var resultPartOne = sleepiestGuard.Id * minuteMostAsleep.Item2;
            var resultPartTwo = consistentSleepScheduleGuard.Id * consistentSleepScheduleGuard.Minute;

            return (nameof(Day04), resultPartOne.ToString(), resultPartTwo.ToString());
        
        }

        private static IEnumerable<Guard> GenerateGuards(IOrderedEnumerable<Observation> observations)
        {
            var guards = new List<Guard>();
            Guard guard = null;
            foreach (var o in observations)
            {
                if (o.Action.Contains("Guard"))
                {
                    var id = int.Parse(o.Action.Split('#')[1].Split(null)[0]);
                    guard = guards.FirstOrDefault(x => x.Id == id);
                    if (guard != null)
                    {
                        guard.ResetMinute();
                    }
                    else
                    {
                        guard = new Guard(id);
                        guards.Add(guard);
                    }
                }
                else
                {
                    guard?.AddObservation(o);
                }
            }

            return guards;
        }

        public class Guard
        {
            public int? Id;
            private readonly int[] observations = new int[60];
            private int currentMinute = -1;

            public Guard(int? id)
            {
                this.Id = id;
            }

            public void ResetMinute() => currentMinute = -1;

            public void AddObservation(Observation observation)
            {
                switch (observation.Action)
                {
                    case "falls asleep":
                        currentMinute += observation.Time - currentMinute;
                        break;
                    case "wakes up":
                        for (var i = currentMinute; i < observation.Time; i++)
                        {
                            observations[i]++;
                            currentMinute++;
                        }
                        break;
                }
            }

            public int GetMinutesAsleep() => observations.Where(x => x > 0).Sum(x => x);

            public (int,int) GetMinuteMostAsleep() => observations.Select((x, i) => (x, i)).Max(x => x);

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType()) return false;
                var g = (Guard)obj;
                return Id == g.Id;
            }

            public override int GetHashCode()
            {
                return new Guard(0).GetHashCode();
            }
        }

        public class Observation
        {
            public string Date { get; }
            public int Time { get; }
            public string Action { get; }
            public string FullDate { get; }

            public Observation(string date, string action)
            {
                FullDate = date;
                Date = date.Substring(0,10).Trim();
                Time = int.Parse(date.Substring(14,2));
                Action = action.Trim();
            }
        }
    }
}
