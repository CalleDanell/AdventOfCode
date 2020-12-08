using System.Text.RegularExpressions;

namespace Common
{
    public static class StringExtensions
    {
        public static string RemoveIntegers(this string input)
        {
            return Regex.Replace(input, @"[\d-]", string.Empty);
        }
    }
}
