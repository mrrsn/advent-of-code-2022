using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;

namespace advent_of_code_2022.Day07
{
   public class Day07
   {
      public enum Typ
      {
         Dir,
         NotDir
      }

      public class Node
      {
         public Node parent;
         public string name;
         public int size;
         public Typ type;
         public List<Node> children;
      }

      public static void Main07()
      {
         var lines = File.ReadAllLines(@"C:\Users\mrrsn\source\repos\advent-of-code-2022\Day07\input");

         var root = new Node
         {
            parent = null,
            name = "/",
            size = 0,
            type = Typ.Dir,
            children = new List<Node>()
         };
         BuildTree( lines, root );

         var sum = SumAllDirsNoBiggerThan100k( root );
         Console.WriteLine( "part 1: " + sum );
         var unusedSpace = 70000000 - root.size;
         var needToDelete = 30000000 - unusedSpace;
         var sizes = new List<long>();
         AllDirSizes( root, sizes );
         sizes.Sort();
         long sizeToDelete = 0;
         foreach ( var size in sizes )
         {
            if ( size < needToDelete )
               continue;

            sizeToDelete = size;
            break;
         }
         Console.WriteLine( "part 2: " + sizeToDelete );
      }

      private static long SumAllDirsNoBiggerThan100k( Node node )
      {
         long sum = 0;
         foreach ( var child in node.children )
         {
            if ( child.type != Typ.Dir ) continue;
            if ( child.size <= 100000 ) sum += child.size;
            sum += SumAllDirsNoBiggerThan100k( child );
         }

         return sum;
      }

      private static void AllDirSizes( Node node, List<long> sizes )
      {
         foreach ( var child in node.children )
         {
            if ( child.type != Typ.Dir ) continue;
            sizes.Add( child.size );
            AllDirSizes( child, sizes );
         }
      }

      private static void BuildTree( string[] lines, Node root )
      {
         Node current = root;

         for ( int i = 0; i < lines.Length; ++i )
         {
            var parts = lines[i].Split( ' ' );

            if ( lines[i].Equals( "$ cd /" ) )
            {
               current = root;
            }
            else if ( lines[i].Equals( "$ cd .." ) )
            {
               current.parent.size += current.size;
               current = current.parent;
            }
            else if ( lines[i].StartsWith( "$ cd " ) )
            {
               var dirName = lines[i].Split( ' ' )[2];
               current = current.children.Where( n => n.name.Equals( dirName ) ).First();
            }
            else if ( lines[i].Equals( "$ ls" ) )
            {
               continue;
            }
            else if ( lines[i].StartsWith( "dir" ) )
            {
               current.children.Add( new Node() { parent = current, name = parts[1], size = 0, type = Typ.Dir, children = new List<Node>() } );
            }
            else
            {
               current.children.Add( new Node() { parent = current, name = parts[1], size = int.Parse( parts[0] ), type = Typ.NotDir, children = null } );
               current.size += int.Parse( parts[0] );
            }
         }

         var tempSize = current.size;
         do
         {
            current = current.parent;
            current.size += tempSize;
         } while ( current.parent != null );
      }
   }
}
