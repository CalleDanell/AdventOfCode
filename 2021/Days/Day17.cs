using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;
using Common.Coordinates;

namespace _2021.Days
{
    public class Day17 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetFullInput(nameof(Day17));

            var parts = input.Split(", ");
            var xValues = parts[0].Split("=")[1].Split("..");
            var yValues = parts[1].Split("=")[1].Split("..");

            var xMax = int.Parse(xValues[1]);
            var xMin = int.Parse(xValues[0]);
            var yMax = int.Parse(yValues[1]);
            var yMin = int.Parse(yValues[0]);

            var targetArea = new HashSet<Coordinate>();
            for (var y = int.Parse(yValues[0]); y <= yMax; y++)
                for (var x = int.Parse(xValues[0]); x <= xMax; x++)
            {
                    targetArea.Add(new Coordinate(x,y));
            }

            var xVelocityRange = Enumerable.Range(0, xMax + 1);
            var yVelocityRange = Enumerable.Range(yMin, 250); // Random range that works for my input... 

            var largestY = 0;
            var hits = 0;
            var velocityRange = xVelocityRange.ToList();
            foreach (var y in yVelocityRange)
            {
                foreach (var x in velocityRange)
                {
                    var result = Shoot(xMax, xMin, yMin, x, y, targetArea);

                    if (result != null)
                        hits++;

                    var max = result?.Max(c => c.Y);
                    if (max > largestY)
                        largestY = (int)max;
                }
            }

            var resultPartOne = largestY;
            var resultPartTwo = hits;

            return (nameof(Day17), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private HashSet<Coordinate> Shoot(int xMax, int xMin, int yMin, int xVelocity, int yVelocity, IEnumerable<Coordinate> targetArea)
        {
            var targetList = targetArea.ToList();
            var positions = new HashSet<Coordinate>();

            var current = new Coordinate(0,0);
            while (current.X < xMax && current.Y > yMin)
            {
                // It is never going to hit
                if (xVelocity == 0 && current.X < xMin)
                {
                    return null;
                }

                var xNew = current.X + xVelocity;
                var yNew = current.Y + yVelocity;
                
                current = new Coordinate(xNew, yNew);
                positions.Add(current);

                if (targetList.Contains(current))
                    return positions;

                if(xVelocity > 0)
                    xVelocity--;

                yVelocity--;
            }

            return null;
        }
    }
}