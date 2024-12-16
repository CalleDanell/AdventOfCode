using Common;
using Common.Coordinates;

namespace _2024.Days
{
    public class Day15 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var isMap = true;
            var map = new Dictionary<Coordinate, char>();
            var instructions = new Queue<char>();
            for (var y = 0; y < input.Count(); y++) 
            {
                var line = input.ElementAt(y);
                if(line.Equals(string.Empty))
                {
                    isMap = false; continue;
                }

                for (var x = 0; x < line.Length; x++)
                {
                    if (isMap)
                    {
                        map.Add(new Coordinate(x, y), line[x]);
                    }
                    else
                    {
                        instructions.Enqueue(line[x]);
                    }
                }
            }

            var currentPosition = map.First(x => x.Value.Equals('@')).Key;
            while(instructions.Count > 0)
            {
                var currentDirection = instructions.Dequeue();
                var blocksToMove = new Stack<Coordinate>();
                blocksToMove.Push(currentPosition);
                var blocksInDirection = GetBlocksInDirection(currentPosition, currentDirection, map);
                
                foreach(var blockToMove in blocksInDirection)
                {
                    blocksToMove.Push(blockToMove);
                }

                while (blocksToMove.Any())
                {
                    currentPosition = blocksToMove.Pop();
                    var currentValue = map[currentPosition];
                    
                    Coordinate next;
                    switch (currentDirection)
                    {
                        case '<':
                            next = new Coordinate(currentPosition.X - 1, currentPosition.Y);
                            break;
                        case '>':
                            next = new Coordinate(currentPosition.X + 1, currentPosition.Y);
                            break;
                        case '^':
                            next = new Coordinate(currentPosition.X, currentPosition.Y - 1);
                            break;
                        case 'v':
                            next = new Coordinate(currentPosition.X, currentPosition.Y + 1);
                            break;
                        default:
                            next = new Coordinate(0, 0);
                            break;
                    }

                    var nextValue = map[next];
                    if (nextValue == '#' || nextValue == 'O')
                    {
                        // Dont move, wall or stuck stone
                    }
                    else
                    {
                        map[next] = currentValue; // Move the value
                        map[currentPosition] = '.'; // Make space for other to be moved
                        currentPosition = next;
                    }
                }
            }

            var result = map.Where(x => x.Value.Equals('O')).ToList().Sum(box => box.Key.Y * 100 + box.Key.X);


            Utils.Print(map);
            var partOne = 0;
            var partTwo = 0;

            return (day, partOne.ToString(), partTwo.ToString());
        }

        private Queue<Coordinate> GetBlocksInDirection(Coordinate start, char currentDirection, Dictionary<Coordinate, char > map)
        {
            var result = new Queue<Coordinate>();
            var count = 1;

            Coordinate? next;
            switch (currentDirection)
            {
                case '<':
                    next = new Coordinate(start.X - count, start.Y);
                    while (map[next] == 'O')
                    {
                        count++;
                        result.Enqueue(next);
                        next = new Coordinate(start.X - count, start.Y);
                    }
                    break;
                case '>':
                    next = new Coordinate(start.X + count, start.Y);
                    while (map[next] == 'O')
                    {
                        count++;
                        result.Enqueue(next);
                        next = new Coordinate(start.X + count, start.Y);
                    }
                    break;
                case '^':
                    next = new Coordinate(start.X, start.Y - count);
                    while (map[next] == 'O')
                    {
                        count++;
                        result.Enqueue(next);
                        next = new Coordinate(start.X, start.Y - count);
                    }
                    break;
                case 'v':
                    next = new Coordinate(start.X, start.Y + count);
                    while (map[next] == 'O')
                    {
                        count++;
                        result.Enqueue(next);
                        next = new Coordinate(start.X, start.Y + count);
                    }
                    break;
            }

            return result;
        }
    }
}