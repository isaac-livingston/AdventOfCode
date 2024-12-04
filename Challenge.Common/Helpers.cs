namespace Challenge.Common;

public static class Helpers
{
    public static Dictionary<DirectionFlag, (int dx, int dy)> DirectionOffsets => GenerateDirectionOffsets();

    private static Dictionary<DirectionFlag, (int dx, int dy)> GenerateDirectionOffsets()
    {
        var baseOffsets = new Dictionary<DirectionFlag, (int dx, int dy)>
        {
            { DirectionFlag.None, (0, 0) },
            { DirectionFlag.Up, (-1, 0) },
            { DirectionFlag.Down, (1, 0) },
            { DirectionFlag.Left, (0, -1) },
            { DirectionFlag.Right, (0, 1) }
        };

        var compositeOffsets = new Dictionary<DirectionFlag, (int dx, int dy)>();

        foreach (var flag in Enum.GetValues<DirectionFlag>())
        {
            if (flag == DirectionFlag.None || baseOffsets.ContainsKey(flag))
            {
                continue;
            }

            var dx = 0;
            var dy = 0;

            foreach (var baseFlag in baseOffsets.Keys)
            {
                if (flag.HasFlag(baseFlag))
                {
                    dx += baseOffsets[baseFlag].dx;
                    dy += baseOffsets[baseFlag].dy;
                }
            }

            compositeOffsets[flag] = (dx, dy);
        }

        return baseOffsets.Concat(compositeOffsets)
                          .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }
}
