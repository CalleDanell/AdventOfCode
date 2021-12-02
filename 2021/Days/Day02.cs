using Common;
using System.Linq;
using System.Threading.Tasks;
using _2021.Submarine;

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

            var basicSubmarine = new Submarine.Submarine(0,0,0);
            var advancedSubmarine = new Submarine.Submarine(0, 0, 0);

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
}