Part1();
// Part2();

static void Part1()
{
    // var lines = File.ReadAllLines("testinput.txt");
    var lines = File.ReadAllLines("input.txt");

    var reports = lines
        .Select(x => 
            Array.ConvertAll(x.Split(' '), num => int.Parse(num)));

    var safeReports = reports.Aggregate(0, (current, report) => 
        {
            if (IsSame(report[0], report[1]) 
                || !IsSafeDifference(report[0], report[1]))
            {
                return current;
            }

            if (IsDecreasing(report[0], report[1]))
            {
                if (!IsOk(report, IsDecreasing))
                {
                    return current;
                }
            }

            if (IsIncreasing(report[0], report[1]))
            {
                if (!IsOk(report, IsIncreasing))
                {
                    return current;
                }
            }

            return current + 1;
        });

    Console.WriteLine(safeReports);

    // Local functions
    bool IsSame(int left, int right) => left == right;

    bool IsIncreasing(int left, int right) => left - right < 0;
    
    bool IsDecreasing(int left, int right) => left - right > 0;

    bool IsSafeDifference(int left, int right) => Math.Abs(left - right) >= 1 && Math.Abs(left - right) <= 3;

    bool IsOk(int[] report, Func<int, int, bool> validate)
    {
        for (int i = 0; i < report.Length - 1; i++)
        {
            if (!IsSafeDifference(report[i], report[i + 1])) 
            {
                return false;
            }

            if (!validate(report[i], report[i + 1]))
            {
                return false;
            }
        } 

        return true;
    }
}

// static void Part2()
// {
//     // var lines = File.ReadAllLines("testinput.txt");
//     var lines = File.ReadAllLines("input.txt");

//     var similarity = ConvertLinesToLists(lines)
//         .Map((Left, Right) => Left.Aggregate(0, (current, left) => 
//         current + left * (Right.TryGetValue(left, out var count) ? count : 0)));

//     Console.WriteLine(similarity);

//     // Local functions
//     static (List<int> Left, Dictionary<int, int> Right) ConvertLinesToLists(string[] lines)
//         => lines
//             .Aggregate((Left: new List<int>(), Right: new Dictionary<int, int>()), PopulateCollections);

//     static (List<int> Left, Dictionary<int, int> Right) PopulateCollections((List<int> Left, Dictionary<int, int> Right) current, string line)
//     {
//         var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
//         current.Left.Add(int.Parse(split[0]));
//         var right = int.Parse(split[1]);
//         current.Right[right] = current.Right.TryGetValue(right, out var val) ? val + 1 : 1;

//         return (current.Left, current.Right);
//     }
// }
