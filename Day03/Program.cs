using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace advent_of_code_2022.Day03
{
   public class Day03
   {
      public static void Main03()
      {
         var score1 = 0;
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\Downloads\advent-of-code-2022-main\Day03\test");
         foreach ( var line in lines )
         {
            var groups = line.GroupBy(c => c).Where(g=>g.Count() > 1);
            foreach ( var group in groups )
            {
               if ( line.Substring( 0, line.Length / 2 ).Contains( group.Key ) && line.Substring( line.Length / 2, line.Length / 2 ).Contains( group.Key ) )
               {
                  score1 += priority[group.Key];
                  break;
               }
            }
         }

         var score2 = 0;
         for ( var i = 0; i < lines.Length; i += 3 )
         {
            var allbags = lines[i] + lines[i+1] + lines[i+2];
            var commons = allbags.GroupBy(c=>c).Where(g=>g.Count() > 1);
            foreach ( var group in commons )
            {
               if ( lines[i].Contains(group.Key) && lines[i+1].Contains(group.Key) && lines[i+2].Contains(group.Key) )
               {
                  score2 += priority[group.Key];
                  break;
               }
            }
         }

         Console.WriteLine( score1 );
         Console.WriteLine( score2 );
      }

      public static Dictionary<char, int> priority = new Dictionary<char, int>(){ { 'a', 1 }, { 'b', 2 }, { 'c', 3 }, { 'd', 4 }, { 'e', 5 }, { 'f', 6 }, { 'g', 7 }, { 'h', 8 }, { 'i', 9 }, { 'j', 10 }, { 'k', 11 }, { 'l', 12 }, { 'm', 13 }, { 'n', 14 }, { 'o', 15 }, { 'p', 16 }, { 'q', 17 }, { 'r', 18 }, { 's', 19 }, { 't', 20 }, { 'u', 21 }, { 'v', 22 }, { 'w', 23 }, { 'x', 24 }, { 'y', 25 }, { 'z', 26 },
                                                                                  { 'A', 27 }, { 'B', 28 }, { 'C', 29 }, { 'D', 30 }, { 'E', 31 }, { 'F', 32 }, { 'G', 33 }, { 'H', 34 }, { 'I', 35 }, { 'J', 36 }, { 'K', 37 }, { 'L', 38 }, { 'M', 39 }, { 'N', 40 }, { 'O', 41 }, { 'P', 42 }, { 'Q', 43 }, { 'R', 44 }, { 'S', 45 }, { 'T', 46 }, { 'U', 47 }, { 'V', 48 }, { 'W', 49 }, { 'X', 50 }, { 'Y', 51 }, { 'Z', 52 }
      };
   }
}
