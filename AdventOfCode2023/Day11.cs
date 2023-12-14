namespace AdventOfCode2023;

public class Day11
{
 void Day11_Part1()
 {
     var input =
         @"...#......
 .......#..
 #.........
 ..........
 ......#...
 .#........
 .........#
 ..........
 .......#..
 #...#.....";
     
     var lines = input.Split(Environment.NewLine).ToArray();
     lines = File.ReadAllLines("Day11_part1.txt").ToArray();
 
     var exp_rows = lines.Select((x, i) => (x,i)).Where(l => l.x.All(x => x == '.')).Select(x => x.i).ToList();
     var exp_cols = new List<int>();
     for (int i = 0; i < lines.First().Length; i++)
     {
         if(lines.Select(x => x[i]).All(x => x == '.'))
             exp_cols.Add(i);
     }
 
     var gal = new List<P2>();
     for (int x = 0; x < lines.First().Length; x++)
     {
         for (int y = 0; y < lines.Length; y++)
         {
             if (lines[y][x] != '#')
                 continue;
             
             var rx = x + exp_cols.Count(t => t < x)*(1000000-1);
             var ry = y + exp_rows.Count(t => t < y)*(1000000-1);
             gal.Add(new P2(rx,ry));
         }
     }
 
     long tot_d = 0;
     for (int i = 0; i < gal.Count; i++)
     {
         for (int j = i+1; j < gal.Count; j++)
         {
             var g1 = gal[i];
             var g2 = gal[j];
             tot_d += Math.Abs(g1.X - g2.X) + Math.Abs(g1.Y - g2.Y);
         }
     }
     
     // int min_d(int d, P2 g, List<P2> gals)
     // {
     //     if (gals.Count == 0)
     //         return d;
     //
     //     var min_td = new List<int>();
     //     foreach (var ng in gals)
     //     {
     //         var dd = d + Math.Abs(g.X - ng.X) + Math.Abs(g.Y - ng.Y) - 1;
     //         var td = min_d(dd, ng, gals.Where(x => x != ng).ToList());
     //         min_td.Add(td);
     //     }
     //     
     //     return min_td.Min();
     // }
     //
     // var ds = new List<int>();
     // foreach (var g in gal)
     // {
     //     ds.Add(min_d(0,g,gal.Where(x => x != g).ToList()));
     // }
     
     Console.WriteLine($"sum short {tot_d}");
 }   
}