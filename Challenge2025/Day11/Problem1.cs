namespace Challenge2025.Day11;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day11", useTest: false);
        ParseInputs(inputs);

        var result = CountPaths("you", "out");

        Console.WriteLine($"Answer: {result}");
    }
}
