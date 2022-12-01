using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2021.Days
{
    public class Day12 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day12));

            var caves = new Stack<Cave>();
            foreach(var line in input)
            {
                var parts = line.Split('-');

                var caveOne = caves.FirstOrDefault(x => x.Id == parts[0]);
                var caveTwo = caves.FirstOrDefault(x => x.Id == parts[1]);
                
                if(caveOne == null)
                {
                    caveOne = new Cave(parts[0]);
                    caves.Push(caveOne);
                }

                if (caveTwo == null)
                {
                    caveTwo = new Cave(parts[1]);
                    caves.Push(caveTwo);
                }

                caveOne.AddConnection(caveTwo);
                caveTwo.AddConnection(caveOne);
            }

            var test = Navigate(caves);

            var resultPartOne = 0;
            var resultPartTwo = 1;

            return (nameof(Day12), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        public int Navigate(IEnumerable<Cave> caves)
        {
            var path = new List<Cave>();
            var current = caves.FirstOrDefault(x => x.IsStart);
            path.Add(current);
            
            while(caves.Any(x => x.)
            {
                current = current.GetNextConnection();
                path.Add(current);

                if (current.IsEnd)
                {
                    return path.Count;
                }

                var next = current.GetNextConnection();
            }
        }
    }

    public class Cave
    {
        private List<Cave> connections = new List<Cave>();

        public Cave(string id)
        {

            if (id.Equals("start"))
            {
                IsStart = true;
            } 
            else if(id.Equals("end"))
            {
                IsEnd = true;
            } 
            else if(id.All(c => char.IsUpper(c)))
            {
                IsLarge = true;
            }
            else
            {
                IsLarge = false;
            }

            Id = id;
        }

        public void AddConnection(Cave cave)
        {
            if(!connections.Any(c => c.Id == cave.Id))
            {
                connections.Add(cave);
            }
        }

        public Cave GetNextConnection()
        {
            var connection = connections.FirstOrDefault();
            if (!connection.IsLarge)
            {
                connections.Remove(connection);
            }

            return connection;
        }

        public string Id { get; set; }
        public bool IsStart { get; set; }
        public bool IsEnd { get; set; }
        public bool IsLarge { get; set; }
        public bool Discovered { get; set; }
    }
}