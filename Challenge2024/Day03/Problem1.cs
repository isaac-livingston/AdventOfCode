namespace Challenge2024.Day03;

internal class Problem1 : Day03Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day03", false);
        ParseInputs(inputs);

        LoadInstruction(Program);

        var mulTotal = MemorySets.Select(x => x.Item1 * x.Item2).Sum();

        Console.WriteLine($"Total: {mulTotal:N0}");
    }
}
