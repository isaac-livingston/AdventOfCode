using static Challenge2024.Day07.Operators;

namespace Challenge2024.Day07;

internal class Problem2 : Day07Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day07", false);
        ParseInputs(inputs);

        EvaluateProblems(ProblemSet, [Add, Multiply, Concatinate]);

        var sumValidResults = ValidResults.Sum(x => x.Key.target);

        Console.WriteLine($"Count of Valid Results: {ValidResults.Count}");
        Console.WriteLine($"Sum of Valid Results: {sumValidResults}");
    }
}
