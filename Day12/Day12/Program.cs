using System;
using System.IO;
using System.Linq;

namespace Day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("PuzzleInput.txt").Select(x => x.ToCharArray());
            var currPointX = 0;
            var currPointY = 0;
            // var currDir = 90;  // facing 90 degrees (East)
            var currWaypointX = 10;
            var currWaypointY = 1;
            var nextWaypointX = 0;
            var nextWaypointY = 0;
            var iterations = 0;
            foreach (var instruction in input)
            {
                var direction = instruction[0];
                var magnitude = int.Parse(string.Join("",instruction.Skip(1)));

                /*
                Action N means to move north by the given value.
                Action S means to move south by the given value.
                Action E means to move east by the given value.
                Action W means to move west by the given value.
                Action L means to turn left the given number of degrees.
                Action R means to turn right the given number of degrees.
                Action F means to move forward by the given value in the direction the ship is currently facing.
                */
                /*if (direction == 'F')
                {
                    if (currDir == 0)
                    {
                        direction = 'N';
                    }
                    else if (currDir == 90)
                    {
                        direction = 'E';
                    }
                    else if (currDir == 180)
                    {
                        direction = 'S';
                    }
                    else if (currDir == 270)
                    {
                        direction = 'W';
                    }
                    else
                    {
                        Console.WriteLine("Unknown number of degrees");
                    }
                }*/
                switch(direction)
                {
                    case 'N':
                        // way point is x y
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
                        //currDir -= magnitude;
                        //currDir += 360;
                        //currDir = currDir % 360;
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
                        //currDir += magnitude;
                        //currDir = currDir % 360;
                        break;
                    case 'F':
                        //nextWaypointX = currWaypointX;
                        //nextWaypointY = currWaypointY;
                        var amountX = magnitude * (currWaypointX - currPointX);
                        var amountY = magnitude * (currWaypointY - currPointY);
                        currPointX += amountX;
                        currPointY += amountY;
                        //11670
                        //10680
                        //currWaypointX = currPointX + currWaypointX;
                        //currWaypointY = currPointY + currWaypointY;
                        currWaypointX += amountX;
                        currWaypointY += amountY;
                        break;
                    default:
                        Console.WriteLine("Unknown direction");
                        break;
                }
                Console.WriteLine($"{currPointX} + {currPointY}");
                Console.WriteLine($"{currWaypointX} + {currWaypointY}");
            }
            Console.WriteLine(Math.Abs(currPointX) + Math.Abs(currPointY));
        }
    }
}
