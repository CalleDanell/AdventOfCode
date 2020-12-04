using Common;
using Common.Days;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var rawPassports = GetRawPassports(input);
            var passports = rawPassports.Select(x => 
            {
                var passportPropertyPairs = x.Trim().Split(' ');
                return new Passport(
                        Regex.Match(x, "(byr:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(iyr:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(eyr:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(hgt:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(hcl:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(ecl:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(pid:)([^\\s]+)")?.Groups[2]?.Value,
                        Regex.Match(x, "(cid:)([^\\s]+)")?.Groups[2]?.Value
                    );
            });

            var resultPartOne = passports.Count(x => x.IsValid);
            var test = passports.Where(x => !x.IsValid);
            var resultPartTwo = string.Empty;
            
            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static IEnumerable<string> GetRawPassports(IEnumerable<string> input)
        {
            var rawPassports = new List<string>();
            var passport = string.Empty;
            var emptylines = 0;
            foreach (var line in input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    emptylines++;
                    rawPassports.Add(passport);
                    passport = string.Empty;
                }
                else
                {
                    passport = string.Join(" ", passport, line);
                }
            }

            // Add the last passport
            rawPassports.Add(passport);
            return rawPassports;
        }
    }

    public class Passport
    {
        public Passport(string birthYear, string issueYear, string expirationYear, string height, string hairColor, string eyeColor, string passportId, string countryId)
        {
            BirthYear = Validate(birthYear);
            IssueYear = Validate(issueYear);
            ExpirationYear = Validate(expirationYear);
            Height = Validate(height);
            HairColor = Validate(hairColor);
            EyeColor = Validate(eyeColor);
            PassportId = Validate(passportId);
            CountryId = countryId;
        }

        private string Validate(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                IsValid = false;
            }
            return s;
        }


        public bool IsValid { get; private set; } = true;
        public string BirthYear { get; }
        public string IssueYear { get; }
        public string ExpirationYear { get; }
        public string Height { get; }
        public string HairColor { get; }
        public string EyeColor { get; }
        public string PassportId { get; }
        public string CountryId { get; }
    }
}