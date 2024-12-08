namespace Challenge2024.Day08;

internal class Problem1 : Day08Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day08", false);
        ParseInputs(inputs);

        var uniqueGridBoundAntinodes = AntennaPairAntinodes.SelectMany(x => x.Value)
                                                           .ToList()
                                                           .Where(x => x.X >= 0 && x.X <= GridBoundsX && x.Y >= 0 && x.Y <= GridBoundsY)
                                                           .ToHashSet();

        Console.WriteLine($"Unique Antinodes in Grid: {uniqueGridBoundAntinodes.Count}");
    }
}
