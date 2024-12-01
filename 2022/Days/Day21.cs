using Common;

namespace _2022.Days
{
    public class Day21 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputByLineAsync(day);

            var monkeys = input.Select(x => new Monkey(x)).ToDictionary(x => x.Name, x => x);

            var current = monkeys["root"];
            current = PartOne(monkeys, current);

            var partOne = monkeys["root"].Value;
            var partTwo = 1;
            return (day, partOne.ToString(), partTwo.ToString());
        }

        private static Monkey PartOne(Dictionary<string, Monkey> monkeys, Monkey current)
        {
            var queue = new UniqueQueue<Monkey>();
            queue.Enqueue(current);

            while (queue.Any())
            {
                current = queue.Dequeue();
                if (!current.Calculated && current.MonkeyDeps != null)
                {
                    {
                        var one = monkeys[current.MonkeyDeps.Value.monkeyOne];
                        var two = monkeys[current.MonkeyDeps.Value.monkeyTwo];

                        if (!one.Calculated)
                        {
                            queue.Enqueue(one);
                        }

                        if (!two.Calculated)
                        {
                            queue.Enqueue(two);
                        }

                        if (!one.Calculated || !two.Calculated)
                        {
                            queue.Enqueue(current);
                            continue;
                        }

                        switch (current.Operation)
                        {
                            case "*":
                                current.Value = one.Value * two.Value;
                                break;
                            case "+":
                                current.Value = one.Value + two.Value;
                                break;
                            case "-":
                                current.Value = one.Value - two.Value;
                                break;
                            case "/":
                                current.Value = one.Value / two.Value;
                                break;
                        }

                        current.Calculated = true;
                    }
                }
            }

            return current;
        }

        public class UniqueQueue<T>
        {
            private readonly Queue<T> queue = new Queue<T>();
            private readonly HashSet<T> set = new HashSet<T>();

            public UniqueQueue()
            {
                
            }

            public bool Enqueue(T item)
            {
                if(set.Add(item))
                {
                    queue.Enqueue(item);
                    return true;
                }

                return false;
            }

            public bool Any() => set.Any();

            public T Dequeue()
            {
                var item = queue.Dequeue();
                set.Remove(item);
                return item;
            }
        }

        public class Monkey
        {
            public Monkey(string input)
            {
                var parts = input.Split(' ');
                Name = parts[0].Replace(":", string.Empty);
                if(parts.Length > 2)
                {
                    MonkeyDeps = (parts[1], parts[3]);
                    Operation = parts[2];
                } 
                else
                {
                    Value = int.Parse(parts[1]);
                    Calculated = true;
                }
            }

            public string Name { get; init; }
            public (string monkeyOne, string monkeyTwo)? MonkeyDeps { get; set; }
            public string? Operation { get; init; }
            public long Value { get; set; }
            public bool Calculated { get; set; } = false;
        }
    }
}