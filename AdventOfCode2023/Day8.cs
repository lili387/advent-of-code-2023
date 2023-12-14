using System.Numerics;
using Open.Numeric.Primes;

public class Day8
{
    void Day7_PartX()
{
    var input =
@"32T3K 765
T55J5 684
KK677 28
KTJJT 220
QQQJA 483";
    
    var lines = input.Split(Environment.NewLine).ToList();
    lines = File.ReadAllLines("Day7_part1.txt").ToList();

    var new_cards = (string hand) => hand
        .Replace("A", "F")
        .Replace("K", "E")
        .Replace("Q", "D")
        .Replace("J", "C")
        .Replace("T", "B");

    var hand_score = (string hand) =>
    {
        var counts = hand.GroupBy(x => x).Select(x => new { x.Key, count = x.Count() }).OrderByDescending(x => x.count)
            .ThenByDescending(x => x.Key).ToList();
        var scoreC = "0000000".ToCharArray();
        foreach (var countcard in counts)
        {
            if (countcard.count == 5)
                scoreC[0] = countcard.Key;
            if (countcard.count == 4)
                scoreC[1] = countcard.Key;
        }
        if (counts.Any(x => x.count == 3) && counts.Any(x => x.count == 2))
        {
            scoreC[2] = counts.Single(x => x.count == 3).Key;
            scoreC[3] = counts.Single(x => x.count == 2).Key;
        }
        else if (counts.Any(x => x.count == 3))
        {
            scoreC[4] = counts.Single(x => x.count == 3).Key;
        }
        else if (counts.Count(x => x.count == 2) == 2)
        {
            scoreC[5] = counts.Where(x => x.count == 2).First().Key;
            scoreC[6] = counts.Where(x => x.count == 2).Last().Key;
        }
        else if (counts.Any(x => x.count == 2))
        {
            scoreC[6] = counts.Single(x => x.count == 2).Key;
        }
        
        var score = new string(scoreC);
        score += (new string(counts.Where(x => x.count == 1).Select(x => x.Key).ToArray())).PadLeft(5,"0".ToCharArray()[0]);
        return score;
    };

    var hands = lines.Select(x => x.Split(" "))
        .Select(x => new { org = x[0], nc = new_cards(x[0]), hand_score = hand_score(new_cards(x[0])), score = x[1] }).ToList();

    var sum = 0;
    var i = 1;
    foreach (var hand in hands.OrderBy(x => x.hand_score))
    {
        sum += i*int.Parse(hand.score);
        Console.WriteLine($" {hand.org} {hand.hand_score} {hand.score} med {i} = {i*int.Parse(hand.score)}");
        i += 1;
    }
        
    Console.WriteLine($"Tot score{sum} of {hands.Count()}");
    // foreach (var hand in hands)
    // {
    //     Console.WriteLine($"Producten {hand.org} med {hand.nc} ger score {hand.hand_score} ");
    // }
}

void Day7_Part1()
{
    var input =
@"LR

11A = (11B, XXX)
11B = (XXX, 11Z)
11Z = (11B, XXX)
22A = (22B, XXX)
22B = (22C, 22C)
22C = (22Z, 22Z)
22Z = (22B, 22B)
XXX = (XXX, XXX)";
    
    var lines = input.Replace(" ", "").Replace(")", "").Replace("(", "").Split(Environment.NewLine).ToList();
    lines = File.ReadAllLines("Day8_part1.txt").Select(x => x.Replace(" ", "").Replace(")", "").Replace("(", "")).ToList();
    
    var inst = lines.First().Select(x =>
    {
        if (x == "L".ToCharArray()[0])
            return 0;
        return 1;
    }).ToList();

    var nodes = lines.Skip(2).Select(x => new
    {
        key = x.Split("=").First(),
        list = x.Split("=").Last().Split(",")
    }).ToDictionary(x => x.key, y=> y.list);

    int CountSteps(string cur)
    {
        var i = 0;

        while (cur.Last() != "Z".ToCharArray()[0])
        {
            //Console.WriteLine($"On step {step} we look at inst {step%inst.Count} gives instruction {inst[step%(inst.Count())]}");
            var newcur = nodes[cur][inst[i % (inst.Count())]];
            //($"{cur} => {newcur}");
            i += 1;
            cur = newcur;
        }

        return i;
    }

    Console.WriteLine("slut steps");
    var allSteps = nodes.Keys.Where(x => x[2] == "A".ToCharArray()[0]).Select(CountSteps);

    Console.WriteLine("våra steps");
    foreach (var stepCount in allSteps)
    {
        Console.Write($"{stepCount},");
    }
    
    var pf = allSteps.Select(x => Prime.Factors(x).Where(y => y > 1)).ToList();
    
    var all_pg = pf.SelectMany(x => x.GroupBy(y => y).Select(y => new { prime = y.Key, count = y.Count() }));

    var t = 3;
    if (DateTime.Now.Millisecond > 10)
    {
        t = pf.Count + allSteps.Count();
    }
    BigInteger prod = 1;
    var primes = all_pg.Select(x => x.prime).Distinct();
    
    foreach (var prime in primes)
    {
        int maxGroup = all_pg.Where(y => y.prime == prime).Max(y => y.count);
        for (int i = 0; i < maxGroup; i++)
        {
            prod *= prime;
        }
    }
    
    Console.WriteLine($"Tot steps {prod}");
}
}