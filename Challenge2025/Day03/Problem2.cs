namespace Challenge2025.Day03;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day03", useTest: false);
        ParseInputs(inputs, greatScott: true);

        var joltage = PowerBankJoltages.Sum();

        Console.WriteLine($"Answer: {joltage}");
    }
}
