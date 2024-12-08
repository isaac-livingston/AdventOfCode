namespace Challenge2024.Day08;

internal class Problem2 : Day08Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day08", false);
        ParseInputs(inputs);

        var interferenceAntinodes = AntennaPairAntinodes.SelectMany(x => x.Value)
                                                        .ToList()
                                                        .Where(x => x.X >= 0 && x.X <= GridBoundsX && x.Y >= 0 && x.Y <= GridBoundsY)
                                                        .ToHashSet();

        var harmonicAntinodes = CalculateResonantHarmonicAntinodes().ToList()
                                                                    .Where(x => x.X >= 0 && x.X <= GridBoundsX && x.Y >= 0 && x.Y <= GridBoundsY)
                                                                    .ToHashSet();

        foreach(var i in interferenceAntinodes)
        {
            Console.WriteLine($"Interference Antinode: {i}");
        }

        foreach (var h in harmonicAntinodes)
        {
            Console.WriteLine($"Harmonic Antinode: {h}");
        }

        Console.WriteLine($"Unique Interference Antinodes in Grid: {interferenceAntinodes.Count}");
        Console.WriteLine($"Unique Harmonic Anitnodes in Grid: {harmonicAntinodes.Count - interferenceAntinodes.Count}");
        Console.WriteLine($"Total Unique Anitnodes in Grid: {harmonicAntinodes.Count}");
    }
}
