using Common;
using Common.Coordinates;

namespace _2024.Days
{
    public class Day14 : IDay
    {
        private const int MaxX = 100; //101
        private const int MaxY = 102; //103

        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);


            var map = new Dictionary<Coordinate, char>();
            for (var y = 0; y < MaxY; y++)
            {
                for (var x = 0; x < MaxX; x++)
                {
                    map.Add(new Coordinate(x, y), '.');
                }
            }


            var robots = new List<Robot>();
            foreach(var line in input)
            {
                var parts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                var xy = parts[0][2..].Split(",").Select(int.Parse).ToArray();
                var vxvy = parts[1][2..].Split(",").Select(int.Parse).ToArray();

                var robot = new Robot(xy[0], xy[1], vxvy[0], vxvy[1], MaxX, MaxY);
                robots.Add(robot);
            }

            var xLine = MaxX / 2;
            var yLine = MaxY / 2;
            var seconds = 10000;

            for (var i = 1; i <= seconds; i++)
            {
                foreach (var r in robots)
                {
                    r.Move();
                }

                WiriteToFile(map, robots, i);
            }

            
            var topLeft = robots.Count(x => x.X < xLine && x.Y < yLine);
            var topRight = robots.Count(x => x.X > xLine && x.Y < yLine);
            var botLeft = robots.Count(x => x.X < xLine && x.Y > yLine);
            var botRight = robots.Count(x => x.X > xLine && x.Y > yLine);


            long partOne = topLeft * topRight * botLeft * botRight;
            var partTwo = "Look for the iteration with a Christmas tress in the output.txt for";

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private void Print(Dictionary<Coordinate, char> map, List<Robot> robots)
        {
            for (var y = 0; y <= MaxY; y++)
            {
                for (var x = 0; x <= MaxX; x++)
                {
                    var current = new Coordinate(x, y);

                    Console.Write(robots.Count(x => x.CurrentPosition.Equals(current)));
                    
                }
                Console.WriteLine();
            }
        }

        private void WiriteToFile(Dictionary<Coordinate, char> map, List<Robot> robots, int iteration)
        {
            File.AppendAllText("output.txt", Environment.NewLine + "iteration:" + iteration + Environment.NewLine);
            
            var lines = new List<string>(); 
            for (var y = 0; y <= MaxY; y++)
            {
                var line = string.Empty;
                for (var x = 0; x <= MaxX; x++)
                {
                    var current = new Coordinate(x, y);

                    var count = robots.Count(x => x.CurrentPosition.Equals(current));
                    var output = count > 0 ? '#' : '.';
                    line += output;
                }

                lines.Add(line + Environment.NewLine);
            }
            File.AppendAllLines("output.txt", lines);
        }

        internal class Robot {

            public Coordinate CurrentPosition => new Coordinate(X, Y);
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Vx { get; }
            public int Vy { get; }

            private int MaxX = 0;
            private int MaxY = 0;

            public Robot(int x, int y, int vx, int vy, int maxx, int maxy)
            {
                X = x; Y = y; Vx = vx; Vy = vy; MaxX = maxx; MaxY = maxy;
            }

            public void Move()
            {
                var newX = X + Vx;
                var newY = Y + Vy;

                if (newY > MaxY)
                {
                    newY = newY - MaxY - 1;
                }

                else if (newY < 0)
                {
                    newY = newY + MaxY + 1;
                }

                if (newX > MaxX)
                {
                    newX = newX - MaxX - 1;
                }

                else if (newX < 0)
                {
                    newX = newX + MaxX + 1;
                }

                X = newX;
                Y = newY;

            }
        }
    }
}