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
            // PartOne(input);
            PartTwo(input);
        }

        private static void PartTwo(string[] input)
        {
            var buses = input[1].Split(',');
            var tuples = new List<Tuple<int, int>>();
            for (int i = 0; i < buses.Count(); i++)
            {
                if (buses[i] != "x")
                {
                    var busId = int.Parse(buses[i]);
                    tuples.Add(new Tuple<int, int>(busId, i));
                }
            }
            tuples.OrderByDescending(x => x.Item1);
            var timestamp = 100000000000000;
            long amountToAdd = tuples.First().Item1;
            while ((timestamp + tuples.First().Item2) % tuples.First().Item1 != 0)
            {
                timestamp++;
            }
            tuples.RemoveAt(0);
            while (tuples.Count > 0)
            {
                timestamp = GetNewTimestamp(timestamp, amountToAdd, tuples.First().Item1, tuples.First().Item2);
                amountToAdd *= tuples.First().Item1;
                tuples.RemoveAt(0);
            }
            Console.WriteLine(timestamp);
        }

        private static long GetNewTimestamp(long timestamp, long amountToAdd, int item1, int item2)
        {
            while ((timestamp + item2) % item1 != 0)
            {
                timestamp += amountToAdd;
            }
            return timestamp;
        }

        private static void PartOne(string[] input)
        {
            var initialTimestamp = int.Parse(input[0]);
            var timestamp = initialTimestamp - 1;  // account for initial loop
            var buses = input[1].Split(',').Where(x => x != "x").Select(s => int.Parse(s));
            var earliestBus = new List<int>();
            while (earliestBus.Count < 1)
            {
                timestamp++;
                earliestBus.AddRange(buses.Where(x => CheckIfMultiple(x, timestamp)));
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
