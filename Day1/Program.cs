using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using CommandLine;

namespace Day1
{
    class Options
    {
        [Option("total", Required = true, HelpText = "Number to sum to.")]
        public int Total { get; set; }

        [Option("numEntries", Required = true, HelpText = "Number of elements to sum (min 2).")]
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

        private static void Application(Options opts)
        {
            var expenseReport = new ExpenseReport();
            var result = expenseReport.Run(GetInputAsListNums(), opts.Total, opts.NumEntries);
            if(result > 0) 
            {
                Console.WriteLine($"The answer is {result}");
            }
            else 
            {
                Console.WriteLine($"Couldn't find {opts.NumEntries} numbers which add up to {opts.Total}");   
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
            input.OrderBy(i=>i).ToList().RemoveAll(x => x>totalSum);
            return CalculateResult(input, totalSum, numEntries);
        }
        private int CalculateResult(List<int> input, int totalSum, int numEntries)
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
