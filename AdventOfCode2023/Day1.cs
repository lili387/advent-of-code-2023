public class Day1
{
    public static void Day1_part1()
    {
        IEnumerable<string> nums;
        nums = File.ReadLines("day1.txt");
        int s = 0;
        foreach (var n in nums)
        {
            //n.FirstOrDefault(),n.LastOrDefault()
            var nnums = n.Where(c => Char.IsNumber(c)).ToList();
            var t = new char[] { nnums.FirstOrDefault(), nnums.LastOrDefault() };
            s += int.Parse(t);
        }
        Console.WriteLine($"Day 1 sum {s}");
    }
    
    public static void Day1_part2()
    {
        string[] nums;
        nums = File.ReadLines("day1.2.txt").ToArray();
        
        // test data
    //     var testInput =
    // @"two1nine
    // eightwothree
    // abcone2threexyz
    // xtwone3four
    // 4nineeightseven2
    // zoneight234
    // 7pqrstsixteen";
    //     
    //     nums = testInput.Split(Environment.NewLine);
        var replacemnets = new Dictionary<string, string>
        {
            { "one", "one1one" },
            { "two", "two2two" },
            { "three", "three3three" },
            { "four", "four4four" },
            { "five", "five5five" },
            { "six", "six6six" },
            { "seven", "seven7seven" },
            { "eight", "eight8eight" },
            { "nine", "nine9nine" }
        };
        nums = nums.Select(n =>
        {
            var s = n;
            foreach (var r in replacemnets)
            {
                s = s.Replace(r.Key, r.Value);
            }
            Console.WriteLine(s);
            return s;
        }).ToArray();
        int s = 0;
        foreach (var n in nums)
        {
            Console.WriteLine(n);
            //n.FirstOrDefault(),n.LastOrDefault()
            var nnums = n.Where(c => Char.IsNumber(c)).ToList();
            var t = new char[] { nnums.FirstOrDefault(), nnums.LastOrDefault() };
            Console.WriteLine($"Line{n} tal {int.Parse(t)}");
            s += int.Parse(t);
        }
        Console.WriteLine($"Day 1 sum {s}");
    }
}