namespace Challenge2024.Day07;

internal class Problem2 : Day07Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day07", false);
        ParseInputs(inputs);

        EvaluateProblems(ProblemSet, evalConcat: false);

        var valids = ValidResults.Select(x => x.Key)
                                 .ToList();

        var invalidProblemSet = ProblemSet.Except(valids)
                                          .ToList();

        EvaluateProblems(invalidProblemSet, evalConcat: true);

        var sumValidRsults = ValidResults.Sum(x => x.Key.target);

        //foreach(var ((target,terms), result) in ValidResults)
        //{
        //    Console.WriteLine($"Valid Result: {target}: {string.Join(" ", terms)} => {result}");
        //}
        Console.WriteLine("");
        Console.WriteLine($"Count of Valid Results: {ValidResults.Count}");
        Console.WriteLine($"Sum of Valid Results: {sumValidRsults}");
    }
}
