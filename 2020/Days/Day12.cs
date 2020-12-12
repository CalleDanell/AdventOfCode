using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day12 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);
            var navigationInstructions = input.Select(x => new NavigationInstruction(x));

            var navCpu = new NavigationComputer(navigationInstructions);

            var result1 = navCpu.Navigate();
            var result2 = string.Empty;

            return (result1.ToString(), result2.ToString());
        }
    }

    public class NavigationComputer
    {
        private Action currentLatitude = Action.East;
        private readonly Queue<NavigationInstruction> instructions;
        private (int x, int y) currentPosition = (0, 0);
        private readonly Dictionary<Action, (int x, int y)> navigationTable = new Dictionary<Action, (int x , int y)>
        {
            { Action.East, (1, 0) },
            { Action.South, (0,-1) },
            { Action.West, (-1, 0) },
            { Action.North, (0, 1) }
        };

        private readonly Dictionary<Action, (Action last, Action next)> turnTable = new Dictionary<Action, (Action last, Action next)>
        {
            { Action.East, (Action.North, Action.South) },
            { Action.South, (Action.East, Action.West) },
            { Action.West, (Action.South, Action.North) },
            { Action.North, (Action.West, Action.East) }
        };

        public NavigationComputer(IEnumerable<NavigationInstruction> instructions)
        {
            this.instructions = new Queue<NavigationInstruction>(instructions);
        }

        public int Navigate()
        {
            while (instructions.Count > 0)
            {
                var instruction = instructions.Dequeue();
                var (x, y) = navigationTable[currentLatitude];
                switch (instruction.Action)
                { 
                    case Action.Forward:
                        currentPosition.x += x * instruction.Value;
                        currentPosition.y += y * instruction.Value;
                        break;
                    case Action.Left:
                        TurnShip(-1, instruction.Value);
                        break;
                    case Action.Right:
                        TurnShip(1, instruction.Value);
                        break;
                    case Action.North:
                    case Action.South:
                    case Action.West:
                    case Action.East:
                        var newDirection = navigationTable[instruction.Action];
                        currentPosition.x += newDirection.x * instruction.Value;
                        currentPosition.y += newDirection.y * instruction.Value;
                        break;
                }
            }

            return Math.Abs(currentPosition.x) + Math.Abs(currentPosition.y);
        }

        private void TurnShip(int direction, int degrees)
        {
            var latitudesToTurn = degrees / 90 * direction;
            for (var i = 0; i < Math.Abs(latitudesToTurn); i++)
            {
                currentLatitude = latitudesToTurn < 0 ? turnTable[currentLatitude].last : turnTable[currentLatitude].next;
            }
        }
    }

    public class NavigationInstruction
    {
        public NavigationInstruction(string input)
        {
            Action = (Action)input.ElementAt(0);
            Value = int.Parse(input.Substring(1));
        }

        public Action Action { get; set; }

        public int Value { get; set; }
    }

    public enum Action
    {
        North = 'N',
        South = 'S',
        East = 'E',
        West = 'W',
        Left = 'L',
        Right = 'R',
        Forward = 'F'
    }
}