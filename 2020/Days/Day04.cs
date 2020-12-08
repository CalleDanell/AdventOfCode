using Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2020.Days
{
    public class Day04 : IDay
    {
        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputWithNewLineSeparation(day, " ");
            var passports = input.Select(x => new Passport(
                Regex.Match(x, "(byr:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(iyr:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(eyr:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(hgt:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(hcl:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(ecl:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(pid:)([^\\s]+)").Groups[2].Value,
                Regex.Match(x, "(cid:)([^\\s]+)").Groups[2].Value
            ));

            var enumerable = passports.ToList();
            var resultPartOne = enumerable.Count(x => x.IsValidFirstTime);
            var resultPartTwo = enumerable.Count(x => x.IsValidSecondTime);

            return (resultPartOne.ToString(), resultPartTwo.ToString());
        }
    }

    public class Passport
    {
        public Passport(string birthYear, string issueYear, string expirationYear, string height, string hairColor, string eyeColor, string passportId, string countryId)
        {
            BirthYear = ValidateExist(birthYear) ? birthYear : ValidateYears(birthYear, 1920, 2002);
            IssueYear = ValidateExist(issueYear) ? issueYear : ValidateYears(issueYear, 2010, 2020);
            ExpirationYear = ValidateExist(expirationYear) ? expirationYear : ValidateYears(expirationYear, 2020, 2030);
            Height = ValidateExist(height) ? height : ValidateHeight(height, 150, 193, 59, 76);
            HairColor = ValidateExist(hairColor) ? hairColor : ValidateHairColor(hairColor);
            EyeColor = ValidateExist(eyeColor) ? eyeColor : ValidateEyeColor(eyeColor);
            PassportId = ValidateExist(passportId) ? passportId : ValidatePid(passportId);
            CountryId = countryId;
        }

        private bool ValidateExist(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                IsValidFirstTime = false;
                IsValidSecondTime = false;
                return true;
            }

            return false;
        }

        private string ValidateHairColor(string s)
        {
            if (!Regex.Match(s, "#[a-zA-Z0-9]{6,6}").Groups[0].Success || s.Length != 7)
            {
                IsValidSecondTime = false;
            }
            return s;
        }

        private string ValidatePid(string s)
        {
            if (s.Length != 9 || !int.TryParse(s, out int _))
            {
                IsValidSecondTime = false;
            }
            return s;
        }

        private string ValidateEyeColor(string s)
        {
            var validColors = new[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!validColors.Contains(s))
            {
                IsValidSecondTime = false;
            }
            return s;
        }

        private string ValidateHeight(string s, int minCm, int maxCm, int minIn, int maxIn)
        {
            var unit = s.Substring(s.Length - 2, 2);
            var height = s.Substring(0, s.Length - 2);
            if (unit.Equals("cm"))
            {
                if (!int.TryParse(height, out var heightCm) || heightCm < minCm || heightCm > maxCm)
                {
                    IsValidSecondTime = false;
                }
            }
            else if (unit.Equals("in"))
            {
                if (!int.TryParse(height, out var heightIn) || heightIn < minIn || heightIn > maxIn)
                {
                    IsValidSecondTime = false;
                }
            }
            else
            {
                IsValidSecondTime = false;
            }

            return s;
        }


        private string ValidateYears(string s, int min, int max)
        {
            if (!int.TryParse(s, out var year) || year < min || year > max)
            {
                IsValidSecondTime = false;
            }

            return s;
        }

        public bool IsValidFirstTime { get; private set; } = true;
        public bool IsValidSecondTime { get; private set; } = true;
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