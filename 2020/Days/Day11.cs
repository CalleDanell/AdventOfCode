using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day11 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);
            var currentLayout = input.Select((line, y) =>
            {
                var array = line.ToCharArray();
                return array.Select((seatStatus, x) => (seatStatus, x, y));
            }).SelectMany(x => x).ToDictionary(x => (x.x, x.y), y => y.seatStatus);
            
            var result1 = FindOccupiedSeats(currentLayout, 4, 1);
            var result2 = FindOccupiedSeats(currentLayout, 5,  100);

            return (result1.ToString(), result2.ToString());
        }

        private static int FindOccupiedSeats(Dictionary<(int X, int Y), char> currentLayout, int maxAdjacent, int length)
        {
            var previousLayout = new Dictionary<(int X, int Y), char>();
            while (!CompareSeatLayouts(currentLayout, previousLayout))
            {
                var updatedLayout = GetNewSeatLayout(currentLayout, maxAdjacent, length);
                previousLayout = currentLayout;
                currentLayout = updatedLayout;
            }

            return currentLayout.Count(x => x.Value == '#');
        }

        private static bool CompareSeatLayouts(Dictionary<(int X, int Y), char> one, Dictionary<(int X, int Y), char> two)
        {
            if (one.Count != two.Count)
            {
                return false;
            }
            return one.All(pair => one[pair.Key] == two[pair.Key]);
        }

        private static Dictionary<(int X, int Y), char> GetNewSeatLayout(Dictionary<(int X, int Y), char> layout, int maxAdjacent, int length) 
        {
            var newLayout = new Dictionary<(int X, int Y), char>();
            newLayout.Clear();

            foreach (var seat in layout)
            {
                var seatState = seat.Value;
                if (seatState == '.')
                {
                    newLayout[seat.Key] = seatState;
                    continue;
                }

                var adjacentSeats = GetNumberOfOccupiedAdjacent(layout, seat.Key, length);
                if (seatState.Equals('L') && adjacentSeats == 0)
                {
                    seatState = '#';
                }
                else if (seatState.Equals('#') && adjacentSeats >= maxAdjacent)
                {
                    seatState = 'L';
                }

                newLayout[seat.Key] = seatState;
            }

            return newLayout;
        }

        private static int GetNumberOfOccupiedAdjacent(Dictionary<(int X, int Y), char> seatLayout, (int x , int y) seatKey, int length)
        {
            var occupiedSeats = new List<int>();
            var up = FindFirstSeat(seatLayout, seatKey, 0, -1, length);
            var down = FindFirstSeat(seatLayout, seatKey, 0, 1, length);
            var left = FindFirstSeat(seatLayout, seatKey, -1, 0, length);
            var right = FindFirstSeat(seatLayout, seatKey, 1, 0, length);

            var upLeft = FindFirstSeat(seatLayout, seatKey, -1, -1, length);
            var upRight = FindFirstSeat(seatLayout, seatKey, 1, -1, length);
            var downLeft = FindFirstSeat(seatLayout, seatKey, -1, 1, length);
            var downRight = FindFirstSeat(seatLayout, seatKey, 1, 1, length);

            occupiedSeats.AddRange(new[] { up, down, left, right, upLeft, upRight, downLeft, downRight });

            return occupiedSeats.Sum();
        }

        private static int FindFirstSeat(Dictionary<(int X, int Y), char> seatLayout, (int x, int y) seat, int xDirection, int yDirection, int length)
        {
            for (var i = 1; i <= length; i++)
            {
                var xTemp = i * xDirection;
                var yTemp = i * yDirection;
                var neighbor = (seat.x + xTemp, seat.y + yTemp);
                
                if (neighbor.Item1 < 0 || neighbor.Item2 < 0)
                {
                    return 0;
                }

                seatLayout.TryGetValue(neighbor, out var state);
                if (state == '#')
                {
                    return 1;
                }

                if (state == 'L')
                {
                    return 0;
                }
            }

            return 0;
        }
    }
}