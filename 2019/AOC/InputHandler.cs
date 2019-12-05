using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AOC
{
    public static class InputHandler
    {
        public static async Task<IEnumerable<string>> GetInputByLineAsync(string day)
        {
            return await File.ReadAllLinesAsync($"Input/{day}.txt");
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
    }
}
