namespace Challenge2024.Day04;

internal class Problem2 : Day04Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day04", false);
        ParseInputs(inputs);

        InitializeCandidates('M', 'A');

        foreach (var letter in "S")
        {
            var candidates = Candidates.Select(candidate => FindNextInSequence(candidate, letter))
                                       .Where(x => x != null)
                                       .ToList();

            UpdateCandidates(candidates!);
        }

        var diagonalMAS = Candidates.Where(x => x.Direction == DirectionFlag.UpLeft
                                             || x.Direction == DirectionFlag.UpRight
                                             || x.Direction == DirectionFlag.DownLeft
                                             || x.Direction == DirectionFlag.DownRight)
                                    .ToList();

        var crossedMAS = diagonalMAS.GroupBy(x => x.History['A'])
                                    .Where(x => x.Count() > 1)
                                    .ToList();

        Console.WriteLine($"Total: {crossedMAS.Count:N0}");
    }
}
