using System;
using System.IO;
using System.Linq;

namespace Day11
{
    class Program
    {
        // TODO - this code is horrible
        static void Main()
        {
            var input = File.ReadAllLines("PuzzleInput.txt")
                .ToList()
                .Select(x => x.ToCharArray())
                .ToArray();
            Console.WriteLine(GetCount(input)); 
        }

        static int GetCount(char[][] input)
        {
            var secondInput = input.Select(a => (char[])a.Clone()).ToArray();
            while (true)
            {
                var changed = false;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (input[i][j] == 'L' && PartTwo(input, i, j) == 0)
                        {
                            secondInput[i][j] = '#';
                            changed = true;
                        }
                        else if (input[i][j] == '#' && PartTwo(input, i, j) >= 5)
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
                    return secondInput.SelectMany(x => x).ToArray().Where(x => x == '#').Count();
                }
            }
        }

        static int RecursiveSearch(char[][] input, int i, int j, int? valToStopI, int? valToStopJ)
        {
            var check = false;
            var iCopy = i;
            var jCopy = j;
            var valToAddI = 0;
            var valToAddJ = 0;
            if (valToStopI.HasValue)
            {
                valToAddI = valToStopI == 0 ? -1 : 1;
            }
            if (valToStopJ.HasValue)
            {
                valToAddJ = valToStopJ == 0 ? -1 : 1;
            }
            if (iCopy == valToStopI | jCopy == valToStopJ)
            {
                return 0;
            }
            while (!check)
            {
                iCopy += valToAddI;
                jCopy += valToAddJ;
                check = CheckChar(input[iCopy][jCopy]);
                if (iCopy == valToStopI | jCopy == valToStopJ)
                {
                    break;
                }
            }
            return EqualsChar(input[iCopy][jCopy], '#');
        }

        static int PartTwo(char[][] input, int i, int j)
        {
            var count = 0;
            var maxY = input.Length - 1;
            var maxX = input[0].Length - 1;
            count += RecursiveSearch(input, i, j, null, maxX);
            count += RecursiveSearch(input, i, j, null, 0);
            count += RecursiveSearch(input, i, j, 0, null);
            count += RecursiveSearch(input, i, j, maxY, null);
            count += RecursiveSearch(input, i, j, maxY, maxX);
            count += RecursiveSearch(input, i, j, maxY, 0);
            count += RecursiveSearch(input, i, j, 0, 0);
            count += RecursiveSearch(input, i, j, 0, maxX);
            return count;
        }

        static int CheckIsOccupied(char[][] input, int i, int j)
        {
            var count = 0;
            var maxY = input.Length - 1;
            var maxX = input[0].Length - 1;

            if (j < maxX)
            {
                count += EqualsChar(input[i][j + 1], '#');
            }
            if (j > 0)
            {
                count += EqualsChar(input[i][j - 1], '#');
            }
            if (i < maxY)
            {
                count += EqualsChar(input[i + 1][j], '#');
                if (j < maxX)
                {
                    count += EqualsChar(input[i + 1][j + 1], '#');
                }
                if (j > 0)
                {
                    count += EqualsChar(input[i + 1][j - 1], '#');
                }
            }
            if (i > 0)
            {
                count += EqualsChar(input[i - 1][j], '#');
                if (j < maxX)
                {
                    count += EqualsChar(input[i - 1][j + 1], '#');
                }
                if (j > 0)
                {
                    count += EqualsChar(input[i - 1][j - 1], '#');
                }
            }
            return count;
        }

        static int EqualsChar(char toCompare, char ch)
        {
            return toCompare == ch ? 1 : 0;
        }

        static bool CheckChar(char ch)
        {
            return (ch == '#' | ch == 'L');
        }

    }
}
