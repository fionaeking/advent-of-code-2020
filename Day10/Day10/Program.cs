using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Day10
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = GetInputAsListNums().OrderBy(x=>x).ToList();
            input.Add(input.Last() + 3);
            var differences = input.Zip(input.Skip(1), (x, y) => y - x);
            var numberOnes = differences.Where(x => x == 1).Count();
            var numberThrees = differences.Where(x => x == 3).Count();
            Console.WriteLine($"{numberOnes} differences of 1 jolt; {numberThrees} differences of 3");
            // What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
            Console.WriteLine($"The answer is {numberOnes*numberThrees}");
        }

        private static List<int> GetInputAsListNums()
        {
            return File.ReadLines("PuzzleInput.txt").Select(int.Parse).Append(0).ToList();
        }
    }
}
