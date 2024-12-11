namespace Challenge2024.Day11;

internal class Problem1 : Day11Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day11", false);
        ParseInputs(inputs);

        PrintStones();

        var iterations = 25;

        for (int i = 0; i < iterations; i++)
        {
            ProcessStones();
            //PrintStones();
        }

        Console.WriteLine($"After {iterations} iterations, there are {Stones.Count} stones.");
    }
}
