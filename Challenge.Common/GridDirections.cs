namespace Challenge.Common;

public static class GridDirections
{
    private static readonly Dictionary<char, (int r1, int c1)> _directions = new()
    {
        { '^', (-1, 0) },
        { '>', (0, 1) },
        { 'v', (1, 0) },
        { '<', (0, -1) }
    };

    private static readonly Dictionary<DirectionFlag, (int r1, int c1)> _flagDirections = new()
    {
        { DirectionFlag.Up, (-1, 0) },
        { DirectionFlag.Down, (1, 0) },
        { DirectionFlag.Left, (0, -1) },
        { DirectionFlag.Right, (0, 1) },
        { DirectionFlag.UpLeft, (-1, -1) },
        { DirectionFlag.UpRight, (-1, 1) },
        { DirectionFlag.DownLeft, (1, -1) },
        { DirectionFlag.DownRight, (1, 1) }
    };

    public static (int r1, int c1) GetDirection(char symbol)
    {
        if (!_directions.TryGetValue(symbol, out var direction))
        {
            throw new ArgumentException($"Invalid direction symbol: {symbol}");
        }

        return direction;
    }

    public static Dictionary<char, (int r1, int c1)> GetAllDirections()
    {
        return new Dictionary<char, (int r1, int c1)>(_directions);
    }

    public static (int r1, int c1) GetDirection(DirectionFlag flag)
    {
        if (!_flagDirections.TryGetValue(flag, out var direction))
        {
            throw new ArgumentException($"Invalid DirectionFlag: {flag}");
        }

        return direction;
    }

    public static Dictionary<DirectionFlag, (int r1, int c1)> GetAllFlagDirections()
    {
        return new Dictionary<DirectionFlag, (int r1, int c1)>(_flagDirections);
    }
}
