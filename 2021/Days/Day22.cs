using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day22 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day22));
            var rebootInstructions = input.Select(x => new CubeInstruction(x));

            foreach(var instruction in rebootInstructions)
            {
                if(instruction.xRange == null ||
                    instruction.yRange == null ||
                    instruction.zRange == null)
                {
                    //No valid cubes in the area. 
                    continue;
                }

                for(var i = 0; i < instruction.xRange.Count(); i++)
                {

                }
            }

            return (nameof(Day22), string.Empty, string.Empty);
        }

        public List<Cube>GenertateCubes(IEnumerable<int> xRange, IEnumerable<int> yRange, IEnumerable<int> zRange)
        {
            return new List<Cube>();
        }

        public class CubeInstruction
        {
            public CubeInstruction(string input)
            {
                var action = input.Split(' ')[0];
                Action = action.Equals("on") ? true : false;
                var dimensions = input.Split(',');
                xRange = GetRange(dimensions[0]);
                yRange = GetRange(dimensions[1]);
                zRange = GetRange(dimensions[2]);
            }

            private IEnumerable<int>GetRange(string dimension)
            {
                var parts = dimension.Split('=')[1].Split(new string[] { ".." }, StringSplitOptions.None);
                var start = int.Parse(parts[0]);
                var end = int.Parse(parts[1]);

                if(start < -50)
                {
                    start = -50;
                } 
                if(end > 50)
                {
                    end = 50;
                }

                var length = end - start + 1;
                if(length < 0)
                {
                    // No cubes inside the valid area. 
                    return null;
                }

                return Enumerable.Range(start, length);
            }

            public IEnumerable<int> xRange { get; set; } = null;
            public IEnumerable<int> yRange { get; set; } = null;
            public IEnumerable<int> zRange { get; set; } = null;

            public bool Action { get; private set; }
        }

        public class Cube
        {
            public int X { get; set; }
            public int Y { get; set; }
            public int Z { get; set; }
            public bool Status { get; set; }
        }
    }
}