using System;
using System.Linq;
using System.IO;

namespace Day2
{
    class Program
    {
        static void Main()
        {
            var validPasswordCount = 0;
            foreach (var line in File.ReadLines("PuzzleInput.txt"))
            {
                var substrings = line.Split(' ');
                var firstNum = Int32.Parse(substrings[0].Split('-')[0]);
                var secondNum = Int32.Parse(substrings[0].Split('-')[1]);
                var letter = substrings[1][0];
                var password = substrings[2];
                // validPasswordCount += OldInterpretation(password, letter, firstNum, secondNum);
                validPasswordCount += NewInterpretation(password, letter, firstNum, secondNum);
            }
            Console.WriteLine(validPasswordCount);

        }

        private static int OldInterpretation(string password, char letter, int minNum, int maxNum)
        {
            var letterCount = password.Count(x => x == letter);
            return (minNum <= letterCount && letterCount <= maxNum) ? 1 : 0;
        }

        private static int NewInterpretation(string password, char letter, int firstNum, int secondNum)
        {
            return (password[firstNum - 1] == letter ^ password[secondNum - 1] == letter) ? 1 : 0;
        }
    }
}
