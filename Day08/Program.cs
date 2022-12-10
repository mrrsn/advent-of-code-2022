using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day08
{
   public class Day08
   {
      public struct Tree
      {
         public int height;
         public bool visible_N;
         public bool visible_W;
         public bool visible_S;
         public bool visible_E;
      }

      public static void Main08()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day08\test").ToList();
         var forestWidth = lines[0].Length;
         var forestHeight = lines.Count;

         var forest = new Tree[ forestWidth, forestHeight ];
         for ( var h = 0; h < forestHeight; h++ )
         {
            for ( var w = 0; w < forestWidth; w++ )
            {
               forest[w, h].height = int.Parse( lines[h][w].ToString() );
               forest[w, h].visible_N = forest[w, h].visible_W = forest[w, h].visible_S = forest[w, h].visible_E = false;

               if ( w == 0 )
                  forest[w, h].visible_W = true;
               else if ( w == forestWidth - 1 )
                  forest[w, h].visible_E = true;
               else if ( h == 0 )
                  forest[w, h].visible_N = true;
               else if ( h == forestHeight - 1 )
                  forest[w, h].visible_S = true;
            }
         }

         for ( var h = 1; h < forestHeight - 1; h++ )
         {
            int tallest_W = forest[0, h].height;
            for ( var w = 1; w < forestWidth - 1; w++ )
            {
               if ( forest[w, h].height > tallest_W )
               {
                  tallest_W = forest[w, h].height;
                  forest[w, h].visible_W = true;
               }
            }

            int tallest_E = forest[forestWidth - 1, h].height;
            for ( var w = forestWidth - 1; w > 0; w-- )
            {
               if ( forest[w, h].height > tallest_E )
               {
                  tallest_E = forest[w, h].height;
                  forest[w, h].visible_E = true;
               }
            }
         }

         for ( var w = 1; w < forestWidth - 1; w++ )
         {
            int tallest_N = forest[w, 0].height;
            for ( var h = 1; h < forestHeight - 1; h++ )
            {
               if ( forest[w, h].height > tallest_N )
               {
                  tallest_N = forest[w, h].height;
                  forest[w, h].visible_N= true;
               }
            }

            int tallest_S = forest[w, forestHeight - 1].height;
            for ( var h = forestHeight - 1; h > 0; h-- )
            {
               if ( forest[w, h].height > tallest_S )
               {
                  tallest_S = forest[w, h].height;
                  forest[w, h].visible_S = true;
               }
            }
         }

         PrintResult( forestWidth, forestHeight, forest );
      }

      private static void PrintResult( int forestWidth, int forestHeight, Tree[,] forest )
      {
         var visible = 0;
         for ( var h = 0; h < forestHeight; h++ )
         {
            for ( var w = 0; w < forestWidth; w++ )
            {
               bool visibleAny = forest[w, h].visible_N || forest[w, h].visible_W || forest[w, h].visible_S || forest[w, h].visible_E;
               visible += visibleAny ? 1 : 0;
            }
         }

         Console.WriteLine( "visible: " + visible );
      }
   }
}
