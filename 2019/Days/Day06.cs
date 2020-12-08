using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;

namespace _2019.Days
{
    public class Day06 : IDay
    {
        private const string CenterOfMass = "COM";
        private const string You = "YOU";
        private const string Santa = "SAN";

        public async Task<(string, string)> Solve(string day)
        {
            var input = await InputHandler.GetInputByLineAsync(day);

            var solarSystem = SetupSolarSystemMap(input);

            var result1 = TotalOrbitsToCom(solarSystem);
            var orbitsFromSan = PathToComFromSource(Santa, solarSystem);
            var orbitsFromYou = PathToComFromSource(You, solarSystem);
            var result2 = orbitsFromSan.Union(orbitsFromYou).Where(x => !orbitsFromYou.Contains(x) || !orbitsFromSan.Contains(x)).ToList();
            result2.Remove(You);
            result2.Remove(Santa);

            return (result1.ToString(), result2.Count.ToString());
        }

        private static int TotalOrbitsToCom(Dictionary<string, string> solarSystem)
        {
            var count = 0;
            foreach (var (_, parent) in solarSystem)
            {
                count += OrbitsToCom(parent, solarSystem);
            }

            return count;
        }

        private static List<string> PathToComFromSource(string child, Dictionary<string, string> planets)
        {
            var path = new List<string> { child };
            return planets[child].Equals(CenterOfMass) ? path : path.Concat(PathToComFromSource(planets[child], planets)).ToList();
        }

        private static int OrbitsToCom(string parent, Dictionary<string, string> planets)
        {
            var count = 0;
            if (parent.Equals(CenterOfMass))
            {
                count++;
                return count;
            }

            count++;
            return count + OrbitsToCom(planets[parent], planets);
        }

        private static Dictionary<string, string> SetupSolarSystemMap(IEnumerable<string> input)
        {
            var solarSystem = new Dictionary<string, string>();
            foreach (var orbit in input)
            {
                var split = orbit.Split(')');
                solarSystem.TryAdd(split[1], split[0]);
            }

            return solarSystem;
        }
    }
}
