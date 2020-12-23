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

        static int PartTwo(char[][] input, int i, int j, char c)
        {
            var count = 0;
            var maxY = input.Length - 1;
            var maxX = input[0].Length - 1;
            var jCopy = j;
            var iCopy = i;

            // check if j == maxX
            if (j < maxX)
            {
                var check = false;
                jCopy = j;
                while (!check)
                {
                    check = CheckChar(input[i][++jCopy]);
                    if (jCopy == maxX)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[i][jCopy], '#');
            }

            // check if j == 0
            if (j > 0)
            {
                var check = false;
                jCopy = j;
                while (!check)
                {
                    check = CheckChar(input[i][--jCopy]);
                    if(jCopy==0)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[i][jCopy], '#');
            }

            // check if i == 0
            if (i > 0)
            {
                var check = false;
                iCopy = i;
                while (!check)
                {
                    check = CheckChar(input[--iCopy][j]);
                    if (iCopy == 0)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[iCopy][j], '#');
            }

            // check if i == 0
            if (i < maxY)
            {
                var check = false;
                iCopy = i;
                while (!check)
                {
                    check = CheckChar(input[++iCopy][j]);
                    if (iCopy == maxY)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[iCopy][j], '#');
            }

            if (i < maxY & j < maxX)
            {
                var check = false;
                iCopy = i;
                jCopy = j;
                while (!check)
                {
                    check = CheckChar(input[++iCopy][++jCopy]);
                    if (iCopy == maxY | jCopy == maxX)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[iCopy][jCopy], '#');
            }

            if (i < maxY & j > 0)
            {
                var check = false;
                iCopy = i;
                jCopy = j;
                while (!check)
                {
                    check = CheckChar(input[++iCopy][--jCopy]);
                    if (iCopy == maxY | jCopy == 0)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[iCopy][jCopy], '#');
            }

            if (i > 0 & j > 0)
            {
                var check = false;
                iCopy = i;
                jCopy = j;
                while (!check)
                {
                    check = CheckChar(input[--iCopy][--jCopy]);
                    if (iCopy == 0 | jCopy == 0)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[iCopy][jCopy], '#');
            }

            if (i > 0 & j < maxX)
            {
                var check = false;
                iCopy = i;
                jCopy = j;
                while (!check)
                {
                    check = CheckChar(input[--iCopy][++jCopy]);
                    if (iCopy == 0 | jCopy == maxX)
                    {
                        break;
                    }
                }
                count += EqualsChar(input[iCopy][jCopy], '#');
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
