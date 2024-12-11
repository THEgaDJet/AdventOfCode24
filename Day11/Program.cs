Part1();
// Part2();


static void Part1()
{
    var blinks = 25;

    // var text = File.ReadAllText("../../../testinput.txt");
    var text = File.ReadAllText("../../../input.txt");

    var stones = text.Split(' ').Select(ulong.Parse).ToList();
    for (uint i = 0; i < blinks; i++)
    {
        var newStones = new List<ulong>();
        var transformedStones = stones.Select(ApplyRules);
        stones = transformedStones.SelectMany(s => s).ToList();
    }

    Console.WriteLine(stones.Count);

    // Local functions
    static ulong[] ApplyRules(ulong originalValue)
    {
        if (originalValue == 0)
        {
            return [1];
        }
        else if (originalValue.ToString().Length % 2 == 0)
        {
            var digits = originalValue.ToString().Length / 2;
            var left = originalValue.ToString()[..digits];
            var right = originalValue.ToString().Substring(digits, digits);

            return [ulong.Parse(left), ulong.Parse(right)];
        } 

        return [originalValue * 2024];
    }
}

static void Part2()
{
    // var text = File.ReadAllText("testinput.txt");
    var text = File.ReadAllText("testinput2.txt");
    // var text = File.ReadAllText("input.txt");

    Console.WriteLine();

    // Local functions
}
