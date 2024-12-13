namespace Challenge2024.Day12;

internal class Problem1 : Day12Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day12", false);
        ParseInputs(inputs);

        var plots = FindPlots();

        Console.WriteLine($"Total price: {plots.Sum(x => x.PerimeterFenceCost)}");
    }
}
