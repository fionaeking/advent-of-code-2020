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
                var numberPeople = x.Split("\r\n").Count();
                sum += x.Replace("\n", "")
                    .Replace("\r", "")
                    .GroupBy(i => i)
                    .Select(grp => grp.Count())
                    .Where(count => count==numberPeople)
                    .Count();
            }
            Console.WriteLine(sum);
        }
    }
}
