namespace Challenge2025.Day07;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day07", useTest: false);
        ParseInputs(inputs);

        var finalBeams = SimulateBeam();

        Console.WriteLine($"Total splits: {TotalSplits}");
        Console.WriteLine($"Final beam count: {finalBeams.Count}");

        long result = TotalSplits;

        Console.WriteLine($"Answer: {result}");
    }
}
