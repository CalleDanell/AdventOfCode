using Common;

namespace _2022.Days
{
    public class Day06 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var day = this.GetType().Name;
            var input = await InputHandler.GetFullInput(day);
            
            var packetMarker = GetMarker(input, 4);
            var messageMarker = GetMarker(input, 14);

            return (day, packetMarker.ToString(), messageMarker.ToString());
        }

        private int GetMarker(string input, int segmentSize)
        {
            var chars = new List<char>(input);
            for (var i = 0; i < input.Length - segmentSize; i++)
            {
                var segment = chars.GetRange(i, segmentSize).ToHashSet();
                if (segment.Count == segmentSize)
                {
                    return i + segmentSize;
                }
            }

            return 0;
        }
    }
}