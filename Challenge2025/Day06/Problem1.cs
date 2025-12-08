namespace Challenge2025.Day06;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day06", useTest: false);
        ParseInputs(inputs);

        long result = CalculateTrimmedGridValue();

        Console.WriteLine($"Answer: {result}");
    }
}
