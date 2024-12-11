
Part1();
// Part2();

static void Part1()
{
    var files = new List<int>();
    var freeSpace = new List<int>();

    //var text = File. ReadAllText("testinput.txt");
    using (StreamReader sr = new("../../../testinput.txt"))
    {
        var isFile = true;
        while (sr.Peek() >= 0)
        {
            var c = (char)sr.Read();
            var val = (int)char.GetNumericValue(c);

            if (isFile)
            {
                files.Add(val); 
            }
            else
            { 
                freeSpace.Add(val); 
            }
            isFile = !isFile;
        }

        Console.WriteLine(files.Count);
        Console.WriteLine(files.Sum());

        Console.WriteLine();

        Console.WriteLine(freeSpace.Count);
        Console.WriteLine(freeSpace.Sum());
    }

    // var text = File.ReadAllText("input.txt");

    Console.WriteLine();

    // Local functions
    
}

static void Part2()
{
    // var text = File.ReadAllText("testinput.txt");
    var text = File.ReadAllText("testinput2.txt");
    // var text = File.ReadAllText("input.txt");

    Console.WriteLine();

    // Local functions
    
}
