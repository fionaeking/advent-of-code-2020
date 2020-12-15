using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day7
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("PuzzleInput.txt");
            var outerDict = new Dictionary<string, Dictionary<string, int>> { };
            foreach (var i in input)
            {
                var kvp = i.Replace(".\r\n", "").Replace(" bags", "")
                    .Replace(" bag", "").Replace(".", "")
                    .Split(" contain ");
                var key = kvp[0];
                if (!kvp[1].StartsWith("no other"))
                {
                    var values = kvp[1].Split(", ");
                    var innerDict = new Dictionary<string, int>{ };
                    foreach (var value in values)
                    {
                        var y = new Tuple<string, int>(value[2..], int.Parse(value.Substring(0, 1)));
                        innerDict.Add(y.Item1, y.Item2);
                    }
                    outerDict.Add(key, innerDict);
                }
            }

            // We only care about bags which can hold gold bags initially
            var allBags = outerDict.Where(x => x.Value.ContainsKey("shiny gold"))
                .Select(x => x.Key).ToList();
            var currBags = allBags;
            // Find all bags which can hold these bags!
            // We don't really care about numbers for now
            var diff = allBags.Count();
            while (diff>0)
            {
                List<string> nextBags = new List<string> { };
                foreach (var bag in currBags)
                {
                    nextBags.AddRange(outerDict.Where(x => x.Value.ContainsKey(bag)).Select(x => x.Key));
                }
                allBags.AddRange(nextBags);
                currBags = nextBags;
                diff = nextBags.Count();
            }
            Console.WriteLine(allBags.Distinct().Count());

        }
    }
}
