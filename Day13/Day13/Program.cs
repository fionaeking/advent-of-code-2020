using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("PuzzleInput.txt");
            var initialTimestamp = int.Parse(input[0]);
            var timestamp = initialTimestamp - 1;  // account for initial loop
            var buses = input[1].Split(',').Where(x => x != "x").Select(s => int.Parse(s));
            var earliestBus = new List<int>();
            while(earliestBus.Count<1)
            {
                timestamp++;
                earliestBus.AddRange(buses.Where(x => CheckIfMultiple(x, timestamp) == true));
            }
            var busId = earliestBus.First();
            var minsToWait = timestamp - initialTimestamp;
            Console.WriteLine(busId * minsToWait);
        }

        private static bool CheckIfMultiple(int x, int timestamp)
        {
            return timestamp % x == 0;
        }
    }
}
