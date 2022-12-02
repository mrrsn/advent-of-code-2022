using System;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day02
{
   public class Day02
   {
      public static void Main02()
      {
         var lines = File.ReadAllLines(@"C:\Users\r.morrison\source\repos\advent-of-code-2022\Day02\input");
         Console.WriteLine( lines.Sum( line => ScoreRoundPart1( line[0], line[2] ) ) );
         Console.WriteLine( lines.Sum( line => ScoreRoundPart2( line[0], line[2] ) ) );
      }

      private static int ScoreRoundPart2( char elf, char result )
      {
         var resultScore = result switch
         {
            'X' => 0,
            'Y' => 3,
            'Z' => 6,
            _ => 0
         };

         var me = result switch
         {
            'Y' => elf,
            'X' => elf switch
            {
               'A' => 'C',
               'B' => 'A',
               'C' => 'B',
               _ => ' '
            },
            'Z' => elf switch
            {
               'A' => 'B',
               'B' => 'C',
               'C' => 'A',
               _ => ' '
            },
            _ => '\0'
         };

         var shapeScore = me switch
         {
            'A' => 1,
            'B' => 2,
            'C' => 3,
            _ => 0
         };

         return resultScore + shapeScore;
      }

      private static int ScoreRoundPart1( char elf, char me )
      {
         var shapeScore = me switch
         {
            'X' => 1,
            'Y' => 2,
            'Z' => 3,
            _ => 0
         };

         var outcomeScore = ( elf == 'A' && me == 'X' ) || ( elf == 'B' && me == 'Y' ) || ( elf == 'C' && me == 'Z' )
            ? 3
            : elf switch
            {
               'A' when me == 'Y' => 6,
               'B' when me == 'Z' => 6,
               'C' when me == 'X' => 6,
               _ => 0
            };

         return shapeScore + outcomeScore;
      }
   }
}
