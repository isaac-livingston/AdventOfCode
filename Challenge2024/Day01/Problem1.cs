namespace Challenge2024.Day01;

internal class Problem1 : Day01Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day01");
        ParseInputs(inputs);

        Lefts.Sort();
        Rights.Sort();

        int diff = 0;

        for (int i = 0; i < Lefts.Count; i++)
        {
            diff += Math.Abs(Rights[i] - Lefts[i]);
        }

        Console.WriteLine($"Total: {diff:N0}");
    }
}
