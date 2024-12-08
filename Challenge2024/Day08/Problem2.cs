namespace Challenge2024.Day08;

internal class Problem2 : Day08Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day08", false);
        ParseInputs(inputs);

        var harmonicAntinodes = CalculateHarmonicAntinodes().Where(x => x.X >= 0 && x.X <= GridBoundsX && x.Y >= 0 && x.Y <= GridBoundsY)
                                                            .ToHashSet();

        Console.WriteLine($"Total Unique Anitnodes in Grid: {harmonicAntinodes.Count}");
    }
}
