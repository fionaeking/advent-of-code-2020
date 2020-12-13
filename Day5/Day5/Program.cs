using System;
using System.IO;

namespace Day5
{
    class Program
    {
        static void Main(string[] args)
        {
            var maxSeatId = 0;
            foreach (var inputString in File.ReadLines("PuzzleInput.txt"))
            {
                var seatId = 8 * BinarySearch(inputString.Substring(0, 7), 0, 127, 'F');
                seatId += BinarySearch(inputString[7..], 0, 7, 'L');
                maxSeatId = Math.Max(maxSeatId, seatId);
            }
            Console.WriteLine(maxSeatId);
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
