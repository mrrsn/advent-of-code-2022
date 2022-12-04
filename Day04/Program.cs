using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day04
{
   public class Day04
   {
      public static void Main04()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\Downloads\advent-of-code-2022-main\Day04\input");
         var fullyContained = 0;
         var anyOverlap = 0;
         foreach ( var line in lines )
         {
            char[] delimeters = {',','-' };
            var i = line.Split( delimeters ).Select( x => int.Parse( x ) ).ToArray();

            if ( FullyContains( i[0], i[1], i[2], i[3] ) )
               fullyContained++;
            if ( AnyOverlap( i[0], i[1], i[2], i[3] ) )
               anyOverlap++;
         }

         Console.WriteLine( fullyContained );
         Console.WriteLine( anyOverlap );
      }

      public static bool AnyOverlap( int a, int b, int x, int y ) => a < x ? b >= x : y >= a;

      public static bool FullyContains( int a, int b, int x, int y ) => ( a <= x && b >= y ) || ( x <= a && y >= b );
   }
}
