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
            var currDir = 90;  // facing 90 degrees (East)
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
                if (direction == 'F')
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
                }
                switch(direction)
                {
                    case 'N':
                        currPointY += magnitude;
                        break;
                    case 'S':
                        currPointY -= magnitude;
                        break;
                    case 'E':
                        currPointX += magnitude;
                        break;
                    case 'W':
                        currPointX -= magnitude;
                        break;
                    case 'L':
                        currDir -= magnitude;
                        currDir += 360;
                        currDir = currDir % 360;
                        break;
                    case 'R':
                        currDir += magnitude;
                        currDir = currDir % 360;
                        break;
                    default:
                        Console.WriteLine("Unknown direction");
                        break;
                }
            }
            Console.WriteLine(Math.Abs(currPointX) + Math.Abs(currPointY));
        }
    }
}
