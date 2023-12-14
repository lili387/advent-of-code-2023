namespace AdventOfCode2023;

public class Day6
{
    void Day6_Part1()
    {
        var input =
            @"Time:      7  15   30
Distance:  9  40  200";
    
    
    
        var lines = input.Split(Environment.NewLine).ToList();
        //lines = File.ReadAllLines("Day6_part1.txt").ToList();

        var Y = lines.Last().Split(" ").Where(x => !string.IsNullOrEmpty(x) && x != " ").Skip(1).Select(double.Parse).ToArray();
        var X = lines.First().Split(" ").Where(x => !string.IsNullOrEmpty(x) && x != " ").Skip(1).Select(double.Parse).Select(x => x - 0.0000001).ToArray();

        var prod = 1.0;
        for (int i = 0; i < X.Length; i++)
        {
            var t1 = X[i] / 2 - Math.Pow(Math.Pow(X[i] / 2, 2) - Y[i], 0.5);
            var t2 = X[i] / 2 + Math.Pow(Math.Pow(X[i] / 2, 2) - Y[i], 0.5);
            Console.WriteLine($"From {t1} to {t2} solutions {Math.Floor(t2) - Math.Ceiling(t1) + 1}");
            prod *= Math.Floor(t2) - Math.Ceiling(t1) + 1;
        }
        Console.WriteLine($"Producten {prod}");
    }

    void Day6_Part2()
    {
        var input =
            @"Time:      7  15   30
Distance:  9  40  200";
    
    
    
        var lines = input.Split(Environment.NewLine).ToList();
        lines = File.ReadAllLines("Day6_part1.txt").ToList();

        var Y = new List<double> { double.Parse(new string(lines.Last().Where(x => Char.IsNumber(x)).ToArray())) };
        var X = new List<double> { double.Parse(new string(lines.First().Where(x => Char.IsNumber(x)).ToArray()))};
    
        var prod = 1.0;
        for (int i = 0; i < X.Count; i++)
        {
            var t1 = X[i] / 2 - Math.Pow(Math.Pow(X[i] / 2, 2) - Y[i], 0.5);
            var t2 = X[i] / 2 + Math.Pow(Math.Pow(X[i] / 2, 2) - Y[i], 0.5);
            Console.WriteLine($"From {t1} to {t2} solutions {Math.Floor(t2) - Math.Ceiling(t1) + 1}");
            prod *= Math.Floor(t2) - Math.Ceiling(t1) + 1;
        }
        Console.WriteLine($"Producten {prod}");
    }
}