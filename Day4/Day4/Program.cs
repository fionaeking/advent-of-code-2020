using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day4
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllText("PuzzleInput.txt").Split("\r\n\r\n");
            var passportFields = new List<string> { "ecl", "pid", "eyr", "hcl", "byr", "iyr", "hgt" };
            var validPassports = input.Length;
            Console.WriteLine($"The total number of passports is: {validPassports}");
            foreach (var entry in input)
            {
                var fields = entry
                    .Replace("\n", " ").Replace("\r", "")
                    .Split(' ')
                    .ToDictionary(x => x.Split(':')[0], x => x.Split(':')[1]);
                if (MissingFromList(fields.Keys, passportFields))
                {
                    validPassports--;
                }
                else
                {
                    var v = new Validation();
                    if (!v.ValidateFields(fields))
                    {
                        validPassports--;
                    }
                }
            }
            Console.WriteLine($"The number of valid passports is: {validPassports}");
        }

        public static bool MissingFromList(IEnumerable<string> a, IEnumerable<string> b)
        {
            return b.Except(a).Any();
        }
    }  
}
