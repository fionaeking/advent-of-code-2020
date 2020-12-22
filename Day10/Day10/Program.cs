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
            var input = GetInputAsListNums().OrderBy(x => x).ToList();
            input.Add(input.Last() + 3);
            var differences = input.Zip(input.Skip(1), (x, y) => y - x);
            var numberOnes = differences.Where(x => x == 1).Count();
            var numberThrees = differences.Where(x => x == 3).Count();
            Console.WriteLine($"{numberOnes} differences of 1 jolt; {numberThrees} differences of 3");
            // What is the number of 1-jolt differences multiplied by the number of 3-jolt differences?
            Console.WriteLine($"The answer is {numberOnes * numberThrees}");
            GetAllCombinations(input);
        }

        private static List<int> GetInputAsListNums()
        {
            return File.ReadLines("PuzzleInput.txt").Select(int.Parse).Append(0).ToList();
        }

        private static void GetAllCombinations(List<int> input)
        {
            var numberOnes = new List<int>();
            var count = 0;
            for (int i = 0; i < input.Count - 1; i++)
            {
                if ((input[i + 1] - input[i]) == 1)
                {
                    count++;
                }
                else
                {
                    if (--count >= 1)
                    {
                        numberOnes.Add(count);
                    }
                    count = 0;
                }

            }
            
            long total = 1;
            // 1 jolt - 2 ways, 2 jolt - 4 ways, 3 jolt - 7 ways
            var differentMethods = new int[] { 1, 2, 4, 7 };
            foreach (var c in numberOnes)
            {
                total *= differentMethods[c];
            }

            Console.WriteLine($"The total number of combinations is {total}");
        }
    }
}
