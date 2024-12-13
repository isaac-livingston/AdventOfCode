namespace Challenge2024.Day12;

internal class Problem2 : Day12Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day12", false);
        ParseInputs(inputs);

        var plots = FindPlots();

        foreach(var p in plots)
        {
            Console.WriteLine(p);
        }

        Console.WriteLine($"Total side fence price: {plots.Sum(x => x.SidesFenceCost)}");
        Console.WriteLine($"Total perimeter fence price: {plots.Sum(x => x.PerimeterFenceCost)}");
    }
}
