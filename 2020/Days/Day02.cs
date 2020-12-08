using System;
using Common;
using System.Linq;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day02 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);
            var passwordsAndRules = input.Select(x =>
            {
                var parts = x.Split(':');
                return Tuple.Create(new PasswordRuleSet(parts[0]), parts[1]);
            }).ToArray();

            var resultPartOne = passwordsAndRules.Count(IsValidPasswordOne);
            var resultPartTwo = passwordsAndRules.Count(IsValidPasswordTwo);
            
            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static bool IsValidPasswordOne(Tuple<PasswordRuleSet, string> passwordAndRule)
        {
            var (rule, password) = passwordAndRule;
            var charCount = password.Count(x => x.Equals(rule.Char));
            return charCount <= rule.Last && charCount >= rule.First;
        }

        private static bool IsValidPasswordTwo(Tuple<PasswordRuleSet, string> passwordAndRule)
        {
            var (rule, password) = passwordAndRule;
            var firstOk = password[rule.First].Equals(rule.Char);
            var secondOk = password[rule.Last].Equals(rule.Char);

            if (firstOk && secondOk)
            {
                return false;
            }

            return firstOk || secondOk;
        }
    }

    public class PasswordRuleSet
    {
        public PasswordRuleSet(string rule)
        {
            var parts = rule.Split('-');
            First = int.Parse(parts[0]);
            Last = int.Parse(new string(parts[1].Where(char.IsDigit).ToArray()));
            Char = rule.Last();
        }

        public int Last { get; }

        public int First { get;  }

        public char Char { get; }
    }
}