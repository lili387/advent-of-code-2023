namespace AdventOfCode2023;

public class day9
{
    void Day9_Part1_2()
    {
        var input =
            @"0 3 6 9 12 15
1 3 6 10 15 21
10 13 16 21 30 45";
    
        var lines = input.Split(Environment.NewLine).ToList();
        lines = File.ReadAllLines("Day9_part1.txt").ToList();

        var sum_next = 0;
        var sum_prev = 0;
        foreach (var line in lines)
        {
            var nums = line.Split(" ").Select(int.Parse).ToArray();
            var cur = nums.Count()-1;
            var op = 1;
            var prev = nums[0];
            while (cur >= 0 && nums[..cur].Any(x => x != 0))
            {
                for (int i = 0; i < cur; i++)
                {
                    nums[i] = nums[i + 1] - nums[i];
                }
                prev -= op * nums[0];
                cur += -1;
                op *= -1;
            }
            sum_next += nums.Sum();
            sum_prev += prev;
        }
    
        Console.WriteLine($"Tot sum {sum_next}");
        Console.WriteLine($"Tot back {sum_prev}");
    }
}