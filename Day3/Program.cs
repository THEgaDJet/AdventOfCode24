using System.Text.RegularExpressions;

//Part1();
Part2();

static void Part1()
{
    // var text = File.ReadAllText("testinput.txt");
    var text = File.ReadAllText("input.txt");

    var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)");
    var regex2 = new Regex(@"\d+");

    var matches = regex.Matches(text);
    var multiplications = matches.Aggregate(0, (current, match) => 
        {
            var r = regex2.Matches(match.Value);
            return current + int.Parse(r[0].Value) * int.Parse(r[1].Value);
        });

    Console.WriteLine(multiplications);

    // Local functions
    
}

static void Part2()
{
    // var text = File.ReadAllText("testinput2.txt");
    var text = File.ReadAllText("input.txt");

    var shouldAdd = true;

    var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)");
    var regex2 = new Regex(@"\d+");

    var multiplications = 0;
    var matches = regex.Matches(text);

    foreach (Match match in matches)
    {
        switch (match.Value)
        {
            case "do()":
                shouldAdd = true;
                continue;
            case "don't()":
                shouldAdd = false;
                continue;
            default:
                var r = regex2.Matches(match.Value);
                var mult = shouldAdd ? int.Parse(r[0].Value) * int.Parse(r[1].Value) : 0;
                multiplications += mult;
                break;
        }
    }

    Console.WriteLine(multiplications);
}
