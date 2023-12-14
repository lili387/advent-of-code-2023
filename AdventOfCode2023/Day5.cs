
using System.Text.Json;


public class DAy5
{
    
void Day5_Part1()
{
    var input =
        @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    var lines = input.Split(Environment.NewLine).ToList();
    lines = File.ReadAllLines("Day5_part1.txt").ToList();

    var seeds = lines.First().Replace("seeds: ", "").Split(" ").Select(long.Parse).ToList();
    var trans = new List<List<(long, long, long)>>();
    foreach (var line in lines.Skip(2).Where(x => !string.IsNullOrEmpty(x)))
    {
        if (line.Contains("map:"))
        {
            trans.Add(new List<(long, long, long)>());
        }
        else
        {
            var nums = line.Split(" ").Select(long.Parse).ToArray();
            trans.Last().Add((nums[1], nums[0], nums[2]));
        }
    }

    var locs = new List<long>();
    foreach (var seed in seeds)
    {
        var val = seed;
        Console.Write($"{val}, ");
        foreach (var tran in trans)
        {
            var t = tran.Where(x => x.Item1 <= val && val < x.Item1 + x.Item3).ToArray();
            if (t.Any())
                val = val + t[0].Item2 - t[0].Item1;
            Console.Write($"{val}, ");
        }

        locs.Add(val);
        Console.WriteLine();
    }

    Console.WriteLine(JsonSerializer.Serialize(locs));
    Console.WriteLine($"Min {locs.Min()}");
}

void Day5_Part2()
{
    var input =
        @"seeds: 79 14 55 13

seed-to-soil map:
50 98 2
52 50 48

soil-to-fertilizer map:
0 15 37
37 52 2
39 0 15

fertilizer-to-water map:
49 53 8
0 11 42
42 0 7
57 7 4

water-to-light map:
88 18 7
18 25 70

light-to-temperature map:
45 77 23
81 45 19
68 64 13

temperature-to-humidity map:
0 69 1
1 0 69

humidity-to-location map:
60 56 37
56 93 4";

    var lines = input.Split(Environment.NewLine).ToList();
    lines = File.ReadAllLines("Day5_part1.txt").ToList();

    var s1 = new List<long>();
    var s2 = new List<long>();
    foreach (var s in lines.First().Replace("seeds: ", "").Split(" ").Select(long.Parse).ToList())
    {
        if (s1.Count > s2.Count)
            s2.Add(s);
        else
            s1.Add(s);
    }

    var trans_layers = new List<List<(long, long, long)>>();
    foreach (var line in lines.Skip(2).Where(x => !string.IsNullOrEmpty(x)))
    {
        if (line.Contains("map:"))
        {
            trans_layers.Add(new List<(long, long, long)>());
        }
        else
        {
            var nums = line.Split(" ").Select(long.Parse).ToArray();
            long start = nums[1];
            long end = nums[1] + nums[2] - 1;
            long diff = nums[0] - nums[1];
            trans_layers.Last().Add((start, end, diff));
        }
    }

    var locs = new List<(long, long)>();
    for (int i = 0; i < s1.Count; i++)
    {
        var srs = new List<(long, long)> { (s1[i], s1[i] + s2[i] - 1) };
        var srs_t = new List<(long, long)>();
        for (int j = 0; j < trans_layers.Count; j++)
        {
            var trans = trans_layers[j];
             foreach (var sr in srs) // alla oliak seed range
            {
                // splitta
                var splits = new List<(long, long, long)> { (sr.Item1, sr.Item2, 0) }; // rangen kan delas upp
                foreach (var tran in trans.OrderBy(x => x.Item1))
                {
                    var newSplits = new List<(long, long, long)>();
                    foreach (var split in splits)
                    {
                        // A
                        if (split.Item1 < tran.Item1 && tran.Item2 < split.Item2)
                        {
                            newSplits.Add((split.Item1, tran.Item1 - 1, split.Item3));
                            newSplits.Add((tran.Item1, tran.Item2, tran.Item3));
                            newSplits.Add((tran.Item2 + 1, split.Item2, split.Item3));
                        }
                        // B
                        else if (tran.Item1 <= split.Item1 && split.Item2 <= tran.Item2)
                        {
                            newSplits.Add((split.Item1, split.Item2, tran.Item3));
                        }
                        // C
                        else if (split.Item1 <= tran.Item2 && tran.Item2 < split.Item2 )
                        {
                            newSplits.Add((split.Item1, tran.Item2, tran.Item3));
                            newSplits.Add((tran.Item2 + 1, split.Item2, split.Item3));
                        }
                        // D
                        else if (split.Item1 < tran.Item1 && tran.Item1 <= split.Item2)
                        {
                            newSplits.Add((split.Item1, tran.Item1-1, split.Item3));
                            newSplits.Add((tran.Item1, split.Item2, tran.Item3));
                        }
                        // E
                        else
                        {
                            newSplits.Add((split.Item1, split.Item2, split.Item3));
                        }
                    }

                    splits = newSplits;
                }

                srs_t.AddRange(splits.Select(x => (x.Item1 + x.Item3, x.Item2 + x.Item3)));
            }

            srs = srs_t;
            srs_t = new List<(long, long)>();
        }

        locs.AddRange(srs);
    }

    Console.WriteLine($"Min {locs.Select(x => x.Item1).Min()} in {locs.Count}");
}
}