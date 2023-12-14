namespace AdventOfCode2023;

public class Day7
{
    void Day7_Part1()
{
    var input =
@"2345A 1
Q2KJJ 13
Q2Q2Q 19
T3T3J 17
T3Q33 11
2345J 3
J345A 2
32T3K 5
T55J5 29
KK677 7
KTJJT 34
QQQJA 31
JJJJJ 37
JAAAA 43
AAAAJ 59
AAAAA 61
2AAAA 23
2JJJJ 53
JJJJ2 41";
    
    var lines = input.Split(Environment.NewLine).ToList();
    lines = File.ReadAllLines("Day7_part1.txt").ToList();

    var new_cards = (string hand) => hand
        .Replace("A", "F")
        .Replace("K", "E")
        .Replace("Q", "D")
        .Replace("J", "1")
        .Replace("T", "B");

    var hand_score = (string hand) =>
    {
        var countsWithJs = hand.GroupBy(x => x).Select(x => new { x.Key, count = x.Count() }).OrderByDescending(x => x.count)
            .ThenByDescending(x => x.Key).ToList();

        string newHand = hand;
        if (hand != "11111")
        {
            var newValue = countsWithJs.First(x => x.Key.ToString() != "1").Key.ToString();
            newHand = hand.Replace("1", newValue);
        }
            
        var counts = newHand.GroupBy(x => x).Select(x => new { x.Key, count = x.Count() }).OrderByDescending(x => x.count)
            .ThenByDescending(x => x.Key).ToList();
        
        if (counts.Any(x => x.count == 5))
            return "9" + hand;
        if (counts.Any(x => x.count == 4))
             return "8" + hand;
        if (counts.Any(x => x.count == 3) && counts.Any(x => x.count == 2))
            return "7" + hand;
        if (counts.Any(x => x.count == 3))
            return "6" + hand;
        if (counts.Count(x => x.count == 2) == 2)
            return "5" + hand;
        if (counts.Any(x => x.count == 2))
            return "4" + hand;
        
        return "3" + hand;
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
}