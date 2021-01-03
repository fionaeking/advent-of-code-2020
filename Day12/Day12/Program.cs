using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("PuzzleInput.txt").Select(x => x.ToCharArray());
            Console.WriteLine(CalculateManhattanDistance(input));
        }

        private static int CalculateManhattanDistance(IEnumerable<char[]> input)
        {
            var currPointX = 0;
            var currPointY = 0;
            var currWaypointX = 10;
            var currWaypointY = 1;
            foreach (var instruction in input)
            {
                var nextWaypointX = 0;
                var nextWaypointY = 0;
                var iterations = 0;
                var direction = instruction[0];
                var magnitude = int.Parse(string.Join("", instruction.Skip(1)));
                switch (direction)
                {
                    case 'N':
                        currWaypointY += magnitude;
                        break;
                    case 'S':
                        currWaypointY -= magnitude;
                        break;
                    case 'E':
                        currWaypointX += magnitude;
                        break;
                    case 'W':
                        currWaypointX -= magnitude;
                        break;
                    case 'L':
                        iterations = magnitude / 90;
                        while (iterations > 0)
                        {
                            nextWaypointX = currPointX + currPointY - currWaypointY;
                            currWaypointY = currWaypointX + currPointY - currPointX;
                            currWaypointX = nextWaypointX;
                            iterations--;
                        }
                        break;
                    case 'R':
                        iterations = magnitude / 90;
                        while (iterations > 0)
                        {
                            nextWaypointX = currWaypointY - currPointY + currPointX;
                            nextWaypointY = -currWaypointX + currPointX + currPointY;
                            currWaypointX = nextWaypointX;
                            currWaypointY = nextWaypointY;
                            iterations--;
                        }
                        break;
                    case 'F':
                        nextWaypointX = currWaypointX + magnitude * (currWaypointX - currPointX);
                        nextWaypointY = currWaypointY + magnitude * (currWaypointY - currPointY);
                        currPointX += magnitude * (currWaypointX - currPointX);
                        currPointY += magnitude * (currWaypointY - currPointY);
                        currWaypointX = nextWaypointX;
                        currWaypointY = nextWaypointY;
                        break;
                    default:
                        Console.WriteLine("Unknown direction");
                        break;
                }
            }
            return Math.Abs(currPointX) + Math.Abs(currPointY);
        }
    }
}
