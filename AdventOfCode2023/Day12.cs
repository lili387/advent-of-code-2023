namespace AdventOfCode2023;

public class Day12
{
    void Day12_Part1and2()
{
    var input =
    @"???.### 1,1,3
.??..??...?##. 1,1,3
?#?#?#?#?#?#?#? 1,3,1,6
????.#...#... 4,1,1
????.######..#####. 1,6,5
?###???????? 3,2,1";

    var lines = input.Split(Environment.NewLine).ToArray();
    lines = File.ReadAllLines("Day12_part1.txt").ToArray();
    
    var cache = new Dictionary<(int, int), long>();
    long CountCached(char[] line, int[] grps, int pos)
    {
        if (!cache.ContainsKey((grps.Length, pos)))
        {
            cache[(grps.Length, pos)] = Count(line, grps, pos);
        }

        return cache[(grps.Length, pos)];
    }
    
    long Count(char[] line, int[] grps, int pos)
    {
        // bad case # to left
        if (0 < pos && pos < line.Length && line[pos - 1] == '#')
            return 0;

        // good everything done
        if (grps.Length == 0 && (pos >= line.Length || !line[pos..].Any(x => x is '#')))
        {
            return 1;
        }

        // bad case line should be empty
        if (grps.Length == 0 && line[pos..].Any(x => x is '#'))
            return 0;

        // bad case at end no place to set grps
        if (grps.Length > 0 && pos + grps[0] > line.Length)
            return 0;
       
        // continiue building
        long subtotal = 0;
        var subString = line[pos..(pos + grps[0])];
        if (subString.All(x => x is '?' or '#') && (line.Length <= pos + grps[0] || line[pos + grps[0]] is '?' or '.'))
        {
            subtotal += CountCached(line, grps.Skip(1).ToArray(), pos + grps[0] + 1);
        }

        // evaluate if this grp could move one right
        subtotal += CountCached(line, grps, pos + 1);
        return subtotal;
    }

    var total_part1 = 0;
    long total_part2 = 0;
    foreach (var row in lines)
    {
        var line = row.Split(" ")[0].ToArray();
        var grps = row.Split(" ")[1].Split(",").Select(int.Parse).ToArray();
        cache = new Dictionary<(int, int), long>();
        var line5 = String.Join("?", Enumerable.Repeat(row.Split(" ")[0], 5)).ToArray();
        var grps5 = Enumerable.Repeat(grps, 5).SelectMany(x => x).ToArray();
        var subtotal_part2 = Count(line5, grps5, 0);
        Console.WriteLine($"Subtotal {subtotal_part2}");
        total_part2 += subtotal_part2;
    }

    Console.WriteLine($"part 2 {total_part2}");
}
}