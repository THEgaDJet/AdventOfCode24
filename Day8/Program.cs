using System.Runtime.CompilerServices;

//Part1();
Part2();

static void Part1()
{
    var antennaSymbols = new HashSet<char>();
    var antennaLocations = new Dictionary<char, List<(int x, int y)>>();

    // var text = File.ReadAllLines("../../../testinput.txt");
    var text = File.ReadAllLines("../../../input.txt");

    var maxX = text[0].Length;
    var maxY = text.Length;

    FindAntennas(antennaSymbols, antennaLocations, text);
    var antinodeLocations = antennaLocations.Keys
        .Select(x => new KeyValuePair<char, List<(int x, int y)>>(x, []))
        .ToDictionary();

    var uniqueAntinodes = new HashSet<(int, int)>();
    
    CalculateAntinodeLocations();

    Console.WriteLine(uniqueAntinodes.Count);

    // Local functions
    static void FindAntennas(HashSet<char> antennaSymbols, Dictionary<char, List<(int x, int y)>> antennaLocations, string[] text)
    {
        for (int y = 0; y < text.Length; y++)
        {
            var line = text[y].ToCharArray();
            for (int x = 0; x < line.Length; x++)
            {
                var c = line[x];
                if (char.IsLetterOrDigit(c))
                {
                    antennaSymbols.Add(c);
                    if (antennaLocations.TryGetValue(c, out var locations))
                    {
                        locations.Add((x, y));
                    }
                    else
                    {
                        antennaLocations[c] = [(x, y)];
                    }
                }
            }
        }
    }

    void CalculateAntinodeLocations()
    {
        foreach (var antennaLocation in antennaLocations)
        {
            var antenna = antennaLocation.Key;
            var locations = antennaLocation.Value;
            var numAntennas = locations.Count;
            for (int i = 0; i < numAntennas - 1; i++)
            {
                var baseLocation = locations[i];
                for (int j = i + 1; j < numAntennas; j++)
                {
                    var nextLocation = locations[j];
                    var distanceX = baseLocation.x - nextLocation.x;
                    var distanceY = baseLocation.y - nextLocation.y;
                    
                    var antinode1 = (x: nextLocation.x + 2 * distanceX, y: nextLocation.y + 2 * distanceY);
                    var antinode2 = (x: baseLocation.x - 2 * distanceX, y: baseLocation.y - 2 * distanceY);

                    if (IsInBounds(antinode1.x, antinode1.y)) 
                    {
                        antinodeLocations[antenna].Add(antinode1);
                        uniqueAntinodes.Add(antinode1);
                    }
                    if (IsInBounds(antinode2.x, antinode2.y))
                    {
                        antinodeLocations[antenna].Add(antinode2);
                        uniqueAntinodes.Add(antinode2);
                    }
                }
            }
        }
    }

    bool IsInBounds(int x, int y) 
        => x >= 0 && y >= 0
            && x < maxX && y < maxY;
}

static void Part2()
{
    var antennaSymbols = new HashSet<char>();
    var antennaLocations = new Dictionary<char, List<(int x, int y)>>();

    // var text = File.ReadAllLines("../../../testinput.txt");
    // var text = File.ReadAllLines("../../../testinput2.txt");
    var text = File.ReadAllLines("../../../input.txt");

    var maxX = text[0].Length;
    var maxY = text.Length;

    FindAntennas(antennaSymbols, antennaLocations, text);
    var antinodeLocations = antennaLocations.Keys
        .Select(x => new KeyValuePair<char, List<(int x, int y)>>(x, []))
        .ToDictionary();

    var uniqueAntinodes = new HashSet<(int, int)>();
    
    CalculateAntinodeLocations();

    Console.WriteLine(uniqueAntinodes.Count);

    // Local functions
    static void FindAntennas(HashSet<char> antennaSymbols, Dictionary<char, List<(int x, int y)>> antennaLocations, string[] text)
    {
        for (int y = 0; y < text.Length; y++)
        {
            var line = text[y].ToCharArray();
            for (int x = 0; x < line.Length; x++)
            {
                var c = line[x];
                if (char.IsLetterOrDigit(c))
                {
                    antennaSymbols.Add(c);
                    if (antennaLocations.TryGetValue(c, out var locations))
                    {
                        locations.Add((x, y));
                    }
                    else
                    {
                        antennaLocations[c] = [(x, y)];
                    }
                }
            }
        }
    }

    void CalculateAntinodeLocations()
    {
        var multipleLocations = antennaLocations.Where(x => x.Value.Count > 1);
        foreach (var antennaLocation in multipleLocations)
        {
            var antenna = antennaLocation.Key;
            var locations = antennaLocation.Value;
            var numAntennas = locations.Count;
            for (int i = 0; i < numAntennas - 1; i++)
            {
                var baseLocation = locations[i];
                uniqueAntinodes.Add(baseLocation);
                for (int j = i + 1; j < numAntennas; j++)
                {
                    var nextLocation = locations[j];
                    uniqueAntinodes.Add(nextLocation);

                    var distanceX = baseLocation.x - nextLocation.x;
                    var distanceY = baseLocation.y - nextLocation.y;

                    int backwardsCount = 1;
                    var antinode1 = (x: nextLocation.x + backwardsCount * distanceX, y: nextLocation.y + backwardsCount * distanceY);
                    
                    while (IsInBounds(antinode1.x, antinode1.y))
                    {
                        if (!uniqueAntinodes.Contains(antinode1))
                        {
                            antinodeLocations[antenna].Add(antinode1);
                            uniqueAntinodes.Add(antinode1);
                        }
                        backwardsCount += 1;
                        antinode1 = (x: nextLocation.x + backwardsCount * distanceX, y: nextLocation.y + backwardsCount * distanceY);
                    }
                    
                    int forwardsCount = 1;
                    var antinode2 = (x: baseLocation.x - forwardsCount * distanceX, y: baseLocation.y - forwardsCount * distanceY);

                    while (IsInBounds(antinode2.x, antinode2.y))
                    {
                        if (!uniqueAntinodes.Contains(antinode2))
                        {
                            antinodeLocations[antenna].Add(antinode2);
                            uniqueAntinodes.Add(antinode2);
                        }
                        forwardsCount += 1;
                        antinode2 = (x: baseLocation.x - forwardsCount * distanceX, y: baseLocation.y - forwardsCount * distanceY);
                    }
                }
            }
        }
    }

    bool IsInBounds(int x, int y) 
        => x >= 0 && y >= 0
            && x < maxX && y < maxY;
}
