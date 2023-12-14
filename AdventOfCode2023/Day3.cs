public class DAy3
{
    void Day3_Part1()
    {
        var input = 
            @"467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..";
        
        var l = input.Split(Environment.NewLine).ToList();
        l = File.ReadAllLines("Day3_part1.txt").ToList();

        l.Insert(0,string.Concat(Enumerable.Repeat(".", l.First().Length)));
        l.Add(string.Concat(Enumerable.Repeat(".", l.First().Length)));
        var sum = 0;
        var stars = new Dictionary<(int, int), List<int>>();
        for (int i = 0; i < l.Count; i++)
        {
            int? s = null;
            for (int j = 0; j < l[i].Length; j++)
            {
                if (char.IsNumber(l[i][j]) && s == null)
                {
                    s = j;
                }

                if ((!char.IsNumber(l[i][j]) || j == l[i].Length - 1 )&& s != null)
                {
                    // har ett nummer
                    var start = s.Value;
                    int end = j - 1;
                    if (j == l[i].Length - 1 && char.IsNumber(l[i][j]) )
                        end = j;
                    int length = end - start + 1;
                    
                    int os = Math.Max(0,start - 1);
                    int oe = Math.Min(l[i].Length - 1, end + 1);
                    int ol = oe - os + 1;
                    
                    var ns = l[i].Substring(start, length);
                    
                    var isAdj = l.GetRange(i - 1, 3)
                        .Any(x => x.Substring(os, ol)
                            .Any(
                                x => !char.IsNumber(x) && x != ".".ToCharArray()[0]
                            )
                        );
                    
                    if (isAdj)
                    {
                        sum += int.Parse(ns);
                        
                    }
                    else 
                    {
                        Console.WriteLine($"{ns} is not adjesent");
                    }
                    
                    s = null;
                }
            }
        }
        Console.WriteLine($" final sum {sum}");
    }

    void Day3_Part2()
    {
        var input = 
            @"467..114..
    ...*......
    ..35..633.
    ......#...
    617*......
    .....+.58.
    ..592.....
    ......755.
    ...$.*....
    .664.598..";
        
        var l = input.Split(Environment.NewLine).ToList();
        l = File.ReadAllLines("Day3_part1.txt").ToList();

        l.Insert(0,string.Concat(Enumerable.Repeat(".", l.First().Length)));
        l.Add(string.Concat(Enumerable.Repeat(".", l.First().Length)));
        var sum = 0;
        var stars = new Dictionary<(int, int), List<int>>();
        for (int i = 0; i < l.Count; i++)
        {
            int? s = null;
            for (int j = 0; j < l[i].Length; j++)
            {
                if (char.IsNumber(l[i][j]) && s == null)
                {
                    s = j;
                }

                if ((!char.IsNumber(l[i][j]) || j == l[i].Length - 1 )&& s != null)
                {
                    // har ett nummer
                    var start = s.Value;
                    int end = j - 1;
                    if (j == l[i].Length - 1 && char.IsNumber(l[i][j]) )
                        end = j;
                    int length = end - start + 1;
                    
                    int os = Math.Max(0,start - 1);
                    int oe = Math.Min(l[i].Length - 1, end + 1);
                    int ol = oe - os + 1;
                    
                    var ns = l[i].Substring(start, length);

                    for (int i2 = i-1; i2 < i+2; i2++)
                    {
                        for (int j2 = os; j2 <= oe; j2++)
                        {
                            if (l[i2][j2] == "*".ToCharArray()[0])
                            {
                                if (!stars.ContainsKey((i2,j2)))
                                    stars.Add((i2,j2), new List<int>());
                                stars[(i2,j2)].Add(int.Parse(ns));
                                
                            }
                        }
                    }
                    s = null;
                }
            }
        }

        var gears = stars.Where(x => x.Value.Count == 2);
        foreach (var gear in gears)
        {
            Console.WriteLine($"found gear {gear.Key.Item1},{gear.Key.Item2} val {gear.Value.First()}*{gear.Value.Last()}");
        }
        
        
        Console.WriteLine($" final sum {gears.Sum(y => y.Value.First()*y.Value.Last())}");
    }

}