using System;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        static void Main()
        {
            var input = File.ReadAllLines("PuzzleInput.txt")
                .ToList()
                .Select(x => x.ToCharArray())
                .ToArray();
            var secondInput = input.Select(a => (char[])a.Clone()).ToArray();

            while (true)
            {
                var changed = false;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (input[i][j] == 'L' && CheckIsOccupied(input, i, j, '#') == 0)
                        {
                            secondInput[i][j] = '#';
                            changed = true;
                        }
                        else if (input[i][j] == '#' && CheckIsOccupied(input, i, j, '#') >= 4)
                        {
                            secondInput[i][j] = 'L';
                            changed = true;
                        }
                        else
                        {
                            secondInput[i][j] = input[i][j];
                        }
                    }
                }

                if (changed == true)
                {
                    input = secondInput.Select(a => (char[])a.Clone()).ToArray();
                }
                else
                {
                    var count = secondInput.SelectMany(x => x).ToArray().Where(x=> x=='#').Count();
                    Console.WriteLine(count);
                    break;
                }
            }

               
        }

        static int CheckIsOccupied(char[][] input, int i, int j, char ch)
        {
            var count = 0;
            var maxY = input.Length - 1;
            var maxX = input[0].Length - 1;

            if (i < maxY)
            {
                count += EqualsChar(input[i + 1][j], ch);
            }
            if (j < maxX)
            {
                count += EqualsChar(input[i][j + 1], ch);
            }
            if (i > 0)
            {
                count += EqualsChar(input[i - 1][j], ch);
            }
            if (j > 0)
            {
                count += EqualsChar(input[i][j - 1], ch);
            }
            if (i < maxY & j < maxX)
            {
                count += EqualsChar(input[i + 1][j + 1], ch);
            }
            if (i < maxY & j > 0)
            {
                count += EqualsChar(input[i + 1][j - 1], ch);
            }
            if (i > 0 & j < maxX)
            {
                count += EqualsChar(input[i - 1][j + 1], ch);
            }
            if (i > 0 & j > 0)
            {
                count += EqualsChar(input[i - 1][j - 1], ch);
            }
            return count;
        }

        static int EqualsChar(char toCompare, char ch)
        {
            return toCompare == ch ? 1 : 0;
        }

    }
}
