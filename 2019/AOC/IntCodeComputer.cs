using System.Collections.Generic;
using System.Linq;

namespace AOC
{
    public static class IntCodeComputer
    {
        public static int GetNounAndVerbForOutput(List<int> input, int maxParameterValue, int outputValue)
        {
            foreach (var noun in Enumerable.Range(0, maxParameterValue))
            foreach (var verb in Enumerable.Range(0, maxParameterValue))
            {
                var testInput = new List<int>(input);
                if (GetProgramOutput(testInput, noun, verb) == outputValue)
                {
                    return 100 * noun + verb;
                }
            }

            return 0;
        }

        public static int GetProgramOutput(List<int> input, int noun, int verb)
        {
            input[1] = noun;
            input[2] = verb;

            for (var i = 0; i < input.Count; i += 4)
            {
                switch ((OperationCode)input[i])
                {
                    case OperationCode.Add:
                        CalculateIntCode(input, i, OperationCode.Add);
                        break;
                    case OperationCode.Multiply:
                        CalculateIntCode(input, i, OperationCode.Multiply);
                        break;
                    case OperationCode.Stop:
                        break;
                }
            }

            return input[0];
        }

        private static void CalculateIntCode(IList<int> input, int operationIndex, OperationCode operation)
        {
            var noun = input[input[operationIndex + 1]];
            var verb = input[input[operationIndex + 2]];
            var outputIndex = input[operationIndex + 3];

            if (operation == OperationCode.Add)
            {
                input[outputIndex] = noun + verb;
            }
            else if (operation == OperationCode.Multiply)
            {
                input[outputIndex] = noun * verb;
            }
        }
    }
}
