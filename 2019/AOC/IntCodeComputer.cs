using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC
{
    public static class IntCodeComputer
    {
        private static int[] input;

        public static int GetNounAndVerbForOutput(List<int> program, int maxParameterValue, int outputValue)
        {
            foreach (var noun in Enumerable.Range(0, maxParameterValue))
            foreach (var verb in Enumerable.Range(0, maxParameterValue))
            {
                var testInput = new List<int>(program);
                if (GetProgramOutputByVerbAndNoun(testInput, noun, verb) == outputValue)
                {
                    return 100 * noun + verb;
                }
            }

            return 0;
        }

        public static int GetProgramOutputByVerbAndNoun(List<int> program, int noun, int verb)
        {
            program[1] = noun;
            program[2] = verb;
            var counter = 0;

            RunProgram(program, counter);

            return program[0];
        }

        public static int GetProgramOutputByInput(List<int> program, params int[] userInput)
        {
            var counter = 0;
            input = userInput;

            RunProgram(program, counter);

            return program[0];
        }

        private static void RunProgram(List<int> program, int counter)
        {
            while (counter < program.Count)
            {
                var operation = GetOperation(program[counter]);
                if (!Execute(program, ref counter, operation))
                {
                    break;
                }
            }
        }

        private static string GetOperation(int i)
        {
            return i.ToString().PadLeft(5, '0');
        }

        private static bool Execute(IList<int> instructions, ref int counter, string operation)
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
                parameterOne = GetParameter(instructions, counter, modeOne, 1);
                parameterTwo = GetParameter(instructions, counter, modeTwo, 2);
                outputIndex = modeThree == 0 ? instructions[counter + 3] : counter + 3;
            }
            catch (ArgumentOutOfRangeException)
            {
                // Don't need the params 
            }

            switch (code)
            {
                case OperationCode.Add:
                    instructions[outputIndex] = parameterOne + parameterTwo;
                    counter += 4;

                    return true;

                case OperationCode.Multiply:
                    instructions[outputIndex] = parameterOne * parameterTwo;
                    counter += 4;

                    return true;

                case OperationCode.Input:
                    instructions[instructions[counter + 1]] = input[0];
                    counter += 2;
                    var inputList = input.ToList();
                    inputList.RemoveAt(0);
                    input = inputList.ToArray();

                    return true;

                case OperationCode.Output:
                    instructions[0] = parameterOne;
                    counter += 2;
                    
                    return true;

                case OperationCode.JumpIfTrue:
                    if (parameterOne != 0)
                    {
                        counter = parameterTwo;
                    }
                    else
                    {
                        counter += 3;
                    }
                    
                    return true;

                case OperationCode.JumpIfFalse:
                    if (parameterOne == 0)
                    {
                        counter = parameterTwo;
                    }
                    else
                    {
                        counter += 3;
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

                    counter += 4;

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

                    counter += 4;

                    return true;

                default:
                    counter += 4;
                    return false;
            }
        }

        private static int GetParameter(IList<int> instructions, int counter, int param, int index)
        {
            return param == 0 ? instructions[instructions[counter + index]] : instructions[counter + index];
        }
    }
}
