using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Common;
using Common.Coordinates;
using System.Linq;

namespace _2018.Days
{
    public class Day11 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetFullInput(nameof(Day11));
            var gridSerialNumber = int.Parse(input);

            var grid = GenerateGrid(1, 300, 1, 300, gridSerialNumber);
            var summarizedGrid = BuildSummedAreaTable(grid);
            
            var highestPowerLevelCoordinate = FindThreeByThreePowerCoordinate(grid);
            var (coordinate, size, power) = FindMaxPower(summarizedGrid);

            return (nameof(Day11), $"({highestPowerLevelCoordinate.X},{highestPowerLevelCoordinate.Y})", $"({coordinate.X},{coordinate.Y},{size})");
        }

        private static Coordinate FindThreeByThreePowerCoordinate(Dictionary<Coordinate, int> grid)
        {
            Coordinate highestPowerLevelCoordinate = null;
            var highestPowerLevel = 0;

            foreach (var cell in grid.Keys)
            {
                var totalPowerLevel = 0;
                var threeByThreeSquareWithingrid = cell.GetAdjacent(true).Where(x => grid.ContainsKey(x)).ToList();

                foreach (var c in threeByThreeSquareWithingrid)
                {
                    totalPowerLevel += grid[c];
                }

                if (totalPowerLevel > highestPowerLevel)
                {
                    highestPowerLevel = totalPowerLevel;
                    highestPowerLevelCoordinate = threeByThreeSquareWithingrid.OrderBy(x => x.X).ThenBy(x => x.Y).First();
                }
            }

            return highestPowerLevelCoordinate;
        }

        private static int CalculatePowerLevel(int gridSerialNumber, Coordinate cell)
        {
            var rackid = cell.X + 10;
            var powerLevel = rackid * cell.Y;
            powerLevel += gridSerialNumber;
            powerLevel *= rackid;
            int hundredDigit = Math.Abs(powerLevel / 100 % 10);
            powerLevel = hundredDigit - 5;
            return powerLevel;
        }

        private static Dictionary<Coordinate,int> GenerateGrid(int xMin, int xMax, int yMin, int yMax, int gridSerialNumber)
        {
            var grid = new Dictionary<Coordinate, int>();
            for (var y = yMin; y <= yMax; y++)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    var key = new Coordinate(x, y);
                    var powerLevel = CalculatePowerLevel(gridSerialNumber, key);
                    grid.Add(key, powerLevel);
                }
            }

            return grid;
        }

        public (Coordinate, int, int) FindMaxPower(Dictionary<Coordinate, int> summedAreaTable)
        {
            int maxPower = int.MinValue;
            Coordinate topLeft = null;
            int bestSize = 0;

            for (int size = 1; size <= 300; size++)
            {
                for (int y = size; y <= 300; y++)
                {
                    for (int x = size; x <= 300; x++)
                    {
                        var bottomRight = new Coordinate(x, y);
                        var topLeftCoord = new Coordinate(x - size, y - size);

                        int totalPower = summedAreaTable[bottomRight]
                            - (x >= size ? summedAreaTable.GetValueOrDefault(new Coordinate(x - size, y), 0) : 0)
                            - (y >= size ? summedAreaTable.GetValueOrDefault(new Coordinate(x, y - size), 0) : 0)
                            + (x >= size && y >= size ? summedAreaTable.GetValueOrDefault(topLeftCoord, 0) : 0);

                        if (totalPower > maxPower)
                        {
                            maxPower = totalPower;
                            topLeft = new Coordinate(x - size + 1, y - size + 1);
                            bestSize = size;
                        }
                    }
                }
            }

            return (topLeft, bestSize, maxPower);
        }

        private Dictionary<Coordinate, int> BuildSummedAreaTable(Dictionary<Coordinate, int> inputGrid)
        {
            var grid = new Dictionary<Coordinate, int>();
            for (int y = 1; y <= 300; y++)
            {
                for (int x = 1; x <= 300; x++)
                {
                    var coord = new Coordinate(x, y);
                    int currentPower = inputGrid[coord];

                    int left = x > 1 ? grid[new Coordinate(x - 1, y)] : 0;
                    int above = y > 1 ? grid[new Coordinate(x, y - 1)] : 0;
                    int diagonal = (x > 1 && y > 1) ? grid[new Coordinate(x - 1, y - 1)] : 0;

                    grid[coord] = currentPower + left + above - diagonal;
                }
            }

            return grid;
        }
    }
}
