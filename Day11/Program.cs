using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day11
{
   public delegate long CalcDelegate( long x, long y );

   public class Monkey
   {
      public List<long> items;
      public CalcDelegate calc;
      public string right;
      public int test, t, f;
      public long itemsHandled;
   }

   public class Day11
   {
      public static long Add( long x, long y ) => x + y;
      public static long Mult( long x, long y ) => x * y;

      public static void Main11()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day11\input").ToList();
         var chunks = new List<string>();
         var monkeys = FillMonkeys( lines, chunks );

         var rounds = 20;
         while ( rounds --> 0 )
         {
            monkeys.ForEach( m =>
            {
               m.items.ForEach( i =>
               {
                  i = m.calc.Invoke( i, m.right.Equals( "old" ) ? i : int.Parse( m.right ) );
                  i /= 3;
                  if ( i % m.test == 0 )
                  {
                     monkeys[m.t].items.Add( i );
                  }
                  else
                  {
                     monkeys[m.f].items.Add( i );
                  }

                  m.itemsHandled++;
               } );

               m.items.Clear();
            } );
         }

         var counts = monkeys.Select( h => h.itemsHandled ).ToList();
         counts.Sort();

         Console.WriteLine( "Part 1: " + counts[^2] * counts.Last() );
      }

      private static List<Monkey> FillMonkeys( List<string> lines, List<string> chunks )
      {
         for ( int i = 0; i < lines.Count - 6; )
         {
            chunks.Add( lines[i++].Trim().Split( ' ' )[1][0] + ";" +
                        lines[i++].Trim().Split( ": " )[1] + ";" +
                        lines[i++].Trim().Split( " = " )[1] + ";" +
                        lines[i++].Trim().Split( ' ' ).Last() + ";" +
                        lines[i++].Trim().Split( ' ' ).Last() + ";" +
                        lines[i++].Trim().Split( ' ' ).Last() );
            i++;
         }

         var monkeys = new List<Monkey>();
         chunks.ForEach( c =>
         {
            var m = c.Split( ';' );
            var itemsStr = m[1];
            var itemsList = itemsStr.Split( ", " ).ToList();
            var itemsLongList = itemsList.Select( n => long.Parse( n ) ).ToList();
            var temp = new Monkey();
            temp.items = new List<long>();
            temp.items.AddRange( itemsLongList );
            var op = m[2].Split( ' ' );
            temp.calc = op[1].Equals( "+" ) ? new CalcDelegate( Add ) : new CalcDelegate( Mult );
            temp.right = op[2];
            temp.test = int.Parse( m[3] );
            temp.t = int.Parse( m[4] );
            temp.f = int.Parse( m[5] );
            monkeys.Add( temp );
         } );

         return monkeys;
      }
   }
}
