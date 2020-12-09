using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2019.Days
{
    public class Day08 : IDay
    {
        private const int Width = 25;
        private const int Height = 6;

        public async Task<(string, string)> Solve(string day)
        {
            var encodedImage = await InputHandler.GetFullInput(day);
            var layers = GetImageLayers(encodedImage);

            var imageLayers = layers.ToList();
            var result1 = IntegrityCheck(imageLayers);

            var result2 = string.Empty;

            return (result1.ToString(), result2);
        }



        private static int IntegrityCheck(IEnumerable<IEnumerable<char>> imageLayers)
        {
            var layers = imageLayers.ToList();
            var min = layers.First();
            foreach (var layer in layers.Where(layer => layer.Count(x => x == '0') < min.Count(x => x == '0')))
            {
                min = layer.ToList();
            }

            var enumerable = min.ToList();
            return enumerable.Count(x => x == '1') * enumerable.Count(x => x == '2');
        }

        private static IEnumerable<IEnumerable<char>> GetImageLayers(string encodedImage)
        {
            var layers = new List<IEnumerable<char>>();
            for (var i = 0; i < encodedImage.Length; i += Height * Width)
            {
                layers.Add(encodedImage.Skip(i).Take(Height * Width));
            }

            return layers;
        }
    }
}