namespace Challenge2024.Day07;

internal class Problem1 : Day07Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day07", false);
        ParseInputs(inputs);

        EvaluateProblems();

        var validResults = ValidResultCounts.Where(x => x.Value > 0)
                                            .ToList();

        var sumValidRsults = validResults.Sum(x => x.Key.target);
        Console.WriteLine($"Sum of Valid Results: {sumValidRsults}");
    }
}
