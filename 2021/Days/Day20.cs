using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Common;
using System.Threading.Tasks;
using System.Transactions;
using Common.Coordinates;

namespace _2021.Days
{
    public class Day20 : IDay
    {
        public async Task<(string, string, string)> Solve()
        {
            var input = await InputHandler.GetInputByLineAsync(nameof(Day20));

            var inputList = input.ToList();
            var algorithm = inputList.ElementAt(0);
            inputList.RemoveRange(0, 2);

            var image = new Dictionary<Coordinate, char>();
            for (var y = 0; y < inputList.Count; y++)
                for (var x = 0; x < inputList[y].Length; x++)
                {
                    var value = inputList[y].ToCharArray()[x];
                    image.TryAdd(new Coordinate(x, y), value);
                }

            var xMin = -1;
            var yMin = -1;
            var xMax = inputList[0].Length + 1;
            var yMax = inputList.Count + 1;


            image = GenerateOutputImage(image, algorithm, xMin, yMin, xMax, yMax, 2);
            var resultPartOne = image.Count(x => x.Value == '#');

            image = GenerateOutputImage(image, algorithm, xMin, yMin, xMax, yMax, 48);
            var resultPartTwo = image.Count(x => x.Value == '#');

            return (nameof(Day20), resultPartOne.ToString(), resultPartTwo.ToString());
        }

        private static Dictionary<Coordinate, char> GenerateOutputImage(Dictionary<Coordinate, char> image, string algorithm, int xMin, int yMin, int xMax, int yMax, int iterations)
        {
            var outputImage = new Dictionary<Coordinate, char>();
            var currentIteration = 0;

            while(currentIteration < iterations)
            {
                var buffer = new Dictionary<Coordinate, char>();
                for (var y = yMin; y < yMax; y++)
                for (var x = xMin; x < xMax; x++)
                {
                    var coordinate = new Coordinate(x,y);
                    var binaryString = string.Empty;

                    var adjacent = coordinate.GetAdjacent();
                    adjacent.Add(coordinate);
                    var orderedCoordinates = adjacent.OrderBy(c => c.Y).ThenBy(c => c.X);

                    foreach (var adjacentCoordinate in orderedCoordinates)
                    {
                        image.TryGetValue(adjacentCoordinate, out char value);
                        if (currentIteration % 2 == 0)
                        {
                            if (outputImage.ContainsKey(adjacentCoordinate))
                            {
                                if (value == '#')
                                {
                                    binaryString += '1';
                                }
                                else
                                {
                                    binaryString += '0';
                                }
                            }
                            else
                            {
                                binaryString += '0';
                            }

                        }
                        else
                        {
                            if (outputImage.ContainsKey(adjacentCoordinate))
                            {
                                if (value == '#')
                                {
                                    binaryString += '1';
                                }
                                else
                                {
                                    binaryString += '0';
                                }
                            }
                            else
                            {
                                binaryString += '1';
                            }
                        }
                    }

                    var indexInAlgorithm = Convert.ToInt32(binaryString, 2);
                    var newPixel = algorithm[indexInAlgorithm];

                    buffer.Add(coordinate, newPixel);
                }

                outputImage.Clear();

                foreach (var b in buffer)
                {
                    outputImage.Add(b.Key, b.Value);
                }

                xMin--;
                yMin--;
                xMax++;
                yMax++;
                currentIteration++;
            }

            return outputImage;
        }

        private static Dictionary<Coordinate, int> ExpandImage(Dictionary<Coordinate, int> image, int width, int height, int value)
        {
            var newImage = new Dictionary<Coordinate,int>(image);
            var minX = image.Min(x => x.Key.X);
            var minY = image.Min(x => x.Key.Y);

            for (var x = minX - 1; x <= width; x++)
            {
                newImage.Add(new Coordinate(x, minY - 1), value);
                newImage.Add(new Coordinate(x, height), value);
            }

            for (var y = minY; y < height; y++)
            {
                newImage.Add(new Coordinate(minX - 1, y), value);
                newImage.Add(new Coordinate(width, y), value);
            }

            return newImage;
        }

        private static void DrawImage(Dictionary<Coordinate, int> image, int size)
        {
            Console.WriteLine("-------");
            var count = 0;
            foreach (var c in image.Keys.OrderBy(x => x.Y).ThenBy(y => y.X))
            {
                Console.Write(image[c]);
                count++;

                if (count % size == 0)
                {
                    Console.WriteLine();
                }
            }

            Console.WriteLine("-------");
        }
    }
}