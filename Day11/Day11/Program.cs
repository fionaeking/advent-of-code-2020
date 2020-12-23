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
            var secondInput = input.Select(a => (char[])a.Clone()).ToArray();

            while (true)
            {
                var changed = false;
                for (int i = 0; i < input.Length; i++)
                {
                    for (int j = 0; j < input[0].Length; j++)
                    {
                        if (input[i][j] == 'L' && PartTwo(input, i, j, '#') == 0)
                        {
                            secondInput[i][j] = '#';
                            changed = true;
                        }
                        else if (input[i][j] == '#' && PartTwo(input, i, j, '#') >= 5)
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

        static int JRepeatableFunction(char[][] input, int i, int j, int valToStop, bool increment)
        {
            var check = false;
            var jCopy = j;
            var valToAdd = increment ? 1 : -1;
            while (!check)
            {
                jCopy += valToAdd;
                check = CheckChar(input[i][jCopy]);
                if (jCopy == valToStop)
                {
                    break;
                }
            }
            return EqualsChar(input[i][jCopy], '#');
        }

        static int IRepeatableFunction(char[][] input, int i, int j, int valToStop, bool increment)
        {
            var check = false;
            var iCopy = i;
            var valToAdd = increment ? 1 : -1;
            while (!check)
            {
                iCopy += valToAdd;
                check = CheckChar(input[iCopy][j]);
                if (iCopy == valToStop)
                {
                    break;
                }
            }
            return EqualsChar(input[iCopy][j], '#');
        }

        static int IJRepeatableFunction(char[][] input, int i, int j, int valToStopI, int valToStopJ, bool incrementI, bool incrementJ)
        {
            var check = false;
            var iCopy = i;
            var jCopy = j;
            var valToAddI = incrementI ? 1 : -1;
            var valToAddJ = incrementJ ? 1 : -1;
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

        static int PartTwo(char[][] input, int i, int j, char ch)
        {
            var count = 0;
            var maxY = input.Length - 1;
            var maxX = input[0].Length - 1;

            if (j < maxX)
            {
                count += JRepeatableFunction(input, i, j, maxX, true);
            }

            if (j > 0)
            {
                count += JRepeatableFunction(input, i, j, 0, false);
            }

            if (i > 0)
            {
                count += IRepeatableFunction(input, i, j, 0, false);
            }

            if (i < maxY)
            {
                count += IRepeatableFunction(input, i, j, maxY, true);
            }

            if (i < maxY & j < maxX)
            {
                count += IJRepeatableFunction(input, i, j, maxY, maxX, true, true);
            }

            if (i < maxY & j > 0)
            {
                count += IJRepeatableFunction(input, i, j, maxY, 0, true, false);
            }

            if (i > 0 & j > 0)
            {
                count += IJRepeatableFunction(input, i, j, 0, 0, false, false);
            }

            if (i > 0 & j < maxX)
            {
                count += IJRepeatableFunction(input, i, j, 0, maxX, false, true);
            }
            return count;
        }

        static int CheckIsOccupied(char[][] input, int i, int j, char ch)
        {
            var count = 0;
            var maxY = input.Length - 1;
            var maxX = input[0].Length - 1;

            if (j < maxX)
            {
                count += EqualsChar(input[i][j + 1], ch);
            }
            if (j > 0)
            {
                count += EqualsChar(input[i][j - 1], ch);
            }
            if (i < maxY)
            {
                count += EqualsChar(input[i + 1][j], ch);
                if (j < maxX)
                {
                    count += EqualsChar(input[i + 1][j + 1], ch);
                }
                if (j > 0)
                {
                    count += EqualsChar(input[i + 1][j - 1], ch);
                }
            }
            if (i > 0)
            {
                count += EqualsChar(input[i - 1][j], ch);
                if (j < maxX)
                {
                    count += EqualsChar(input[i - 1][j + 1], ch);
                }
                if (j > 0)
                {
                    count += EqualsChar(input[i - 1][j - 1], ch);
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
