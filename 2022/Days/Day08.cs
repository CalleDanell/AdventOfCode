using Common;

namespace _2022.Days
{
    public class Day08 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var grid = GetGrid(input);

            var treesThatCanBeSeen = 0;
            long bestScore = 0;
            for(var i = 0; i < grid.Count; i++)
            {
                for(var j = 0; j < grid[i].Count; j++)
                {
                    if(i == 0 
                        || i == grid.Count
                        || j == 0
                        || j == grid[i].Count)
                    {
                        // is edge 
                        treesThatCanBeSeen++;
                        continue;
                    }

                    var current = grid[i][j];

                    var above = grid.Take(i).Select(x => x[j]);
                    var below = grid.Skip(i + 1).Take(grid.Count - i).Select(x => x[j]);
                    var left = grid[i].Take(j);
                    var right = grid[i].Skip(j + 1).Take(grid[i].Count - j);

                    // Reverse to get the trees in the viewing direction. Not outside and in. 
                    var viewingDistanceAbove = GetViewDistance(above.Reverse(), current);
                    var viewingDistanceBelow = GetViewDistance(below, current);
                    var viewingDistanceLeft = GetViewDistance(left.Reverse(), current);
                    var viewingDistanceRight = GetViewDistance(right, current);

                    long scenicScore = viewingDistanceAbove * viewingDistanceBelow * viewingDistanceLeft * viewingDistanceRight;
                    if(scenicScore > bestScore)
                        bestScore = scenicScore;

                    if (above.All(x => x < current) 
                        || below.All(x => x < current) 
                        || left.All(x => x < current) 
                        || right.All(x => x < current)
                        )
                    {
                        treesThatCanBeSeen++;
                    }
                }
            }

            var resPartOne = treesThatCanBeSeen;
            var resPartTwo = bestScore;

            return (day, resPartOne.ToString(), resPartTwo.ToString());
        }

        private int GetViewDistance(IEnumerable<int> direction, int treeHeight)
        {
            var distance = 0;
            foreach(var tree in direction)
            {
                distance++;
                if (tree >= treeHeight)
                {
                    return distance;
                }
            }

            return distance; 
        }

        private static List<List<int>> GetGrid(IEnumerable<string> input)
        {
            var grid = new List<List<int>>();
            foreach (var line in input)
            {
                var paddedline = line.Select(x => x - '0');
                var row = new List<int>(paddedline);
                grid.Add(row);
            }

            return grid;
        }
    }
}