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
                var antinodes = CalculateGridAntinodes(antenna.X, antenna.Y, otherAntenna.X, otherAntenna.Y);
                AntennaPairAntinodes.Add((antenna, otherAntenna), antinodes);
            }
        }
    }

    public static List<Antinode> CalculateGridAntinodes(int x1, int y1, int x2, int y2)
    {
        var antinodes = new List<Antinode>();

        int dx = x2 - x1;
        int dy = y2 - y1;

        antinodes.Add(new Antinode(x1 - dx, y1 - dy));
        antinodes.Add(new Antinode(x2 + dx, y2 + dy));

        return antinodes;
    }
}

internal record Antenna(string Frequency, int X, int Y)
{
    public override string ToString() => $"{Frequency} ({X}, {Y})";
}

internal record Antinode(int X, int Y);
