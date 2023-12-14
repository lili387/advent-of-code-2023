public class Day2
{
    void DAy2_Part1()
{
    var input =
        @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
    var lines = input.Split(Environment.NewLine);
    lines = File.ReadAllLines("Day2_part1.txt");
    
    var s = 0;
    foreach (var line in lines)
    {
        int id = int.Parse(line.Replace("Game ", "").Split(": ")[0]);
        var grps = 
            line.Split(": ").Last().Split("; ").Select(gr => gr.Split(", ").Select(x => (int.Parse(x.Split(" ")[0]),x.Split(" ")[1])));

        var blue = grps.Max(x => x.Where(y => y.Item2 == "blue").Sum(y => y.Item1));
        var green = grps.Max(x => x.Where(y => y.Item2 == "green").Sum(y => y.Item1));
        var red = grps.Max(x => x.Where(y => y.Item2 == "red").Sum(y => y.Item1));
        if (red > 12)
        {
            Console.WriteLine($"To Many reds {red} in line {line}");
        }
        else if (green > 13)
        {
            Console.WriteLine($"To Many green {green} in line {line}");
        }
        else if (blue > 14)
        {
            Console.WriteLine($"To Many blue {blue} in line {line}");
        }
        else
        {
            s += id;
        }
    }
    Console.WriteLine($"Sum {s}");

}

void DAy2_Part2()
{
    var input =
        @"Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue
Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red
Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red
Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green";
    var lines = input.Split(Environment.NewLine);
    lines = File.ReadAllLines("Day2_part1.txt");
    
    var s = 0;
    foreach (var line in lines)
    {
        int id = int.Parse(line.Replace("Game ", "").Split(": ")[0]);
        var grps = 
            line.Split(": ").Last().Split("; ")
                .Select(gr => gr.Split(", ").Select(x => (int.Parse(x.Split(" ")[0]),x.Split(" ")[1])));

        var blue = grps.Max(x => x.Where(y => y.Item2 == "blue").Sum(y => y.Item1));
        var green = grps.Max(x => x.Where(y => y.Item2 == "green").Sum(y => y.Item1));
        var red = grps.Max(x => x.Where(y => y.Item2 == "red").Sum(y => y.Item1));
        Console.WriteLine($"red {red}, green {green}, blue {blue}");
        s += blue * green * red;
    }

    Console.WriteLine($"the poser {s}");
}
}