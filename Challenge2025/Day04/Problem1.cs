namespace Challenge2025.Day04;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day04", useTest: false);
        ParseInputs(inputs);

        var result = MoveableRolls();

        Console.WriteLine($"Answer: {result}");
    }
}
