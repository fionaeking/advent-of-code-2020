using System;
using System.IO;
using System.Linq;

namespace Day6
{
    class Program
    {
        static void Main()
        {
            // Read in values and split by newline
            var input = File.ReadAllText("PuzzleInput.txt")
                .Split("\r\n\r\n")
                .ToList();
            var sum = 0;
            foreach (var x in input)
            {
                sum += x.Replace("\n", "")
                    .Replace("\r", "")
                    .Distinct()
                    .Count();
            }
            Console.WriteLine(sum);
        }
    }
}
