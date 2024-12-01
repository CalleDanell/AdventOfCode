using Common;
using Common.Coordinates;

namespace _2022.Days
{
    public class Day17 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetFullInput(day);
            var sequence = new Queue<char>(input.ToCharArray());

            const long fallingRocks = 2022;

            // for part B I need to detect a cycle by printing stuff...
            const long cycleSize = 2729; // Found via prinitng and searching for the same line to find the pattern. 
            const long shortCut = fallingRocks % cycleSize;

            var floor = new List<Coordinate>
            {
                new Coordinate(0,0),
                new Coordinate(1,0),
                new Coordinate(2,0),
                new Coordinate(3,0),
                new Coordinate(4,0),
                new Coordinate(5,0),
                new Coordinate(6,0)
            };

            var grid = new List<Coordinate>(floor);
            var grids = new List<string>();
            var cycleHeight = 0;

            for (long i = 0; i < fallingRocks + 1; i++)
            {
                var startHeight = grid.Max(x => x.Y) + 4;
                var stoneType = (int)i % 5;
                var stone = new Stone(stoneType, startHeight);

                if(i == cycleSize)
                {
                    cycleHeight = grid.Max(x => x.Y) - 2;
                }

                var collision = false;
                while (!collision)
                {
                    var next = sequence.Dequeue();
                    sequence.Enqueue(next);
                    stone.Move(next);
                    collision = stone.CoveredCoordiantes.Intersect(grid).Any();
                    if(collision)
                    {
                        // Move it back if collision
                        stone.Move(next == '>' ? '<' : '>');
                        collision = false;
                    }

                    stone.Move('v');
                    collision = stone.CoveredCoordiantes.Intersect(grid).Any();
                }

                // Move it back if collision
                stone.Move('^');
                grid.AddRange(stone.CoveredCoordiantes);
            }

            var partOne = grid.Max(x => x.Y) - 2;
            var partTwo = 1;
            return (day, partOne.ToString(), partTwo.ToString());
    }

    public class Stone
        {
            private readonly int Type = 0;
            private List<Coordinate> coveredCoordiantes = new List<Coordinate>();
            public Stone(int type, int startY)
            {
                this.Type = type;
                switch (Type)
                {
                    case 0:
                        coveredCoordiantes.AddRange(new List<Coordinate> 
                            { 
                                new Coordinate(2,startY),
                                new Coordinate(3,startY),
                                new Coordinate(4,startY),
                                new Coordinate(5,startY)
                            }
                        );
                        break;
                    case 1:
                        coveredCoordiantes.AddRange(new List<Coordinate>
                            {
                                new Coordinate(3,startY + 2),
                                new Coordinate(2,startY + 1),
                                new Coordinate(3,startY + 1),
                                new Coordinate(4,startY + 1),
                                new Coordinate(3,startY)
                            }
                        );
                        break;
                    case 2:
                        coveredCoordiantes.AddRange(new List<Coordinate>
                            {
                                new Coordinate(4,startY + 2),
                                new Coordinate(4,startY + 1),
                                new Coordinate(4,startY),
                                new Coordinate(3,startY),
                                new Coordinate(2,startY)
                            }
                        );
                        break;
                    case 3:
                        coveredCoordiantes.AddRange(new List<Coordinate>
                            {
                                new Coordinate(2,startY + 3),
                                new Coordinate(2,startY + 2),
                                new Coordinate(2,startY + 1),
                                new Coordinate(2,startY)
                            }
                        );
                        
                        break;
                    case 4:
                        coveredCoordiantes.AddRange(new List<Coordinate>
                            {
                                new Coordinate(2,startY + 1),
                                new Coordinate(3,startY + 1),
                                new Coordinate(2,startY),
                                new Coordinate(3,startY)
                            }
                        );
                        break;
                }
            }

            public List<Coordinate> CoveredCoordiantes => coveredCoordiantes;

            public void Move(char direction) 
            {
                switch (direction)
                {
                    case '>':
                        if(coveredCoordiantes.Max(x => x.X) < 6)
                            coveredCoordiantes.ForEach(x => x.Move(1, 0));
                        break;
                    case '<':
                        if (coveredCoordiantes.Min(x => x.X) > 0)
                            coveredCoordiantes.ForEach(x => x.Move(-1, 0));
                        break;
                    case 'v':
                        if (coveredCoordiantes.Min(x => x.Y) > 0)
                            coveredCoordiantes.ForEach(x => x.Move(0, -1));
                        break;
                    case '^':
                       coveredCoordiantes.ForEach(x => x.Move(0, 1));
                       break;
                }
            }
        }
    }
}