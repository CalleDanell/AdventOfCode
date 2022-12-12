using Common;

namespace _2022.Days
{
    public class Day10 : IDay
    {
        const bool PrintSolution = false;

        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var instructions = input.Select(x => new Instruction(x));

            var cycles = 0;
            var running = new List<Instruction>();
            var registerValue = 1;
            var signalStrength = 0;
            var cycleOfInterest = 20;
            var drawingPos = 0;

            foreach(var instruction in instructions)
            {
                while(!instruction.Completed())
                {
                    instruction.Execute();
                    cycles++;
                    drawingPos++;

                    var spritePos = new List<int>() { registerValue, registerValue + 1, registerValue + 2 };
                    
                    if(PrintSolution)
                    {
#pragma warning disable CS0162 // Unreachable code detected
                        if (spritePos.Contains(drawingPos))
                        {
                            Console.Write("#");
                        }
                        else
                        {
                            Console.Write(".");
                        }
#pragma warning restore CS0162 // Unreachable code detected

                        if (cycles % 40 == 0)
                        {
                            drawingPos = 0;
                            Console.WriteLine();
                        }
                    }
                    
                    if (cycles == cycleOfInterest)
                    {
                        signalStrength += cycleOfInterest * registerValue;
                        cycleOfInterest += 40;
                    }
                }

                registerValue += instruction.Value;
            }

            var resPartOne = signalStrength;

            return (day, resPartOne.ToString(), "Enable print to get answer");
        }

        internal class Instruction
        {
            public Instruction(string input)
            {
                var parts = input.Split(" ");
                if (parts[0].Equals("noop"))
                {
                    Cycles = 1;
                    Value = 0;    
                } 
                else
                {
                    Cycles = 2;
                    Value = int.Parse(parts[1]);
                }
             
                State = 0;
            }

            public void Execute() => State++;
            public bool Completed() => Cycles == State;
            public int Cycles { get; private set; }
            public int State { get; set; }
            public int Value { get; private set; }
        }
    }
}