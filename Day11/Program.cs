// Part1();
Part2();


static void Part1()
{
    var blinks = 25;

    // var text = File.ReadAllText("../../../testinput.txt");
    var text = File.ReadAllText("../../../input.txt");

    var stones = text.Split(' ').Select(ulong.Parse).ToList();
    for (uint i = 0; i < blinks; i++)
    {
        var transformedStones = stones.Select(ApplyRules);
        stones = transformedStones.SelectMany(s => s).ToList();
    }

    Console.WriteLine(stones.Count);

    // Local functions
}

static void Part2()
{
    var blinks = 75;
    // Probably need to batch.
    // Given it is simultaneous, apply the transforms to one stone at a time and sum the resulting lengths

    var text = File.ReadAllText("../../../input.txt");
    var stones = text.Split(' ').Select(ulong.Parse).ToList();

    var sum = 0;
    for (int i = 0; i < stones.Count; i++)
    {
        var stone = stones[i];
        var newStones = new List<ulong>() {stone};
        for (int j = 0; j < blinks; j++)
        {
            var transformedStones = newStones.Select(ApplyRules);
            newStones = transformedStones.SelectMany(s => s).ToList();
        }

        sum += newStones.Count;
    }

    Console.WriteLine(sum);
}

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