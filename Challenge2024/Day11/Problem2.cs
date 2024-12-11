namespace Challenge2024.Day11;

internal class Problem2 : Day11Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day11", false);
        ParseInputs(inputs);

        var iterations = 75;

        for (int i = 0; i < iterations; i++)
        {
            ProcessStones();
        }

        Console.WriteLine($"After {iterations} iterations, there are {StoneCounts.Sum(x => (decimal)x.Value)} stones.");
    }
}
