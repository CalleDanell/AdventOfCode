using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day16 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day16));
            
            var ticketInput = input.Where(x => !string.IsNullOrEmpty(x)).ToList();
            var ticketFields = SetupRangesForTicketFields(ticketInput);
            var (validTickets, errorRate) = FindErrorRateAndGetValidTickets(ticketInput, ticketFields);

            var ticketMap = FindPossibleCombinationsForTicketFields(validTickets, ticketFields).ToList();
            var reducedMap = ReduceCombinations(ticketMap);

            // Extract values from first 6 ranges in my ticket.
            var ticketIndex = ticketInput.FindIndex(x => x.Equals("your ticket:")) + 1;
            var myTicket = ticketInput.ElementAt(ticketIndex).Split(',').Select(int.Parse).ToList();
            long departureLocation = myTicket[reducedMap[0]];
            long departureStation = myTicket[reducedMap[2]];
            long departurePlatform = myTicket[reducedMap[4]];
            long departureTrack = myTicket[reducedMap[6]];
            long departureDate = myTicket[reducedMap[8]];
            long departureTime = myTicket[reducedMap[10]];

            var result1 = errorRate;
            long result2 = departureLocation * departureStation * departurePlatform *
                           departureTrack * departureDate * departureTime;

            return (nameof(Day16), result1.ToString(), result2.ToString());
        }

        private static List<int[]> SetupRangesForTicketFields(List<string> ticketInput)
        {
            var ticketFields = new List<int[]>();
            foreach (var line in ticketInput)
            {
                if (line.Equals("your ticket:"))
                {
                    break;
                }

                var rangeString = line.Split(':')[1];
                var ranges = rangeString.Split(new[] {"or"}, StringSplitOptions.None);

                var firstRange = ranges[0].Split('-');
                var secondRange = ranges[1].Split('-');

                ticketFields.Add(Enumerable.Range(int.Parse(firstRange[0]), int.Parse(firstRange[1]) - int.Parse(firstRange[0]) + 1).ToArray());
                ticketFields.Add(Enumerable.Range(int.Parse(secondRange[0]), int.Parse(secondRange[1]) - int.Parse(secondRange[0]) + 1).ToArray());
            }

            return ticketFields;
        }

        private static (List<List<int>>, int) FindErrorRateAndGetValidTickets(List<string> ticketInput, IReadOnlyCollection<int[]> ticketFields)
        {
            var validTickets = new List<List<int>>();
            var nearbyTicketIndex = ticketInput.FindIndex(x => x.Equals("nearby tickets:")) + 1;
            var errorRate = 0;
            for (var i = nearbyTicketIndex; i < ticketInput.Count; i++)
            {
                var ticket = ticketInput.ElementAt(i).Split(',').Select(int.Parse).ToList();
                var valid = true;
                foreach (var t in ticket)
                {
                    if (!ticketFields.Any(x => x.Contains(t)))
                    {
                        errorRate += t;
                        valid = false;
                    }
                }

                if (valid)
                {
                    validTickets.Add(ticket);
                }
            }

            return (validTickets, errorRate);
        }

        private static Dictionary<int, int> ReduceCombinations(List<KeyValuePair<int, List<int>>> ticketMap)
        {
            var result = new Dictionary<int, int>();
            while (ticketMap.Any(x => x.Value.Count > 0))
            {
                var values = ticketMap.First(x => x.Value.Count == 1);
                var value = values.Value.First();
                result.Add(value, values.Key);
                ticketMap.ForEach(x => x.Value.Remove(value));
            }

            return result;
        }

        private static Dictionary<int, List<int>> FindPossibleCombinationsForTicketFields(IReadOnlyCollection<List<int>> validTickets, IReadOnlyList<int[]> ticketFields)
        {
            var ticketMap = new Dictionary<int, List<int>> ();
            for (var i = 0; i < validTickets.First().Count; i++)
            {
                var index = i;
                var fieldToTry = validTickets.Select(x => x.ElementAt(index)).ToList();
                for (var j = 0; j < ticketFields.Count - 1; j+=2)
                {
                    var ticketRange = ticketFields[j].Concat(ticketFields[j + 1]);

                    if (fieldToTry.All(x => ticketRange.Contains(x)))
                    {
                        if (ticketMap.ContainsKey(index))
                        {
                            ticketMap[index].Add(j);
                        }
                        else
                        {
                            ticketMap.Add(index, new List<int>{j});
                        }
                    }
                }
            }

            return ticketMap;
        }
    }
}