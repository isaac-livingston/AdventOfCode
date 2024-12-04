namespace Challenge2024.Day04;

internal class Problem1 : Day04Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day04", false);
        ParseInputs(inputs);

        InitializeCandidates('X', 'M', DirectionFlag.None);

        foreach (var letter in "AS")
        {
            var candidates = Candidates.Select(candidate => FindNextInSequence(candidate, letter))
                                   .Where(x => x != null)
                                   .ToList();

            UpdateCandidates(candidates!);
        }

        Console.WriteLine($"Total: {Candidates.Count:N0}");
    }
}
