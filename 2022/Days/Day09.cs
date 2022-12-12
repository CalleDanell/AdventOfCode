using Common;
using Common.Coordinates;

namespace _2022.Days
{
    public class Day09 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var motions = input.Select(x => new Motion(x));

            var twoKnots = GetKnots(2);
            var twoVisited = MoveKnots(motions, twoKnots);

            var tenKnots = GetKnots(10);
            var tenVisited = MoveKnots(motions, tenKnots);

            var resPartOne = twoVisited.Count;
            var resPartTwo = tenVisited.Count;

            return (day, resPartOne.ToString(), resPartTwo.ToString());
        }

        private static HashSet<Coordinate> MoveKnots(IEnumerable<Motion> motions, List<Knot> knots)
        {
            var motionList = new List<Motion>(motions);
            var visited = new HashSet<Coordinate>();
            foreach (var motion in motionList)
            {
                for (var i = 0; i < motion.Distance; i++)
                {
                    foreach (var knot in knots)
                    {
                        if (knot.Parent == null)
                        {
                            switch (motion.Direction)
                            {
                                case "R":
                                    knot.Move(-1, 0);
                                    break;
                                case "L":
                                    knot.Move(1, 0);
                                    break;
                                case "U":
                                    knot.Move(0, 1);
                                    break;
                                case "D":
                                    knot.Move(0, -1);
                                    break;
                            }

                            continue;
                        }

                        var distance = Math.Sqrt(Math.Pow(knot.Pos.X - knot.Parent?.Pos.X ?? 0, 2) + Math.Pow(knot.Pos.Y - knot.Parent?.Pos.Y ?? 0, 2));
                        var parentX = knot.Parent?.Pos.X ?? knot.Pos.X;
                        var parentY = knot.Parent?.Pos.Y ?? knot.Pos.Y;

                        if (distance > 1.5)
                        {
                            if (knot.Pos.Y == parentY)
                            {
                                if (knot.Pos.X < parentX)
                                {
                                    knot.Move(1, 0);
                                }
                                else
                                {
                                    knot.Move(-1, 0);
                                }
                            }
                            else if (knot.Pos.X == parentX)
                            {
                                if (knot.Pos.Y < parentY)
                                {
                                    knot.Move(0, 1);
                                }
                                else
                                {
                                    knot.Move(0, -1);
                                }
                            }
                            else if (knot.Pos.Y > parentY && knot.Pos.X > parentX)
                            {
                                knot.Move(-1, -1);
                            }
                            else if (knot.Pos.Y > parentY && knot.Pos.X < parentX)
                            {
                                knot.Move(1, -1);
                            }
                            else if (knot.Pos.Y < parentY && knot.Pos.X < parentX)
                            {
                                knot.Move(1, 1);
                            }
                            else if (knot.Pos.Y < parentY && knot.Pos.X > parentX)
                            {
                                knot.Move(-1, 1);
                            }
                        }
                        var tail = knots.First(x => x.Child == null);
                        visited.Add(tail.Pos);
                    }
                }
            }

            return visited;
        }

        private static List<Knot> GetKnots(int count)
        {
            var first = new Knot() { Id = 0 };
            var knots = new List<Knot>() { first };
            for (var i = 1; i < count; i++)
            {
                var second = new Knot() { Id = i, Parent = first };
                knots.Add(second);
                first.Child = second;
                first = second;
            }

            return knots;
        }
    }

    public class Knot
    {
        public int Id { get; set; }
        public Knot? Parent { get; set; }
        public Knot? Child { get; set; }
        public Coordinate Pos { get; set; } = new Coordinate(0, 0);
        public void Move(int x, int y) => Pos = new Coordinate(Pos.X + x, Pos.Y + y);
    }

    public class Motion
    {
        public Motion(string input)
        {
            var parts = input.Split(" ");
            Direction = parts[0];
            Distance = int.Parse(parts[1]);
        }

        public int Distance { get; }
        public string Direction { get; }
    }
}