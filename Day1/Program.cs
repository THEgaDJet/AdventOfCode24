// Part1();
Part2();


void Part1()
{
    var listLeft = new List<int>();
    var listRight = new List<int>();

    // var lines = File.ReadAllLines("testinput.txt");
    var lines = File.ReadAllLines("input.txt");
    var length = lines.Length;

    foreach (var line in lines)
    {
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        listLeft.Add(int.Parse(split[0]));
        listRight.Add(int.Parse(split[1]));
    }

    listLeft.Sort();
    listRight.Sort();

    var aggregate = 0;

    for (int i = 0; i < length; i++)
    {
        aggregate += Math.Abs(listLeft[i] - listRight[i]);
    }

    Console.WriteLine(aggregate);
}

static void Part2()
{
    // var lines = File.ReadAllLines("testinput.txt");
    var lines = File.ReadAllLines("input.txt");
    
    var listLeft = new List<int>();
    var dictRight = new Dictionary<int, int>();

    foreach (var line in lines)
    {
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        listLeft.Add(int.Parse(split[0]));
        var right = int.Parse(split[1]);
        dictRight[right] = dictRight.TryGetValue(right, out var val) ? val + 1 : 1;
    }

    var aggregate = 0;

    for (int i = 0; i < lines.Length; i++)
    {
        var left = listLeft[i];
        aggregate += left * (dictRight.TryGetValue(left, out var count) ? count : 0);
    }

    Console.WriteLine(aggregate);
}