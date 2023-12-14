namespace AdventOfCode2023;

public class Day4
{
    void Day4_Part1()
    {
        var input = 
            @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
        
        var lines = input.Split(Environment.NewLine).ToList();
        lines = File.ReadAllLines("Day4_part1.txt").ToList();
        var sum = 0.0;

        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            var num_lines = line.Split(": ").Last().Split("| ");
            var c = num_lines[0].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x));
            var d = num_lines[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x));
            var n = c.Count(x => d.Contains(x));
            var val = Math.Pow(2,n-1);
            if (n > 0)
                sum += val;
        }

        Console.WriteLine($" final sum {sum}");

    }

    void Day4_Part2()
    {
        var input = 
            @"Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53
    Card 2: 13 32 20 16 61 | 61 30 68 82 17 32 24 19
    Card 3:  1 21 53 59 44 | 69 82 63 72 16 21 14  1
    Card 4: 41 92 73 84 69 | 59 84 76 51 58  5 54 83
    Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36
    Card 6: 31 18 13 56 72 | 74 77 10 23 35 67 36 11";
        
        var lines = input.Split(Environment.NewLine).ToList();
        lines = File.ReadAllLines("Day4_part1.txt").ToList();
        var sum = 0.0;

        int CountC(int j)
        {
            var line = lines[j];
            var num_lines = line.Split(": ").Last().Split("| ");
            var c = num_lines[0].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x));
            var d = num_lines[1].Split(" ").Where(x => !string.IsNullOrEmpty(x)).Select(x => int.Parse(x));
            var n = c.Count(x => d.Contains(x));
            var sum = 1;
            if (n == 0)
                return sum;
                
            for (int k = j + 1; k < Math.Min(lines.Count, j + n + 1); k++)
            {
                if (k % 10 == 0 && j % 10 == 0)
                    Console.WriteLine($"Call {k} from {j}");
                sum += CountC(k);
            }

            // Console.WriteLine($"Line {j} sum {sum}");
            return sum;
        }

        var t_sum = lines.Select((x, i) => CountC(i)).Sum();

        Console.WriteLine($" final sum {t_sum}");

    }
}