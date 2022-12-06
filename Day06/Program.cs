using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day06
{
   public class Day06
   {
      public static void Main06()
      {
         var line = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day06\input").FirstOrDefault();

         var watch = System.Diagnostics.Stopwatch.StartNew();

         int i = 0;
         while ( i++ < line.Length - 4 )
         {
            if ( line.Substring( i, 4 ).Distinct().Count() == 4 )
            {
               Console.WriteLine( i + 4 );
               break;
            }
         }

         int j = 0;
         while ( j++ < line.Length - 14 )
         {
            if ( line.Substring( j, 14 ).Distinct().Count() == 14 )
            {
               Console.WriteLine( j + 14 );
               break;
            }
         }

         watch.Stop();
         Console.WriteLine( watch.Elapsed );
      }
   }
}
