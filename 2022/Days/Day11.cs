using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace Common.Template
{
    public class Day11 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = GetType().Name;
            var input = await InputHandler.GetInputGroupWithNewLineSeparation(day, "$");
            
            var monkeys = input.Select(x => new Monkey(x));

            long resultPartOne = PlayMonkeyBusiness(monkeys, 20, true);
            long resultPartTwo = PlayMonkeyBusiness(monkeys, 10000, false);

            return (day, resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static long PlayMonkeyBusiness(IEnumerable<Monkey> monkeyInput, int roundsToPlay, bool veryWorried)
        {
            var monkeys = new Queue<Monkey>(monkeyInput);
            var currentRound = 0;

            var leastCommonDenominator = 1;
            foreach(var monkey in monkeys)
            {
                leastCommonDenominator = leastCommonDenominator * monkey.TestNumber;
            }

            while (currentRound < roundsToPlay)
            {
                var monkey = monkeys.Dequeue();
                monkeys.Enqueue(monkey);

                while (monkey.Items.Any())
                {
                    long currentItem = monkey.Items.Dequeue();
                    long worryLevel = monkey.Operation.Evaluate(currentItem);

                    if(veryWorried)
                    {
                        worryLevel = worryLevel / 3;
                    } 
                    else
                    {
                        worryLevel = worryLevel % leastCommonDenominator;
                    }

                    var test = worryLevel % monkey.TestNumber;
                    if (test == 0)
                    {
                        monkeys.First(x => x.Id == monkey.TestTrue).Items.Enqueue(worryLevel);
                    }
                    else
                    {
                        monkeys.First(x => x.Id == monkey.TestFalse).Items.Enqueue(worryLevel);
                    }

                    monkey.Inspections++;
                }

                if (monkey.Id == monkeys.Count - 1)
                {
                    currentRound++;
                }
            }

            var orderedMonkeys = monkeys.OrderByDescending(x => x.Inspections);
            return orderedMonkeys.First().Inspections * orderedMonkeys.Skip(1).First().Inspections;
        }

        public class Monkey
        {
            public Monkey(string input)
            {
                var parts = input.Trim().Split('$').Skip(1);
                
                Id = int.Parse(Regex.Match(parts.ElementAt(0).Trim(), @"\d+").Value);
                Items = new Queue<long>(parts.ElementAt(1).Split(":")[1].Trim().Split(",").Select(long.Parse));
                Operation = new Operation(parts.ElementAt(2).Split("=")[1].Trim());

                TestNumber = int.Parse(Regex.Match(parts.ElementAt(3).Trim(), @"\d+").Value);
                TestTrue = int.Parse(Regex.Match(parts.ElementAt(4).Trim(), @"\d+").Value);
                TestFalse = int.Parse(Regex.Match(parts.ElementAt(5).Trim(), @"\d+").Value);
            }

            public Operation Operation { get; }
            public int Id { get; }
            public int TestNumber { get; }
            public int TestTrue { get; }
            public int TestFalse { get; }
            public Queue<long> Items { get; }
            public long Inspections { get; set; }
        }

        public class Operation
        {
            private string statement;
            public Operation(string input)
            {
                this.statement = input;

                var parts = statement.Trim().Split(" ");
                First = parts[0];
                Method = parts[1];
                Second = parts[2];
            }

            public string First { get; }
            public string Second { get; }
            public string Method { get; }

            public long Evaluate(long currentWorryLevel)
            {
                var firstInt = long.Parse(First.Replace("old", currentWorryLevel.ToString()));
                var secondInt = long.Parse(Second.Replace("old", currentWorryLevel.ToString()));
                switch (Method)
                {
                    case "*":
                        return firstInt * secondInt;
                    case "+":
                        return firstInt + secondInt;
                }

                return 0;
            }
        }
    }
}