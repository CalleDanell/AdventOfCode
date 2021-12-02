using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day02));
            var navigationInstructions = input.Select(x =>
            {
                var parts = x.Split(' ');
                return new NavigationInstruction
                {
                    Steps = int.Parse(parts[1]),
                    Navigation = parts[0]
                };
            });

            var basicSubmarine = new Submarine(0,0,0);
            var advancedSubmarine = new Submarine(0, 0, 0);

            foreach (var navigationInstruction in navigationInstructions)
            {
                basicSubmarine.Move(navigationInstruction);
                advancedSubmarine.MoveWithAim(navigationInstruction);
            }

            var resultPartOne = basicSubmarine.PositionResult;
            var resultPartTwo = advancedSubmarine.PositionResult;

            return (nameof(Day02), resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class Submarine
    {
        public Submarine(int horizontalPosition, int depth, int aim)
        {
            HorizontalPosition = horizontalPosition;
            Depth = depth;
            Aim = aim;
        }

        public int Depth { get; private set; }
        public int HorizontalPosition { get; private set; }
        public int Aim { get; private set; }

        public void Move(NavigationInstruction navigationInstruction)
        {
            switch(navigationInstruction.Navigation)
            {
                case "forward":
                    HorizontalPosition += navigationInstruction.Steps;
                    break;
                case "up":
                    Depth -= navigationInstruction.Steps;
                    break;
                case "down":
                    Depth += navigationInstruction.Steps;
                    break;
            }
        }

        public void MoveWithAim(NavigationInstruction navigationInstruction)
        {
            switch (navigationInstruction.Navigation)
            {
                case "forward":
                    HorizontalPosition += navigationInstruction.Steps;
                    Depth += Aim * navigationInstruction.Steps;
                    break;
                case "up":
                    Aim -= navigationInstruction.Steps;
                    break;
                case "down":
                    Aim += navigationInstruction.Steps;
                    break;
            }
        }

        public int PositionResult => Depth * HorizontalPosition;
    }

    public class NavigationInstruction
    {
        public int Steps { get; set; }
        public string Navigation { get; set; }
    }
}