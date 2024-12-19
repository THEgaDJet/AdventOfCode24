Part1();
//Part2();

static void Part1()
{
    var guard = '^';
    var obstacle = '#';

    //var text = File.ReadAllLines("../../../testinput.txt");
    var text = File.ReadAllLines("../../../input.txt");
    var map = text.Select(x => x.ToCharArray()).ToArray();

    var xMax = text[0].Length - 1;
    var yMax = text.Length - 1;

    var position = FindStartCoords(text);
    var direction = Direction.Up;

    var isInMap = IsInMap(position);

    var visited = new HashSet<(int, int)>
    {
        position
    };

    do
    {
        var nextPosition = NextPosition(direction, position);
        isInMap = IsInMap(nextPosition);

        if (isInMap)
        {
            if (IsObstacle(nextPosition))
            {
                direction = NextDirection(direction);
            }
            else
            {
                position = nextPosition;
                visited.Add(position);
            }
        }
    } while (isInMap);

    Console.WriteLine(visited.Count);

    (int x, int y) FindStartCoords(string[] text)
    {
        var x = -1;
        var y = 0;

        while (x == -1) 
        {
            var row = text[y];
            x = row.IndexOf(guard);
            y++;
        }

        return (x, y - 1);
    }    

    static Direction NextDirection(Direction direction) => direction switch
    {
        Direction.Up => Direction.Right,
        Direction.Right => Direction.Down,
        Direction.Down => Direction.Left,
        Direction.Left => Direction.Up,
        _ => throw new ArgumentOutOfRangeException(),
    };

    static (int x, int y) NextPosition(Direction direction, (int x, int y) position)
    {
        return direction switch
        {
            Direction.Up => (position.x, --position.y),
            Direction.Down => (position.x, ++position.y),
            Direction.Left => (--position.x, position.y),
            Direction.Right => (++position.x, position.y),
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    bool IsInMap((int x, int y) position) 
        => position.x >= 0 && position.x <= xMax
            && position.y >= 0 && position.y <= yMax;
    
    bool IsObstacle((int x, int y) position) => map[position.y][position.x] == obstacle;
}

// static void Part2()
// {
//     // var text = File.ReadAllText("testinput2.txt");
//     var text = File.ReadAllText("input.txt");

//     var shouldAdd = true;

//     var regex = new Regex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don't\(\)");
//     var regex2 = new Regex(@"\d+");

//     var multiplications = 0;
//     var matches = regex.Matches(text);

//     foreach (Match match in matches)
//     {
//         switch (match.Value)
//         {
//             case "do()":
//                 shouldAdd = true;
//                 continue;
//             case "don't()":
//                 shouldAdd = false;
//                 continue;
//             default:
//                 var r = regex2.Matches(match.Value);
//                 var mult = shouldAdd ? int.Parse(r[0].Value) * int.Parse(r[1].Value) : 0;
//                 multiplications += mult;
//                 break;
//         }
//     }

//     Console.WriteLine(multiplications);
// }

enum Direction
{
    Up,
    Right,
    Down,
    Left
}
