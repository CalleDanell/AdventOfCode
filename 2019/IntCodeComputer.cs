using System;
using System.Collections.Generic;
using System.Linq;

namespace _2019
{
    public class IntCodeComputer
    {
        private List<int> input;
        private List<int> instructions;
        private readonly List<int> originalInstructions;
        private int pointer;

        public IntCodeComputer(List<int> instructions)
        {
            this.originalInstructions = new List<int>(instructions);
            this.instructions = new List<int>(instructions);
        }

        public void Input(params int[] userInput)
        {
            this.input = userInput.ToList();
        }

        public void Restore()
        {
            this.instructions = new List<int>(originalInstructions);
            pointer = 0;
        }

        public ComputerState ComputerState { get; set; }

        public void SetNounAndVerb(int noun, int verb)
        {
            instructions[1] = noun;
            instructions[2] = verb;
        }
        
        public int Run()
        {
            ComputerState = ComputerState.Running;
            while (ComputerState == ComputerState.Running)
            {
                var operation = GetOperation(instructions[pointer]);
                Execute(operation);
            }

            return instructions[0];
        }

        private static string GetOperation(int i)
        {
            return i.ToString().PadLeft(5, '0');
        }

        private ComputerState Execute(string operation)
        {
            var code = (OperationCode)int.Parse(operation.Substring(3,2));

            var parameterMode = operation.ToCharArray();
            var modeOne = int.Parse(parameterMode[2].ToString());
            var modeTwo = int.Parse(parameterMode[1].ToString());
            var modeThree = int.Parse(parameterMode[0].ToString());          
            
            switch (code)
            {
                case OperationCode.Add:
                    instructions[modeThree == 0 ? instructions[pointer + 3] : pointer + 3] = GetParameter(instructions, pointer, modeOne, 1) + GetParameter(instructions, pointer, modeTwo, 2);
                    pointer += 4;
                    break;

                case OperationCode.Multiply:
                    instructions[modeThree == 0 ? instructions[pointer + 3] : pointer + 3] = GetParameter(instructions, pointer, modeOne, 1) * GetParameter(instructions, pointer, modeTwo, 2);
                    pointer += 4;
                    break;

                case OperationCode.Input:
                    instructions[instructions[pointer + 1]] = input[0];
                    pointer += 2;
                    input.RemoveAt(0);
                    break;

                case OperationCode.Output:
                    instructions[0] = GetParameter(instructions, pointer, modeOne, 1);
                    pointer += 2;
                    ComputerState = ComputerState.Paused;
                    break;

                case OperationCode.JumpIfTrue:
                    if (GetParameter(instructions, pointer, modeOne, 1) != 0)
                    {
                        pointer = GetParameter(instructions, pointer, modeTwo, 2);
                    }
                    else
                    {
                        pointer += 3;
                    }
                    break;

                case OperationCode.JumpIfFalse:
                    if (GetParameter(instructions, pointer, modeOne, 1) == 0)
                    {
                        pointer = GetParameter(instructions, pointer, modeTwo, 2);
                    }
                    else
                    {
                        pointer += 3;
                    }
                    break;

                case OperationCode.LessThan:
                    if (GetParameter(instructions, pointer, modeOne, 1) < GetParameter(instructions, pointer, modeTwo, 2))
                    {
                        instructions[modeThree == 0 ? instructions[pointer + 3] : pointer + 3] = 1;
                    }
                    else
                    {
                        instructions[modeThree == 0 ? instructions[pointer + 3] : pointer + 3] = 0;
                    }

                    pointer += 4;
                    break;

                case OperationCode.Equals:
                    if (GetParameter(instructions, pointer, modeOne, 1) == GetParameter(instructions, pointer, modeTwo, 2))
                    {
                        instructions[modeThree == 0 ? instructions[pointer + 3] : pointer + 3] = 1;
                    }
                    else
                    {
                        instructions[modeThree == 0 ? instructions[pointer + 3] : pointer + 3] = 0;
                    }

                    pointer += 4;
                    break;

                case OperationCode.Stop:
                    ComputerState = ComputerState.Stopped;
                    break;
            }

            return ComputerState;
        }

        private static int GetParameter(IList<int> instructions, int pointer, int mode, int index)
        {
            return mode == 0 ? instructions[instructions[pointer + index]] : instructions[pointer + index];
        }
    }
}