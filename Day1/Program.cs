using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace Day1
{
    class Options
    {
        [Option("total", Required = true, HelpText = "Input file to read.")]
        public int Total { get; set; }

        [Option("numEntries", Required = true, HelpText = "The maximum number of bytes to process.")]
        public int NumEntries { get; set; }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       Application(o);
                   });
        }

        public static void Application(Options opts)
        {
            var desiredTotal = opts.Total;
            var numEntries = opts.NumEntries;
            var expenseReport = new ExpenseReport();
            var result = expenseReport.Run(GetInputAsListNums(), desiredTotal, numEntries);
            if(result > 0) 
            {
                Console.WriteLine($"The answer is {result}");
            }
            else 
            {
                Console.WriteLine($"Couldn't find {numEntries} numbers which add up to {desiredTotal}");   
            }  
        }

        private static List<int> GetInputAsListNums()
        {
            return File.ReadLines("PuzzleInput.txt").Select(int.Parse).ToList();
        }
    }

    class ExpenseReport
    {
        public int Run(List<int> input, int totalSum, int numEntries)
        {
            input.OrderBy(i=>i);
            input.RemoveAll(x => x>totalSum);
            return CalculateResult(input, totalSum, numEntries);
        }
        public int CalculateResult(List<int> input, int totalSum, int numEntries)
        {
            var i = input.Take(totalSum/numEntries);
            while (numEntries>2)
            {   
                foreach (var n in i)
                {
                    input.Remove(n);
                    var result = CalculateResult(input, totalSum-n, numEntries-1);
                    if (result!=0) 
                    {
                        return FindProduct(n, result);
                    }
                }
                return 0;
            }
            foreach (var n in i)
            {
                if (CheckForPair(input, n, totalSum)) 
                {
                    return FindProduct(n, totalSum-n); 
                }
            }
            return 0;
        }

        private bool CheckForPair(List<int> inputNums, int num, int numToSumTo)
        {
            return inputNums.Where(x => x!=num).Any(x => x == numToSumTo - num);
        }

        private int FindProduct(int numOne, int numTwo)
        {
            return numOne * numTwo;
        }

    }
}
