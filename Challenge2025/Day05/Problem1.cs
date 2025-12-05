namespace Challenge2025.Day05;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day05", useTest: false);
        ParseInputs(inputs);

        var result = CountFreshCodes();

        Console.WriteLine($"Answer: {result}");
    }
}
