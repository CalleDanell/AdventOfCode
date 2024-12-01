using Common.Coordinates;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Common
{
    public class Utils
    {
        public static Dictionary<Coordinate, char> GenerateCoordinates(IEnumerable<string> input)
        {
            var coordiantes = new Dictionary<Coordinate, char>();

            for (var i = 0; i < input.Count(); i++)
            {
                for (var j = 0; j < input.ElementAt(i).Length; j++)
                {
                    var value = input.ElementAt(i)[j];
                    coordiantes.Add(new Coordinate(j, i), value);
                }
            }

            return coordiantes;
        }

        public static void Print(Dictionary<Coordinate, char> coords)
        {
            Console.WriteLine();
            
            var xMin = coords.Min(x => x.Key.X);
            var yMin = coords.Min(x => x.Key.Y);
            var xMax = coords.Max(x => x.Key.X);
            var yMax = coords.Max(x => x.Key.Y);

            for (var y = yMin; y <= yMax; y++)
            {
                for (var x = xMin; x <= xMax; x++) 
                { 
                    var current = new Coordinate(x, y);
                    Console.Write(coords[current]);
                }
                Console.WriteLine();
            }
        }

        public static void Print(List<Coordinate> coords, char symbol)
        {
            Console.WriteLine();

            var xMin = coords.Min(x => x.X);
            var yMin = coords.Min(x => x.Y);
            var xMax = coords.Max(x => x.X);
            var yMax = coords.Max(x => x.Y);

            for (var y = yMax ; y >= yMin; y--)
            {
                for (var x = xMin; x <= xMax; x++)
                {
                    var current = new Coordinate(x, y);
                    if(coords.Contains(current))
                    {
                        Console.Write(symbol);
                    } else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }

        public static void AppendToFile(List<Coordinate> coords, char symbol, string fileName)
        {
            var path = fileName + ".txt";

            var xMin = coords.Min(x => x.X);
            var yMin = coords.Min(x => x.Y);
            var xMax = coords.Max(x => x.X);
            var yMax = coords.Max(x => x.Y);

            var lines = new List<string>();
            for (var y = yMax; y >= yMin; y--)
            {
                var line = string.Empty;
                for (var x = xMin; x <= xMax; x++)
                {
                    var current = new Coordinate(x, y);
                    if (coords.Contains(current))
                    {
                        line += symbol;
                    }
                    else
                    {
                        line += ".";
                    }
                }
                lines.Add(line);
            }

            File.AppendAllLines(path, lines);
        }
    }
}
