void Day13_Part1()
{
    var input =
        @"#.##..##.
..#.##.#.
##......#
##......#
..#.##.#.
..##..##.
#.#.##.#.

#...##..#
#....#..#
..##..###
#####.##.
#####.##.
..##..###
#....#..#";

    var lines = input.Split(Environment.NewLine).ToArray();
    lines = File.ReadAllLines("Day13_part1.txt").ToArray();
    var maps = new List<List<string>> { new() };
    foreach (var line in lines)
    {
        if (String.IsNullOrWhiteSpace(line))
            maps.Add(new());
        else
            maps.Last().Add(line);
    }

    List<int> GetRowMir(string row)
    {
        List<int> row_mir = new List<int>();
        for (int i = 1; i < row.Length; i++)
        {
            bool isMirror = true;
            var l = i - 1;
            var r = i;
            while (0 <= l && r < row.Length)
            {
                if (row[l--] != row[r++])
                {
                    isMirror = false;
                    break;
                }
            }
            if (isMirror)
                row_mir.Add(i);
        }
        return row_mir;
    }

    List<string> Transpose(List<string> matrix)
    {
        var t = new List<string>();
        for (int i = 0; i < matrix.First().Length; i++)
        {
            t.Add(string.Concat(matrix.Select(x => x[i])));
        }
        return t;
    }

    var part1Total = 0;
    var part2Total = 0;
    foreach (var map in maps)
    {
        var colPos = map.Select(x => GetRowMir(x)).SelectMany(x => x).GroupBy(x => x).Select(x => x).Where(x => x.Count() == map.Count).Select(x => x.Key).ToList();
        var rowPos = Transpose(map).Select(x => GetRowMir(x)).SelectMany(x => x).GroupBy(x => x).Select(x => x).Where(x => x.Count() == map.First().Length).Select(x => x.Key).ToList();
        int subTotal = colPos.Sum() + 100 * rowPos.Sum();
        part1Total += subTotal;
        
        var newColPos = map.Select(x => GetRowMir(x)).SelectMany(x => x).GroupBy(x => x).Select(x => x).Where(x => x.Count() == map.Count-1).Select(x => x.Key).ToList();
        var newRowPos = Transpose(map).Select(x => GetRowMir(x)).SelectMany(x => x).GroupBy(x => x).Select(x => x).Where(x => x.Count() == map.First().Length -1 ).Select(x => x.Key).ToList();
        var newSubTotal = newColPos.Sum() + 100 * newRowPos.Sum();
        part2Total += newSubTotal;
    }

    Console.WriteLine($"Total {part1Total}, new Total {part2Total}");
}

Day13_Part1();