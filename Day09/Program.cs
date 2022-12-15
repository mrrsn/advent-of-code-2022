using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day09
{
   public class Day09
   {
      private const int len = 10;
      private static List<Point> log = new List<Point>();
      private static Point[] rope = Enumerable.Repeat( new Point( 0, 0 ), len ).ToArray();

      public static void Main09()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day09\input").ToList();

         log.Add( rope[len - 1] );

         lines.ForEach( line =>
         {
            var move = line.Split( ' ' );
            int i = int.Parse( move[1] );
            while ( i --> 0 )
            {
               MoveHead( move[0][0], ref rope[0] );

               for ( int n = 1; n < len; n++ )
               {
                  if ( rope[n - 1] != rope[n] )
                  {
                     MoveTail( rope[n - 1], ref rope[n] );
                     log.Add( rope[len-1] );
                  }
               }
            }
         } );

         log = log.Distinct().ToList();
         Console.WriteLine( log.Count() );
      }

      private static void MoveHead( char dir, ref Point h )
      {
         switch ( dir )
         {
            case 'R':
               h.X++;
               break;
            case 'L':
               h.X--;
               break;
            case 'U':
               h.Y++;
               break;
            case 'D':
               h.Y--;
               break;
         }
      }

      private static void MoveTail( Point h, ref Point t )
      {
         if ( IsHead2StepsAwayInLine( h, t ) )
         {
            if ( h.X > t.X )
               t.X++;
            else if ( h.X < t.X )
               t.X--;
            else if ( h.Y > t.Y )
               t.Y++;
            else if ( h.Y < t.Y )
               t.Y--;
         }
         else if ( AreHeadAndTailNotTouchingAndNotInLine( h, t ) )
         {
            if ( h.X > t.X )
               t.X++;
            else if ( h.X < t.X )
               t.X--;

            if ( h.Y > t.Y )
               t.Y++;
            else if ( h.Y < t.Y )
               t.Y--;
         }
      }

      private static bool AreTouching( Point h, Point t ) => Math.Abs( h.X - t.X ) <= 1 && Math.Abs( h.Y - t.Y ) <= 1;
      private static bool IsHead2StepsAwayInLine( Point h, Point t ) => ( h.X == t.X && Math.Abs( h.Y - t.Y ) == 2 ) || ( h.Y == t.Y && Math.Abs( h.X - t.X ) == 2 );
      private static bool AreHeadAndTailNotTouchingAndNotInLine( Point h, Point t ) => !AreTouching( h, t ) && !( h.X == t.X || h.Y == t.Y );
   }
}
