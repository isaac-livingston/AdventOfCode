namespace Challenge2025.Day06;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day06", useTest: false);
        ParseInputs(inputs);

        long result = CalculateDenseGridValue();

        Console.WriteLine($"Answer: {result}");
    }
}
