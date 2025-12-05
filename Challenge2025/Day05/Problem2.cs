namespace Challenge2025.Day05;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day05", useTest: false);
        ParseInputs(inputs);

        var result = CountAllFreshProductCodes();

        Console.WriteLine($"Answer: {result}");
    }
}
