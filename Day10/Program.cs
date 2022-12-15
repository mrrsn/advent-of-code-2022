using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day10
{
   public class Day10
   {
      public static void Main10()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day10\input").ToList();
         var map = new Dictionary<int,int>(240);
         var key = 1;
         var X = 1;
         map.Add( key, X );

         foreach ( var line in lines )
         {
            if ( string.IsNullOrEmpty( line ) )
               break;

            if ( line.Equals( "noop" ) )
            {
               map.Add( ++key, X );
            }
            else
            {
               map.Add( ++key, X );
               X += int.Parse( line.Split( ' ' )[1] );
               map.Add( ++key, X );
            }
         }

         Console.WriteLine( "Part 1" );
         Console.WriteLine( 20 * map[20] + 60 * map[60] + 100 * map[100] + 140 * map[140] + 180 * map[180] + 220 * map[220] );

         var screen = Enumerable.Repeat( '.', 240 ).ToArray();
         for ( int r = 0; r < 6; r++ )
         {
            for ( int c = 0; c < 40; c++ )
            {
               var z = c + r * 40;
               if ( Math.Abs( c - map[z + 1] ) <= 1 )
               {
                  screen[z] = '#';
               }
            }
         }

         Console.WriteLine( "Part 2" );
         Print( screen );
      }

      private static void Print( char[] screen )
      {
         for ( int r = 0; r < 6; r++ )
         {
            for ( int c = 0; c < 40; c++ )
            {
               Console.Write( screen[c + r * 40] );
            }
            Console.WriteLine();
         }
      }
   }
}
