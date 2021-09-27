using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2018.Days
{
    public class Day03 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day03));
            var claims = input.Select(x => new Claim(x));
            var claimsMap = MapClaims(claims);

            var resultPartOne = claimsMap.Count(x => x.Value > 1);

            var resultPartTwo = GetNonOverlappingClaim(claims, claimsMap).Id;

            return (nameof(Day03), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        public Dictionary<(int, int), int> MapClaims(IEnumerable<Claim> claims)
        {
            var map = new Dictionary<(int,int), int>();

            foreach (var claim in claims)
            {
                var claimMap = claim.GetClaimMap();
                foreach (var coordinate in claimMap)
                {
                    if (!map.TryAdd(coordinate, 1))
                        map[coordinate]++;
                }
            }

            return map;
        }

        public Claim GetNonOverlappingClaim(IEnumerable<Claim> claims, Dictionary<(int,int), int> claimsMap)
        {
            foreach (var claim in claims)
            {
                var claimMap = claim.GetClaimMap();
                var overlap = false;
                foreach (var coordinate in claimMap)
                {
                    if (claimsMap[coordinate] > 1)
                        overlap = true;
                }

                if (!overlap)
                    return claim;

            }

            return null;
        }


        public class Claim
        {
            public Claim(string input)
            {
                var parts = input.Split(null);
                Id = int.Parse(parts[0].Substring(1).Trim());

                var start = parts[2].Remove(parts[2].Length - 1).Split(',');
                var end = parts[3].Split('x');

                StartX = int.Parse(start[0]);
                StartY = int.Parse(start[1]);
                EndX = StartX + int.Parse(end[0]);
                EndY = StartY + int.Parse(end[1]);
            }

            public int Id { get; }
            public int StartX { get; }
            public int StartY { get; }
            public int EndX { get; }
            public int EndY { get; }

            public List<(int,int)> GetClaimMap()
            {
                var map = new List<(int, int)>();

                for (var i = StartX; i < EndX; i++)
                {
                    for (var j = StartY; j < EndY; j++)
                    {
                        map.Add((i,j));
                    }
                }
                return map;
            }
        }
    }
}
