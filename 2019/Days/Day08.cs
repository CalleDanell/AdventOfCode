using System;
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

        public async Task<(string, string, string)> Solve()
        {
            var encodedImage = await InputHandler.GetFullInput(nameof(Day08));
            var layers = GetImageLayers(encodedImage).ToList();

            var result1 = IntegrityCheck(layers);

            var image = layers.First().ToList();
            foreach (var layer in layers)
            {
                for(var i = 0; i < layer.Count(); i++)
                {
                    var current = image[i];
                    if (current == '0' || current == '1') continue;
                    if (current == '2')
                    {
                        image[i] = layer.ElementAt(i);
                    }
                }
            }

            PrintImage(image);

            return (nameof(Day08), result1.ToString(), "See image above!");
        }

        private static void PrintImage(IReadOnlyCollection<char> image)
        {
            for (var i = 0; i < image.Count; i += Width)
            {
                var row = image.Skip(i).Take(Width);
                Console.WriteLine(new string(row.ToArray()).Replace("0", " ").Replace("1", "X"));
            }
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