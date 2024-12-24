Part1();
// Part2();

static void Part1()
{
    // var text = File.ReadAllText("../../../testinput.txt");
    var text = File.ReadAllText("../../../input.txt");

    var parts = text.Split(Environment.NewLine + Environment.NewLine);
    var pageOrderingRules = parts[0].Split(Environment.NewLine)
        .Select(rule => Array.ConvertAll(rule.Split('|'), int.Parse))
        .ToList();
    var updates = parts[1].Split(Environment.NewLine)
        .Select(u => Array.ConvertAll(u.Split(','), int.Parse))
        .ToList();

    var pageOrders = new Dictionary<int, HashSet<int>>();

    foreach (var pageOrder in pageOrderingRules)
    {
        var first = pageOrder[0];
        var second = pageOrder[1];

        if (pageOrders.TryGetValue(first, out var seconds))
        {
            seconds.Add(second);
        }
        else
        {
            pageOrders[first] = [second];
        }
    }

    var validUpdates = new List<int[]>();

    foreach (var update in updates)
    {
        var isValid = true;
        var prevPages = new HashSet<int>();
        for (int i = 1; i < update.Length; i++)
        {
            var currentPage = update[i];
            prevPages.Add(update[i - 1]);

            if (!pageOrders.TryGetValue(currentPage, out var subsequentPages))
            {
                continue;
            }

            if (subsequentPages.Intersect(prevPages).Any())
            {
                isValid = false;
                break;
            }
        }

        if (isValid)
        {
            validUpdates.Add(update);
        }
    }

    var sum = validUpdates.Sum(validUpdate => 
        {
            var middle = validUpdate.Length / 2;
            return validUpdate[middle];
        });

    Console.WriteLine(sum);

    // Local functions
    
}

static void Part2()
{
    var text = File.ReadAllText("../../../testinput.txt");
    // var text = File.ReadAllText("input.txt");

    Console.WriteLine();

    // Local functions
    
}