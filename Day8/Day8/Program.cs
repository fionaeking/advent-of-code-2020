using System;
using System.IO;
using System.Collections.Generic;

namespace Day8
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputLength = File.ReadAllLines("PuzzleInput.txt").Length;
            for (int i=0; i< inputLength; i++)
            {
                var input = File.ReadAllLines("PuzzleInput.txt");
                input[i] = input[i].Replace("nop", "jmp");
                input[i] = input[i].Replace("jmp", "nop");
                if (DoesProgramTerminate(input))
                {
                    Console.WriteLine("End of program! Instruction pointer " + i);
                    break;
                }
            }
            
        }

        static bool DoesProgramTerminate(string[] input)
        {
            var instructionPointer = 0;
            var instructionPointersVisited = new List<int>() { };
            var accumulator = 0;
            var reachEnd = false;
            while (!reachEnd)
            {
                var line = input[instructionPointer];
                var instruction = line.Split(" ")[0];
                var amount = int.Parse(line.Split(" ")[1]);
                switch (instruction)
                {
                    case "acc":
                        instructionPointer += 1;
                        accumulator += amount;
                        break;
                    case "jmp":
                        instructionPointer += amount;
                        break;
                    default:  //case "nop":
                        instructionPointer += 1;
                        break;
                }
                if (instructionPointersVisited.Contains(instructionPointer))
                {
                    // Console.WriteLine(accumulator);
                    break;
                }
                else if (instructionPointer==input.Length)
                {
                    reachEnd = true;
                    Console.WriteLine("Reached end of program");
                    Console.WriteLine(accumulator);
                    break;
                }
                else
                {
                    instructionPointersVisited.Add(instructionPointer);
                }
            }
            return reachEnd;
        }
    }
}
