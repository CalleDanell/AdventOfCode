using Common;
using Common.Coordinates;

namespace _2023.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day03));
            var coordiantes = Utils.GenerateCoordinates(input);

            var xMax = coordiantes.Keys.Max(x => x.X);

            var partNumbers = new List<int>();
            var gears = new Dictionary<Coordinate, List<int>>();

            var currentNumber = string.Empty;
            var found = false;
            Coordinate? gearFound = null;
            

            foreach (var coordiante in coordiantes)
            {
                if (char.IsDigit(coordiante.Value))
                {
                    currentNumber += coordiante.Value.ToString();
                    var adjacent = coordiante.Key.GetAdjacent(false);

                    foreach (var adjacentCoord in adjacent)
                    {
                        if (coordiantes.ContainsKey(adjacentCoord))
                        {
                            var adjacentValue = coordiantes[adjacentCoord];
                            if (adjacentValue != '.' && !char.IsDigit(adjacentValue))
                            {
                                if (adjacentValue.Equals('*'))
                                {
                                    gearFound = adjacentCoord;
                                }
                                found = true;
                            }
                        }
                    }

                }

                if (!char.IsDigit(coordiante.Value) || coordiante.Key.X == xMax)
                {
                    if (found)
                    {
                        var nbr = int.Parse(currentNumber);
                        partNumbers.Add(nbr);
                        found = false;

                        if (gearFound != null)
                        {
                            if (!gears.TryAdd(gearFound, new List<int> { nbr }))
                            {
                                gears[gearFound].Add(nbr);
                            }
                            gearFound = null;
                        }
                    }
                    currentNumber = string.Empty;
                }
            }

            var gearRatios = gears.Where(x => x.Value.Count() == 2).Sum(x => x.Value[0] * x.Value[1]);

            return (nameof(Day03), partNumbers.Sum().ToString(), gearRatios.ToString());
        }
    }
}