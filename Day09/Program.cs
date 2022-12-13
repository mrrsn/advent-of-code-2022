using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day09
{
   public class Day09
   {
      private static List<string> log = new List<string>();
      private static int Hx = 0; private static int Hy = 0; private static int Tx = 0; private static int Ty = 0;

      public static void Main09()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day09\input").ToList();

         Log();

         lines.ForEach( line =>
         {
            var move = line.Split( ' ' );
            int i = int.Parse( move[1] );
            while ( i --> 0 )
            {
               MoveHead( move[0][0] );

               while ( !AreBothWithin1() )
                  MoveTail( move[0][0] );
            }
         } );

         log = log.Distinct().ToList();
         Console.WriteLine( log.Count() );
      }

      private static void MoveHead( char dir )
      {
         if ( dir == 'R' || dir == 'L' )
            Hx += dir == 'L' ? -1 : 1;
         else
            Hy += dir == 'D' ? -1 : 1;
      }

      private static void MoveTail( char dir )
      {
         if ( dir == 'R' || dir == 'L' )
         {
            Ty = Hy;
            Tx += ( dir == 'L' ) ? -1 : 1;
         }
         else
         {
            Tx = Hx;
            Ty += ( dir == 'D' ) ? -1 : 1;
         }

         Log();
      }

      private static void Log() => log.Add( Tx.ToString() + "," + Ty.ToString() );
      private static bool AreBothWithin1() => Math.Abs( Hx - Tx ) <= 1 && Math.Abs( Hy - Ty ) <= 1;
   }
}
