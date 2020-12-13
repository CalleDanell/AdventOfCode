using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day08 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day08));
            var instructions = input.Select(x => new Instruction(x));

            var console = new GameConsole(instructions.ToList());      
            
            var resultPartOne = console.Execute() ? 0 : console.Accumulator();
            
            console.Reload();
            
            var resultPartTwo = FindTheCorruptInstruction(console);

            return (nameof(Day08), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static int FindTheCorruptInstruction(GameConsole console)
        {
            var resultPartTwo = 0;
            foreach (var ins in console.Instructions)
            {
                var temp = ins.Type;
                switch (ins.Type)
                {
                    case Operation.jmp:
                        ins.Type = Operation.nop;
                        break;
                    case Operation.nop:
                        ins.Type = Operation.jmp;
                        break;
                    default:
                        continue;
                }

                if (console.Execute())
                {
                    resultPartTwo = console.Accumulator();
                }

                ins.Type = temp;
                console.Reload();
            }

            return resultPartTwo;
        }
    }

    public class GameConsole
    {
        private int accumulator;
        private int pointer;

        public List<Instruction> Instructions { get; private set; }

        public GameConsole(List<Instruction> instructions)
        {
            Instructions = instructions;
        }

        public void Reload()
        {
            accumulator = 0;
            pointer = 0;
            Instructions.ForEach(x => x.Executed = false);
        }

        public int Accumulator() => accumulator;

        public bool Execute()
        {
            var instruction = Instructions.ElementAt(pointer);
            while (!instruction.Executed)
            {
                switch (instruction.Type)
                {
                    case Operation.nop:
                        pointer++;
                        break;
                    case Operation.acc:
                        pointer++;
                        accumulator += instruction.Argument;
                        break;
                    case Operation.jmp:
                        pointer += instruction.Argument;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                instruction.Executed = true;

                if(pointer >= Instructions.Count())
                {
                    return true;
                }

                instruction = Instructions.ElementAt(pointer);
            }

            return false;
        }
    }

    public class Instruction
    {
        public Instruction(string input)
        {
            var parts = input.Split(" ");
            Type = Enum.Parse<Operation>(parts[0]);
            Argument = int.Parse(parts[1]);
        }

        public Operation Type { get; set; }
        public int Argument { get; set;  }
        public bool Executed { get; set; }
    }

    public enum Operation
    {
        nop = 01,
        acc = 02,
        jmp = 03
    }
}