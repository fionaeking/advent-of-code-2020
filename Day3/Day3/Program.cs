using System;
using System.IO;
using System.Collections.Generic;

namespace Day3
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("PuzzleInput.txt");
            var slopes = new List<Tuple<int, int>>() 
            { 
                new Tuple<int, int>(1, 1),
                new Tuple<int, int>(3, 1),
                new Tuple<int, int>(5, 1),
                new Tuple<int, int>(7, 1),
                new Tuple<int, int>(1, 2)
            };

            var product = 1;
            foreach (var slope in slopes)
            {
                product *= GetTreeCount(slope, input);
            }
            Console.WriteLine($"The result is {product}");
        }

        private static int GetTreeCount(Tuple<int, int> slope, string[] input)
        {
            var deltaX = slope.Item1;
            var deltaY = slope.Item2;
            var currPointX = 0;
            var currPointY = 0;
            var maxX = input[0].Length;
            var maxY = input.Length - 1;
            var treeCount = 0;
            while (currPointY <= maxY)
            {
                // Check point for trees
                treeCount += (input[currPointY][currPointX] == '#') ? 1 : 0;
                currPointX += deltaX;
                currPointX %= maxX;
                currPointY += deltaY;
            }
            return treeCount;
        }
    }
}
