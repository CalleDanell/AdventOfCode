using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day17 : IDay
    {
        private const int TotalRounds = 6;

        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day17));

            var initialCubes = GetInitialCubes(input);
            var cubes = InitGrid(initialCubes, TotalRounds * 2);

            //RunCycles(cubes);

            var result1 = "Too slow...";
            var result2 = string.Empty;

            return (nameof(Day17), result1.ToString(), result2.ToString());
        }

        private static void RunCycles(List<Cube> cubes)
        {
            var rounds = 0;
            while (rounds < TotalRounds)
            {
                var originalCubes = cubes.ConvertAll(x => new Cube(x.X, x.Y, x.Z, x.Active));
                cubes.ForEach(x => x.UpdateState(originalCubes));
                rounds++;
            }
        }

        private static List<Cube> GetInitialCubes(IEnumerable<string> input)
        {
            var initialCubes = input.Select((x, yIndex) =>
                {
                    var chars = x.ToCharArray();
                    var list = new List<Cube>();
                    for (var xIndex = 0; xIndex < chars.Length; xIndex++)
                    {
                        var active = chars[xIndex] == '#';
                        list.Add(new Cube(xIndex, yIndex, 0, active));
                    }

                    return list;
                }).SelectMany(x => x)
                .Where(x => x.Active)
                .ToList();
            return initialCubes;
        }

        private static List<Cube> InitGrid(IReadOnlyCollection<Cube> initialCubes, int gridSize)
        {
            var grid = new List<Cube>();
            for (var i = -gridSize; i < gridSize; i++)
                for(var j = -gridSize; j < gridSize; j++)
                    for (var k = -gridSize; k < gridSize; k++)
                    {
                        var cube = new Cube(i, j, k, false);
                        if (initialCubes.Any(x => x.Equals(cube)))
                        {
                            cube.Active = true;
                        }
                        grid.Add(cube);
                    }

            return grid;
        }
    }

    public class Cube
    {
        public Cube(int x, int y, int z, bool active)
        {
            X = x;
            Y = y;
            Z = z;
            Active = active;
        }

        public int X { get; }
        public int Y { get;  }
        public int Z { get; }
        public bool Active { get; set; }

        public void UpdateState(List<Cube> cubes)
        {
            var activeNeighbors = GetActiveNeighbors(cubes);
            if (Active)
            {
                if (activeNeighbors.Equals(2) || activeNeighbors.Equals(3))
                {
                    Active = true;
                }
                else
                {
                    Active = false;
                }
            }
            else
            {
                Active = activeNeighbors.Equals(3);
            }
        }

        private int GetActiveNeighbors(IEnumerable<Cube> cubes)
        {
            var xRange = Enumerable.Range(X - 1, 3);
            var yRange = Enumerable.Range(Y - 1, 3);
            var zRange = Enumerable.Range(Z - 1, 3);

            var test = cubes.Where(cube => xRange.Contains(cube.X) &&
                                           yRange.Contains(cube.Y) &&
                                           zRange.Contains(cube.Z));

            var count = test.Count(x => x.Active);

            if (Active)
            {
                count -= 1;
            }

            return count;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var c = (Cube)obj;
            return (X == c.X) && (Y == c.Y) && (Z == c.Z);
        }

        public override int GetHashCode()
        {
            return Tuple.Create(X, Y, Z).GetHashCode();
        }
    }
}