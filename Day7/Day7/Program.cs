using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Day7
{
    class Program
    {

        static void Main(string[] args)
        {
            var input = File.ReadLines("PuzzleInput.txt")
                .ToDictionary(
            l => Regex.Match(l, @"^(\w+ \w+)").Groups[1].Value,
            l => l.Contains("no other bags.")
              ? new Dictionary<string, int>() { }//Bags.Empty
              : Regex.Matches(l, @"(\d+) (\w+ \w+) bags?[,.]\s?")
                .ToDictionary(
                  x => x.Groups[2].Value,
                  x => int.Parse(x.Groups[1].Value)));

            Console.WriteLine(PartOne("shiny gold", input));
            Console.WriteLine(PartTwo("shiny gold", input));
        }

        static int PartOne(string bag, Dictionary<string, Dictionary<string, int>> input)
        {
            var sum = 0;
            foreach (var b in input.Values)
            {
                sum += CheckContainsBag(b, bag, input) ? 1 : 0;
            }
            return sum;
        }

        static bool CheckContainsBag(Dictionary<string, int> bags, string bag, Dictionary<string, Dictionary<string, int>> input)
        {
            return bags.ContainsKey(bag) || bags.Keys.Any(b => CheckContainsBag(input[b], bag, input));
        }

        static int PartTwo(string bag, Dictionary<string, Dictionary<string, int>> input)
        {
            return input[bag].Sum(b => b.Value + b.Value * PartTwo(b.Key, input));
        }
        
    }
}

