using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = File.ReadAllText("PuzzleInput.txt");
            var input = a.Split("\r\n\r\n");
            var passportFields = new List<string>() { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt"};
            var validPassports = input.Length;
            Console.WriteLine($"The total number of passports is: {validPassports}");
            foreach (var entry in input) 
            {
                var fields = entry
                    .Replace("\n", " ")
                    .Split(' ')
                    .Select(x => x.Split(':')[0])
                    .ToList();
                if (MissingFields(fields, passportFields))
                {
                    validPassports--;
                }
            }
            Console.WriteLine($"The number of valid passports is: {validPassports}");
        }

        public static bool MissingFields(List<string> a, List<string> b)
        {
            return b.Except(a).Any();
        }
    }
}
