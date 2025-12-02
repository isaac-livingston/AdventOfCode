namespace Challenge2025.Day02;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day02", useTest: false);
        ParseInputs(inputs, limitCheckToMod2: false);

        var n = InvalidCodes;

        Console.WriteLine($"Answer: {n.Count} instances, sum: {n.Sum()}");
    }
}
