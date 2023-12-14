public class day10
{
    void Day10_Part1_first()
{
    var input =
        @"...........
.S--7F---7.
.|F7LJ.F7|.
.|||...|||.
.||L---J||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........";

    var lines = input.Split(Environment.NewLine).ToArray();
    lines = File.ReadAllLines("Day10_part1.txt").ToArray();
    P2 up = new P2(0, -1);
    P2 down = new P2(0, 1);
    P2 right = new P2(1, 0);
    P2 left = new P2(-1, 0);
    P2 noDir = new P2(0, 0);

    char? GetC(P2 p2)
    {
        if (0 <= p2.Y && p2.Y < lines.Length &&
            0 <= p2.X && p2.X < lines[p2.Y].Length)
            return lines[p2.Y][p2.X];
        return null;
    }
    

    P2 StartPos(string[] strings)
    {
        P2 p2 = new P2(-1, -1);
        for (int j = 0; j < strings.Count(); j++)
        {
            if (strings[j].Contains("S"))
                p2 = new P2(strings[j].IndexOf("S"), j);
        }

        return p2;
    }
    
    bool IsNonePipe(P2 p2)
    {
        return 0 <= p2.Y && p2.Y < lines.Length &&
               0 <= p2.X && p2.X < lines[p2.Y].Length &&
               (lines[p2.Y][p2.X] == '.' || lines[p2.Y][p2.X] == 'I');
    }
    
    
    void UpDateNonMain(P2 p2, List<P2> main, char c)
    {
        if (!main.Any(x => x.X == p2.X && x.Y == p2.Y) && GetC(p2) != null)
        {
            var stringAsArray = lines[p2.Y].ToCharArray();
            stringAsArray[p2.X] = c;
            lines[p2.Y] = new string(stringAsArray);
        }
    }
    
    void UpDateNonPipe(P2 p2, char c)
    {
        if (IsNonePipe(p2))
        {
            var stringAsArray = lines[p2.Y].ToCharArray();
            stringAsArray[p2.X] = c;
            lines[p2.Y] = new string(stringAsArray);
        }
    }
    
    (int, P2) Step(P2 dir, P2 pos)
    {
        var c = lines[pos.Y][pos.X];

        if (c == 'F' && dir == up)
            return (1, right);
        if (c == 'F' && dir == left)
            return (-1, down);
        if (c == 'J' && dir == down)
            return (1, left);
        if (c == 'J' && dir == right)
            return (-1, up);
        if (c == 'L' && dir == down)
            return (-1, right);
        if (c == 'L' && dir == left)
            return (1, up);
        if (c == '7' && dir == right)
            return (1, down);
        if (c == '7' && dir == up)
            return (-1, left);
        if (c == '|' && dir == up)
            return (0, up);
        if (c == '|' && dir == down)
            return (0, down);
        if (c == '-' && dir == left)
            return (0, left);
        if (c == '-' && dir == right)
            return (0, right);
        return (0, noDir);
    }

    char? Next(P2 dir, P2 pos)
    {
        var next = new P2(dir.X + pos.X, dir.Y + pos.Y);
        if (next.X < 0 || lines[0].Length <= next.X ||
            next.Y < 0 || lines.Length <= next.Y)
            return null;
        return lines[next.Y][next.X];
    }

    var longest = 0;
    foreach (P2 cardDir in new List<P2> { up, right, down, left })
    {
        var steps = 1;
        var turnCount = 0;
        var dir = new P2(cardDir.X, cardDir.Y);
        var pos = StartPos(lines);
        var poss = new List<P2>();
        poss.Add(pos);

        while (Next(dir, pos) != null && Next(dir, pos) != 'S')
        {
            var newPos = pos + dir;
            var (turn, newdir) = Step(dir, newPos);
            turnCount += turn;
            if (newdir == noDir)
                break;

            dir = newdir;
            pos = newPos;
            poss.Add(pos);
            steps += 1;
        }

        Console.WriteLine($"Part steps {steps}");
        if (Next(dir, pos) == 'S' && steps / 2 > longest)
        {
            longest = steps / 2;

            var arean = 0;
            var possD = poss.ToDictionary(x => (x.X, x.Y), y => true);
            for (int i = 0; i < lines.Length; i++)
            {
                var isInside = false;
                for (int j = 0; j < lines[i].Length-1; j++)
                {
                    if (possD.ContainsKey((j, i)) && !possD.ContainsKey((j+1, i)))
                    {
                        isInside = !isInside;
                    }
                    else if (isInside)
                    {
                        arean += 1;
                    }
                }
            }
            Console.WriteLine($"Area {arean}");
        }

        //updates input with symbols inside and outside
        if (Next(dir, pos) == 'S' && turnCount > 0)
        {
            // walk again and mark inside and outside
            dir = new P2(cardDir.X, cardDir.Y);
            pos = StartPos(lines);
            
            while (Next(dir, pos) != null && Next(dir, pos) != 'S')
            {
                var newPos = pos + dir;
                var (cont, newdir) = Step(dir, newPos);
                
                dir = newdir;
                pos = newPos;
                
                if (dir == up)
                {
                    UpDateNonMain(pos+right,poss, 'I');
                }
                if (dir == down)
                {
                    UpDateNonMain(pos+left, poss,'I');
                }
                if (dir == right)
                {
                    UpDateNonMain(pos+down,poss, 'I');
                }
                if (dir == left)
                {
                    UpDateNonMain(pos+up,poss, 'I');
                }
            }
            
            void GrowI(P2 p2)
            {
                UpDateNonMain(p2, poss, 'I');
                foreach (P2 growDir in new List<P2> { up, right, down, left })
                {
                    var gPos = p2 + growDir;
                    if (GetC(gPos) != null && GetC(gPos) != 'I' && !poss.Any(x => x.X == gPos.X && x.Y == gPos.Y))
                        GrowI(gPos);
                }
            }
            
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (lines[i][j] == 'I')
                        GrowI(new P2(j,i));
                }
            }
            
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    if (!poss.Any(x => x.X == j && x.Y == i) && lines[i][j] != 'I')
                    {
                        var line = lines[i].ToCharArray();
                        line[j] = ' ';
                        lines[i] = new string(line);
                    }
                }
            }

            File.WriteAllLines("test.txt", lines);
        }
        
         foreach (var line in lines)
         {
             Console.WriteLine(line);
         }
    }

    var area = lines.Sum(x => x.Count(y => y == 'I'));

    Console.WriteLine($"Longest {longest} with area {area}");
}

void Day10_Part1_second()
{
    var input =
        @"...........
.S--7F---7.
.|F7LJ.F7|.
.|||...|||.
.||L---J||.
.|L-7.F-J|.
.|..|.|..|.
.L--J.L--J.
...........";

    var lines = input.Split(Environment.NewLine).ToArray();
    lines = File.ReadAllLines("Day10_part1.txt").ToArray();
    P2 up = new P2(0, -1);
    P2 down = new P2(0, 1);
    P2 right = new P2(1, 0);
    P2 left = new P2(-1, 0);
    P2 noDir = new P2(0, 0);

    char? GetC(P2 p2)
    {
        if (0 <= p2.Y && p2.Y < lines.Length &&
            0 <= p2.X && p2.X < lines[p2.Y].Length)
            return lines[p2.Y][p2.X];
        return null;
    }
    
    

    P2 StartPos(string[] strings)
    {
        P2 p2 = new P2(-1, -1);
        for (int j = 0; j < strings.Count(); j++)
        {
            if (strings[j].Contains("S"))
                p2 = new P2(strings[j].IndexOf("S"), j);
        }

        return p2;
    }
    
    (int, P2) Step(P2 dir, P2 pos)
    {
        var c = lines[pos.Y][pos.X];

        if (c == 'F' && dir == up)
            return (1, right);
        if (c == 'F' && dir == left)
            return (-1, down);
        if (c == 'J' && dir == down)
            return (1, left);
        if (c == 'J' && dir == right)
            return (-1, up);
        if (c == 'L' && dir == down)
            return (-1, right);
        if (c == 'L' && dir == left)
            return (1, up);
        if (c == '7' && dir == right)
            return (1, down);
        if (c == '7' && dir == up)
            return (-1, left);
        if (c == '|' && dir == up)
            return (0, up);
        if (c == '|' && dir == down)
            return (0, down);
        if (c == '-' && dir == left)
            return (0, left);
        if (c == '-' && dir == right)
            return (0, right);
        return (0, noDir);
    }

    char? Next(P2 dir, P2 pos)
    {
        var next = new P2(dir.X + pos.X, dir.Y + pos.Y);
        if (next.X < 0 || lines[0].Length <= next.X ||
            next.Y < 0 || lines.Length <= next.Y)
            return null;
        return lines[next.Y][next.X];
    }

    var longest = 0;
    foreach (P2 cardDir in new List<P2> { up, right, down, left })
    {
        var steps = 1;
        var turnCount = 0;
        var dir = new P2(cardDir.X, cardDir.Y);
        var pos = StartPos(lines);
        var poss = new List<P2>();
        poss.Add(pos);

        while (Next(dir, pos) != null && Next(dir, pos) != 'S')
        {
            var newPos = pos + dir;
            var (turn, newdir) = Step(dir, newPos);
            turnCount += turn;
            if (newdir == noDir)
                break;

            dir = newdir;
            pos = newPos;
            poss.Add(pos);
            steps += 1;
        }

        Console.WriteLine($"Part steps {steps}");
        if (Next(dir, pos) == 'S' && steps / 2 > longest)
        {
            longest = steps / 2;

            var arean = 0;
            var possD = poss.ToDictionary(x => (x.X, x.Y), y => true);
            for (int i = 0; i < lines.Length; i++)
            {
                var isInside = false;
                char? last = null;
                for (int j = 0; j < lines[i].Length-1; j++)
                {
                    var c = GetC(new P2(j, i));
                    var isMain = possD.ContainsKey((j, i));
                    if (isMain)
                    {
                        if (c == '|')
                            isInside = !isInside;
                        if (c == 'F' || c == 'L')
                            last = c;
                        if (c == 'J' || c == '7')
                        {
                            var passedPipe = last == 'F' && c == 'J' || last == 'L' && c == '7';
                            if (passedPipe)
                                isInside = !isInside;
                            last = null;
                        }
                    }
                    else if(isInside)
                    {
                        arean += 1;
                    }
                }
            }
            Console.WriteLine($"Area {arean}");
        }
        
         // foreach (var line in lines)
         // {
         //     Console.WriteLine(line);
         // }
    }

    var area = lines.Sum(x => x.Count(y => y == 'I'));

    Console.WriteLine($"Longest {longest} with area {area}");
}
}




internal class P2
{
    public int X { get; }
    public int Y { get; }

    public P2(int x, int y)
    {
        X = x;
        Y = y;
    }
    
    
    public static bool operator ==(P2 left, P2 right)
    {
        if (left is null || right is null)
            return left is null && right is null;

        return left.X == right.X && left.Y == right.Y;
    }

    public static bool operator !=(P2 left, P2 right)
    {
        return !(left == right);
    }

    public static P2 operator +(P2 left, P2 right)
    {
        return new P2(left.X + right.X, left.Y + right.Y);
    }
}

public static class StringChar
{
    public static char AsC(this string s)
    {
        return s.ToCharArray()[0];
    }
}