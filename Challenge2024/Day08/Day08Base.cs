namespace Challenge2024.Day08;

internal class Day08Base : ProblemBase
{
    public Dictionary<(Antenna, Antenna), List<Antinode>> AntennaPairAntinodes { get; } = [];

    public int GridBoundsY { get; private set; } = 0;
    public int GridBoundsX { get; private set; } = 0;

    public void ParseInputs(string[] inputs)
    {
        GridBoundsY = inputs.Length - 1;
        GridBoundsX = inputs[0].Length - 1;

        var antennas = new List<Antenna>();

        for (int row = 0; row < inputs.Length; row++)
        {
            for (int col = 0; col < inputs[row].Length; col++)
            {
                var frequency = inputs[row][col];

                if (frequency != '.')
                {
                    antennas.Add(new Antenna(frequency.ToString(), col, row));
                }
            }
        }

        var frequencyAntennas = antennas.GroupBy(a => a.Frequency).ToDictionary(g => g.Key, g => g.ToList());

        foreach (var antenna in antennas)
        {
            foreach (var otherAntenna in antennas.Where(x=>x.Frequency == antenna.Frequency))
            {
                if (antenna == otherAntenna)
                {
                    continue;
                }
                var antinodes = CalculateInterferenceAntinodes(antenna.X, antenna.Y, otherAntenna.X, otherAntenna.Y);
                AntennaPairAntinodes.Add((antenna, otherAntenna), antinodes);
            }
        }
    }

    public static List<Antinode> CalculateInterferenceAntinodes(int x1, int y1, int x2, int y2)
    {
        var antinodes = new List<Antinode>();

        int dx = x2 - x1;
        int dy = y2 - y1;

        antinodes.Add(new Antinode(x1 - dx, y1 - dy));
        antinodes.Add(new Antinode(x2 + dx, y2 + dy));

        return antinodes;
    }

    public List<Antinode> CalculateHarmonicAntinodes()
    {
        var allAntinodes = new HashSet<Antinode>();

        var xx = AntennaPairAntinodes.GroupBy(x=>x.Key.Item1.Frequency);

        foreach(var c in xx)
        {
            var g = c.Select(x => x.Key).ToList();

            foreach (var pair in g)
            {
                var (antenna1, antenna2) = pair;

                foreach (var (x, y) in GetEchoAntinodes(antenna1.X, antenna1.Y, antenna2.X, antenna2.Y))
                {
                    allAntinodes.Add(new Antinode(x, y));
                }

                allAntinodes.Add(new Antinode(antenna1.X, antenna1.Y));
                allAntinodes.Add(new Antinode(antenna2.X, antenna2.Y));
            }
        }

        return [.. allAntinodes];
    }

    private List<(int x, int y)> GetEchoAntinodes(int x1, int y1, int x2, int y2)
    {
        var points = new List<(int x, int y)>();

        int dx = x2 - x1;
        int dy = y2 - y1;

        int x = x2;
        int y = y2;

        while (true)
        {
            x += dx;
            y += dy;

            if (x < 0 || x > GridBoundsX || y < 0 || y > GridBoundsY)
            {
                break;
            }

            points.Add((x, y));
        }

        return points;
    }
}
