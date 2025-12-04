namespace Challenge2025.Day04;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day04", useTest: false);
        ParseInputs(inputs);

        var result = RemoveRolls();

        Console.WriteLine($"Answer: {result}");
    }
}
