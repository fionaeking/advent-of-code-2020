using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Day9
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("PuzzleInput.txt").ToList()
                .Select(r => int.Parse((r))).ToList();

            var numPartOne = GetPartOne(input); 
            Console.WriteLine("Answer to part one: " + numPartOne);
            Console.WriteLine(PartTwo(input, numPartOne));
        }

        private static int PartTwo(List<int> input, int numToSumTo)
        {
            var currPointer = input.IndexOf(numToSumTo);
            var endPointer = currPointer;
            var sum = 0;
            while (sum!=numToSumTo)
            {
                if (sum < numToSumTo)
                {
                    sum += input[--currPointer];
                }
                else
                {
                    sum -= input[--endPointer];
                }
            }
            return FindRange(input.Skip(currPointer - 1).Take(endPointer - currPointer));
        }

        private static int GetPartOne(List<int> input)
        {
            var pointer = 0;
            var preamble = 25;
            while (true)
            {
                var numbersToConsider = input.Skip(pointer).Take(preamble);
                var finalNum = input.Skip(pointer + preamble).FirstOrDefault();
                if (!GetPairs(numbersToConsider.ToList())
                    .Select(x => SumTuple(x)).Any(x => x == finalNum))
                { 
                    return finalNum;
                }
                else
                {
                    pointer++;
                }
            }
        }

        private static int FindRange(IEnumerable<int> enumerable)
        {
            return enumerable.Max() + enumerable.Min();
        }

        static private int SumTuple(Tuple<int, int> tuple)
        {
            return tuple.Item1 + tuple.Item2;
        }

        static private List<Tuple<int, int>> GetPairs (List<int> input)
        {
            return input.Select(i => input.Select(i1 => new Tuple<int, int>(i, i1)).ToList())
                .ToList()
                .SelectMany(list => list.Select(x => x)).ToList();
        }


    }
}
