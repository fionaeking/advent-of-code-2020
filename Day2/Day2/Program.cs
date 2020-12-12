using System;
using System.Linq;
using System.IO;

namespace Day2
{
    class Program  // 462
    {
        static void Main(string[] args)
        {
            var validPasswordCount = 0;
            foreach (var line in File.ReadLines("PuzzleInput.txt"))
            {
                var substrings = line.Split(' ');
                var minNum = Int32.Parse(substrings[0].Split('-')[0]);
                var maxNum = Int32.Parse(substrings[0].Split('-')[1]);
                var letter = substrings[1][0];
                var password = substrings[2];
                var letterCount = password.Count(x => x == letter);
                if (minNum <= letterCount && letterCount <= maxNum)
                {
                    validPasswordCount++;
                }
            }
            Console.WriteLine(validPasswordCount);
        }
    }
}
