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
         public int view_N;
         public int view_W;
         public int view_S;
         public int view_E;
      }

      public static void Main08()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day08\input").ToList();
         var forestWidth = lines[0].Length;
         var forestHeight = lines.Count;

         var forest = new Tree[ forestWidth, forestHeight ];
         for ( var h = 0; h < forestHeight; h++ )
         {
            for ( var w = 0; w < forestWidth; w++ )
            {
               forest[w, h].height = int.Parse( lines[h][w].ToString() );
               forest[w, h].visible_N = forest[w, h].visible_W = forest[w, h].visible_S = forest[w, h].visible_E = false;
               forest[w, h].view_N = forest[w, h].view_W = forest[w, h].view_S = forest[w, h].view_E = 0;

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

               int scenic = 1;
               for ( int i = w - 1; i > 0; i-- )
               {
                  if ( forest[i, h].height >= forest[w, h].height )
                     break;

                  scenic++;
               }

               forest[w, h].view_W = scenic;
            }

            int tallest_E = forest[forestWidth - 1, h].height;
            for ( var w = forestWidth - 1; w > 0; w-- )
            {
               if ( forest[w, h].height > tallest_E )
               {
                  tallest_E = forest[w, h].height;
                  forest[w, h].visible_E = true;
               }

               int scenic = 1;
               for ( int i = w + 1; i < forestWidth - 1; i++ )
               {
                  if ( forest[i, h].height >= forest[w, h].height )
                     break;

                  scenic++;
               }

               forest[w, h].view_E = scenic;
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

               int scenic = 1;
               for ( int i = h - 1; i > 0; i-- )
               {
                  if ( forest[w, i].height >= forest[w, h].height )
                     break;

                  scenic++;
               }

               forest[w, h].view_N = scenic;
            }

            int tallest_S = forest[w, forestHeight - 1].height;
            for ( var h = forestHeight - 1; h > 0; h-- )
            {
               if ( forest[w, h].height > tallest_S )
               {
                  tallest_S = forest[w, h].height;
                  forest[w, h].visible_S = true;
               }

               int scenic = 1;
               for ( int i = h + 1; i < forestWidth - 1; i++ )
               {
                  if ( forest[w, i].height >= forest[w, h].height )
                     break;

                  scenic++;
               }

               forest[w, h].view_S = scenic;
            }
         }

         for ( var h = 0; h < forestHeight; h++ )
         {
            for ( var w = 0; w < forestWidth; w++ )
            {
               if ( w == 0 )
                  forest[w, h].view_W = 0;
               if ( w == forestWidth - 1 )
                  forest[w, h].view_E = 0;
               if ( h == 0 )
                  forest[w, h].view_N = 0;
               if ( h == forestHeight - 1 )
                  forest[w, h].view_S = 0;
            }
         }

         PrintResult( forestWidth, forestHeight, forest );
      }

      private static void PrintResult( int forestWidth, int forestHeight, Tree[,] forest )
      {
         var visible = 0;
         var scenicMax = 0;
         for ( var h = 0; h < forestHeight; h++ )
         {
            for ( var w = 0; w < forestWidth; w++ )
            {
               bool visibleAny = forest[w, h].visible_N || forest[w, h].visible_W || forest[w, h].visible_S || forest[w, h].visible_E;
               visible += visibleAny ? 1 : 0;
               scenicMax = Math.Max( scenicMax, forest[w, h].view_N * forest[w, h].view_W * forest[w, h].view_S * forest[w, h].view_E );
            }
         }

         Console.WriteLine( "visible: " + visible );
         Console.WriteLine( "scenic: " + scenicMax );
      }
   }
}
