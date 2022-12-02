using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Common
{
    public static class InputHandler
    {
        public static async Task<IEnumerable<string>> GetInputByLineAsync(string day)
        {
            return await File.ReadAllLinesAsync($"Input/{day}.txt");
        }

        public static async Task<string> GetFullInput(string day)
        {
            return await File.ReadAllTextAsync($"Input/{day}.txt");
        }

        public static async Task<IEnumerable<string>> GetInputByCommaSeparationAsync(string day)
        {
            var content = await File.ReadAllTextAsync($"Input/{day}.txt");
            return content.Split(',');
        }

        public static async Task<IEnumerable<string>> GetInputByDashSeparationAsync(string day)
        {
            var content = await File.ReadAllTextAsync($"Input/{day}.txt");
            return content.Split('-');
        }

        /// <summary>
        /// Uses an empty line to group input. 
        /// </summary>
        /// <param name="day">The day to read input for.</param>
        /// <param name="separator">The separator between items in each group.</param>
        /// <returns></returns>
        public static async Task<IEnumerable<string>> GetInputGroupWithNewLineSeparation(string day, string separator)
        {
            var lines = await GetInputByLineAsync(day);
            var contentGroups = new List<string>();
            var contentGroup = string.Empty;
            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    contentGroups.Add(contentGroup);
                    contentGroup = string.Empty;
                }
                else
                {
                    contentGroup = string.Join(separator, contentGroup, line);
                }
            }

            // Add the last group
            contentGroups.Add(contentGroup);
            return contentGroups;
        }
    }
}
