using System;
using System.IO;
using System.Linq;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxSeatId = Int32.MinValue;
            var minSeatId = Int32.MaxValue;
            foreach (var inputString in File.ReadLines("PuzzleInput.txt"))
            {
                var seatId = 8 * BinarySearch(inputString.Substring(0, 7), 0, 127, 'F');
                seatId += BinarySearch(inputString[7..], 0, 7, 'L');
                maxSeatId = Math.Max(maxSeatId, seatId);
                minSeatId = Math.Min(minSeatId, seatId);
            }
            Console.WriteLine(maxSeatId);
            var numberList = Enumerable.Range(minSeatId, maxSeatId-minSeatId + 1).ToList();
            foreach (var inputString in File.ReadLines("PuzzleInput.txt"))
            {
                // Not very efficient to do this again, but easier!
                var seatId = 8 * BinarySearch(inputString.Substring(0, 7), 0, 127, 'F');
                seatId += BinarySearch(inputString[7..], 0, 7, 'L');
                numberList.Remove(seatId);
            }
            Console.WriteLine(string.Join(", ", numberList));
        }

        private static int BinarySearch(string input, int minNum, int maxNum, char charToSearch)
        {
            var result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                var mid = (minNum + maxNum + 1) / 2;
                var lower = (input[i] == charToSearch);
                if (i == (input.Length - 1))
                {
                    result = lower ? minNum : maxNum;
                }
                else if (lower)
                {
                    maxNum = mid - 1;
                }
                else
                {
                    minNum = mid;
                }
            }
            return result;
        }

    }
}
