global using LanguageExt;
global using LanguageExt.Common;
global using static LanguageExt.Prelude;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

Part1();
Part2();

static void Part1()
{
    // var lines = File.ReadAllLines("testinput.txt");
    var lines = File.ReadAllLines("input.txt");

    var aggregate = ConvertLinesToLists(lines)
        .Map((Left, Right) => ZipLists(Left, Right))
        .Sum();

    Console.WriteLine(aggregate);

    // Local functions
    static (List<int> Left, List<int> Right) ConvertLinesToLists(string[] lines)
        => lines
            .Aggregate((Left: new List<int>(), Right: new List<int>()), PopulateLists);

    static (List<int> Left, List<int> Right) PopulateLists((List<int> Left, List<int> Right) current, string line)
    {
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        current.Left.Add(int.Parse(split[0]));
        current.Right.Add(int.Parse(split[1]));

        return (current.Left, current.Right);
    }

    static IEnumerable<int> ZipLists(List<int> left, List<int> right) =>
        left.Order()
        .Zip(right.Order(), SumLists);
    
    static int SumLists(int left, int right) => Math.Abs(left - right);
}

static void Part2()
{
    // var lines = File.ReadAllLines("testinput.txt");
    var lines = File.ReadAllLines("input.txt");

    var similarity = ConvertLinesToLists(lines)
        .Map((Left, Right) => Left.Aggregate(0, (current, left) => 
        current + left * (Right.TryGetValue(left, out var count) ? count : 0)));

    Console.WriteLine(similarity);

    // Local functions
    static (List<int> Left, Dictionary<int, int> Right) ConvertLinesToLists(string[] lines)
        => lines
            .Aggregate((Left: new List<int>(), Right: new Dictionary<int, int>()), PopulateCollections);

    static (List<int> Left, Dictionary<int, int> Right) PopulateCollections((List<int> Left, Dictionary<int, int> Right) current, string line)
    {
        var split = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        current.Left.Add(int.Parse(split[0]));
        var right = int.Parse(split[1]);
        current.Right[right] = current.Right.TryGetValue(right, out var val) ? val + 1 : 1;

        return (current.Left, current.Right);
    }
}