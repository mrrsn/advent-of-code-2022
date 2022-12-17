using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day11
{
   public delegate long CalcDelegate( long x, long y );
   public class Item
   {
      public int monkey;
      public long value;
   }

   public class Monkey
   {
      public int monkeyNum;
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
         var items = new List<Item>();
         var monkeys = FillMonkeys( lines, chunks, out items );
         var common = 1;
         monkeys.ForEach( m => common *= m.test );

         var rounds = 10000;
         while ( rounds --> 0 )
         {
            monkeys.ForEach( m =>
            {
               items.ForEach( i =>
               {
                  if ( i.monkey != m.monkeyNum ) return;

                  i.value = m.calc.Invoke( i.value, m.right.Equals( "old" ) ? i.value : int.Parse( m.right ) );
                  //i.value /= 3;
                  i.monkey = i.value % m.test == 0 ? m.t : m.f;
                  i.value %= common;
                  m.itemsHandled++;
               } );
            } );
         }

         Console.WriteLine();

         var counts = monkeys.Select( h => h.itemsHandled ).ToList();
         counts.Sort();
         Console.WriteLine( counts[^2] * counts.Last() );
      }

      private static List<Monkey> FillMonkeys( List<string> lines, List<string> chunks, out List<Item> items )
      {
         items = new List<Item>();
         var tempItems = new List<Item>();

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
            var temp = new Monkey();
            temp.monkeyNum = int.Parse( m[0] );
            m[1].Split( ", " ).ToList().Select( n => int.Parse( n ) ).ToList().ForEach( i =>
            {
               var item = new Item
               {
                  monkey = temp.monkeyNum,
                  value = i
               };
               tempItems.Add( item );
            } );
            var op = m[2].Split( ' ' );
            temp.calc = op[1].Equals( "+" ) ? new CalcDelegate( Add ) : new CalcDelegate( Mult );
            temp.right = op[2];
            temp.test = int.Parse( m[3] );
            temp.t = int.Parse( m[4] );
            temp.f = int.Parse( m[5] );
            monkeys.Add( temp );
         } );

         items.AddRange( tempItems );

         return monkeys;
      }
   }
}
