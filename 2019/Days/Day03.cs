using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOC.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var enumerable = input.ToList();
            var instructions1 = enumerable.ElementAt(0).Split(',').Select(GetDirectionsFromInput);
            var instructions2 = enumerable.ElementAt(1).Split(',').Select(GetDirectionsFromInput);

            var wire1 = PlaceWireOnBoard(instructions1);
            var wire2 = PlaceWireOnBoard(instructions2);

            var intersections = wire1.Keys.Intersect(wire2.Keys).Where(x => x != (0, 0)).ToList();

            var result = intersections.Min(x => Math.Abs(x.Item1) + Math.Abs(x.Item2));
            var result2 = intersections.Min(x => wire1[x] + wire2[x]);

            return (result.ToString(), result2.ToString());
        }

        private static Dictionary<(int, int), int> PlaceWireOnBoard(IEnumerable<(string, int)> instructions)
        {
            var wire = new Dictionary<(int, int), int>();
            var x = 0;
            var y = 0;
            var value = 0;
            foreach (var (direction, steps) in instructions)
            {
                if (direction.Equals("R"))
                {
                    for (var i = 0; i < steps; i++)
                    {
                        wire.TryAdd((x++, y), value++);
                    }
                }
                else if (direction.Equals("L"))
                {
                    for (var i = 0; i < steps; i++)
                    {
                        wire.TryAdd((x--, y), value++);
                    }
                }
                else if (direction.Equals("U"))
                {
                    for (var i = 0; i < steps; i++)
                    {
                        wire.TryAdd((x, y++), value++);
                    }
                }
                else if (direction.Equals("D"))
                {
                    for (var i = 0; i < steps; i++)
                    {
                        wire.TryAdd((x, y--), value++);
                    }
                }
            }

            return wire;
        }

        private static (string, int) GetDirectionsFromInput(string s)
        {
            var direction = s.Substring(0, 1);
            var steps = int.Parse(s.Substring(1));
            return (direction, steps);
        }
    }
}
