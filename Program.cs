using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace advent_of_code_2022
{
   internal class Program
   {
      private static void Main()
      {
         var lines = File.ReadAllLines(@"C:\Users\r.morrison\source\repos\advent-of-code-2022\Day01\input");

         var sum = 0;
         var sums = new List<int>();
         foreach (var line in lines)
         {
            if (!string.IsNullOrWhiteSpace(line))
            {
               sum += int.Parse(line);
            }
            else
            {
               sums.Add(sum);
               sum = 0;
            }
         }

         sums.Sort();
         sums.Reverse();

         Console.WriteLine(sums[2]+sums[1]+sums[0]);
      }
   }
}
