using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC
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

        public bool IsRunning { get; set; }

        public void SetNounAndVerb(int noun, int verb)
        {
            instructions[1] = noun;
            instructions[2] = verb;
        }
        
        public int Run()
        {
            IsRunning = true;
            while (pointer < instructions.Count && IsRunning)
            {
                var operation = GetOperation(instructions[pointer]);
                if (!Execute(operation))
                {
                    break;
                }
            }

            return instructions[0];
        }

        private static string GetOperation(int i)
        {
            return i.ToString().PadLeft(5, '0');
        }

        private bool Execute(string operation)
        {
            var code = (OperationCode)int.Parse(operation.Substring(4, 1));

            var parameterMode = operation.ToCharArray();
            var modeOne = int.Parse(parameterMode[2].ToString());
            var modeTwo = int.Parse(parameterMode[1].ToString());
            var modeThree = int.Parse(parameterMode[0].ToString());

            var parameterOne = 0;
            var parameterTwo = 0;
            var outputIndex = 0;

            try
            {
                parameterOne = GetParameter(instructions, pointer, modeOne, 1);
                parameterTwo = GetParameter(instructions, pointer, modeTwo, 2);
                outputIndex = modeThree == 0 ? instructions[pointer + 3] : pointer + 3;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Don't need the params 
            }

            switch (code)
            {
                case OperationCode.Add:
                    instructions[outputIndex] = parameterOne + parameterTwo;
                    pointer += 4;

                    return true;

                case OperationCode.Multiply:
                    instructions[outputIndex] = parameterOne * parameterTwo;
                    pointer += 4;

                    return true;

                case OperationCode.Input:
                    instructions[instructions[pointer + 1]] = input[0];
                    pointer += 2;
                    input.RemoveAt(0);

                    return true;

                case OperationCode.Output:
                    instructions[0] = parameterOne;
                    pointer += 2;
                    
                    return true;

                case OperationCode.JumpIfTrue:
                    if (parameterOne != 0)
                    {
                        pointer = parameterTwo;
                    }
                    else
                    {
                        pointer += 3;
                    }
                    
                    return true;

                case OperationCode.JumpIfFalse:
                    if (parameterOne == 0)
                    {
                        pointer = parameterTwo;
                    }
                    else
                    {
                        pointer += 3;
                    }

                    return true;

                case OperationCode.LessThan:
                    if (parameterOne < parameterTwo)
                    {
                        instructions[outputIndex] = 1;
                    }
                    else
                    {
                        instructions[outputIndex] = 0;
                    }

                    pointer += 4;

                    return true;

                case OperationCode.Equals:
                    if (parameterOne == parameterTwo)
                    {
                        instructions[outputIndex] = 1;
                    }
                    else
                    {
                        instructions[outputIndex] = 0;
                    }

                    pointer += 4;

                    return true;

                default:
                    IsRunning = false;
                    return false;
            }
        }

        private static int GetParameter(IList<int> instructions, int counter, int param, int index)
        {
            return param == 0 ? instructions[instructions[counter + index]] : instructions[counter + index];
        }
    }
}
