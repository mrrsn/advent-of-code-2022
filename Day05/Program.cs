using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day05
{
   public class Day05
   {
      public static void Main05()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day05\input");

         var watch = System.Diagnostics.Stopwatch.StartNew();

         var towerRows = new List<string>();
         var moves = new List<string>();
         bool foundBreak = false;
         foreach ( var line in lines )
         {
            if ( string.IsNullOrEmpty( line ) )
            {
               foundBreak = true;
               continue;
            }

            if ( !foundBreak )
               towerRows.Add( line );
            else
               moves.Add( line );
         }

         var towerCount = int.Parse( towerRows.Last().Replace("   "," ").Trim().Split(' ').ToList().Last() );
         var stacks1 = new List<Stack<char>>();
         var stacks2 = new List<Stack<char>>();
         FillStacks( towerRows, towerCount, stacks1 );
         FillStacks( towerRows, towerCount, stacks2 );

         foreach ( var move in moves )
         {
            var steps = move.Split( ' ' );
            var numCrates = int.Parse( steps[1] );
            var fromStack = int.Parse( steps[3] ) - 1; // convert puzzle text to index by -1
            var toStack = int.Parse( steps[5] ) - 1; // same
            var temp = new Stack<char>();
            for ( int i = 0; i < numCrates; i++ )
            {
               stacks1[toStack].Push( stacks1[fromStack].Pop() );

               temp.Push( stacks2[fromStack].Pop() );
            }
            for ( int i = 0; i < numCrates; i++ )
            {
               stacks2[toStack].Push( temp.Pop() );
            }
         }

         watch.Stop();
         Console.WriteLine( watch.Elapsed );

         stacks1.ForEach( x => Console.Write( x.Pop() ) ); // Part 1: ZBDRNPMVH
         Console.WriteLine();
         stacks2.ForEach( x => Console.Write( x.Pop() ) ); // Part 2: WDLPFNNNB
      }

      private static void FillStacks( List<string> towerRows, int towerCount, List<Stack<char>> stacks )
      {
         for ( int i = 0; i < towerCount; ++i )
         { stacks.Add( new Stack<char>() ); }

         for ( int i = towerRows.Count - 2; i >= 0; i-- )
         {
            for ( int j = 0; j < towerCount; j++ )
            {
               var index = j == 0 ? 1 : 1 + j * 4;
               var crate = towerRows[i][index];
               if ( !crate.Equals( ' ' ) )
               {
                  stacks[j].Push( crate );
               }
            }
         }
      }
   }
}
