using Common;
using Common.Coordinates;
using System.Text.RegularExpressions;

namespace _2022.Days
{
    public class Day15 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);
            const int magicRow = 2000000;
            const long searchSpace = 4000000L;
            var pairs = GenerateSensorBeaconPairs(input);
            var ranges = GenerateHorizontalRanges(pairs);

            // Also merges ranges that can be used in partone. 
            long partTwo = SolvePartTwo(searchSpace, ranges);

            var partOne = ranges[magicRow].First().Item2 - ranges[magicRow].First().Item1;

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private Dictionary<int, List<(int, int)>> GenerateHorizontalRanges(List<(Coordinate, Coordinate)> pairs)
        {
            var ranges = new Dictionary<int, List<(int, int)>>();

            foreach (var pair in pairs)
            {
                var distance = Math.Abs(pair.Item1.X - pair.Item2.X) + Math.Abs(pair.Item1.Y - pair.Item2.Y);
                var right = pair.Item1.X + distance;
                var left = pair.Item1.X - distance;
                var xrange = (left, right);
                var start = pair.Item1.Y;

                Add(ranges, start, xrange);

                for (var i = 1; i <= distance; i++)
                {
                    left++;
                    right--;
                    xrange = (left, right);

                    Add(ranges, start - i, xrange);
                    Add(ranges, start + i, xrange);
                }
            }

            return ranges;
        }

        private List<(Coordinate, Coordinate)> GenerateSensorBeaconPairs(IEnumerable<string> input)
        {
            var pairs = new List<(Coordinate, Coordinate)>();

            foreach (var item in input)
            {
                var parts = item.Replace("Sensor at x=", string.Empty).Split('=');
                var sensor = new Coordinate(GetCoordinate(parts[0]), GetCoordinate(parts[1]));
                var beacon = new Coordinate(GetCoordinate(parts[2]), GetCoordinate(parts[3]));
                pairs.Add((sensor, beacon));
            }

            return pairs;
        }

        private static long SolvePartTwo(long searchSpace, Dictionary<int, List<(int, int)>> ranges)
        {
            long partTwo = 0;
            for (var i = 0; i <= searchSpace; i++)
            {
                var mergedIntervalls = MergeIntervals(ranges[i]);
                if (mergedIntervalls.Count > 1)
                {
                    partTwo = ((mergedIntervalls[0].Item2 + 1) * searchSpace) + i;
                    break;
                }

                ranges[i] = mergedIntervalls;
            }

            return partTwo;
        }

        public static List<(int, int)> MergeIntervals(List<(int, int)> intervals)
        {
            if (intervals == null || intervals.Count == 0)
                return new List<(int, int)>();

            intervals.Sort((a, b) => a.Item1.CompareTo(b.Item1));

            var merged = new List<(int, int)>();
            var current = intervals[0];

            foreach (var interval in intervals.Skip(1))
            {
                if (current.Item2 >= interval.Item1)
                {
                    current = (current.Item1, Math.Max(current.Item2, interval.Item2));
                }
                else
                {
                    merged.Add(current);
                    current = interval;
                }
            }

            merged.Add(current);

            return merged;
        }

        public int GetCoordinate(string s) => int.Parse(Regex.Match(s, @"-?\d+").Value);

        public void Add(Dictionary<int, List<(int,int)>> dict, int key, (int,int) value)
        {
            if (dict.ContainsKey(key))
            {
                dict[key].Add(value);
            }
            else
            {
                dict[key] = new List<(int,int)>() { value };
            }
        }
    }
}