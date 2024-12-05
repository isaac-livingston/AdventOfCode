namespace Challenge2024.Day04;

internal class Day04Base : ProblemBase
{
    public char[][] Crossword { get; private set; } = [];

    public List<Candidate> Candidates { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        Crossword = inputs.Select(x => x.ToCharArray())
                          .ToArray();
    }

    public void InitializeCandidates(char term, char term2)
    {
        var candidates = new List<Candidate>();

        for (int x = 0; x < Crossword.Length; x++)
        {
            for (int y = 0; y < Crossword[x].Length; y++)
            {
                if (Crossword[x][y] != term) continue;

                foreach (var (dir, (dx, dy)) in DirectionOffsets)
                {
                    int nx = x + dx;
                    int ny = y + dy;

                    if (IsValidPosition(nx, ny) && Crossword[nx][ny] == term2)
                    {
                        candidates.Add(new Candidate(term2, (nx, ny), dir));
                    }
                }
            }
        }

        UpdateCandidates(candidates);
    }

    public Candidate? FindNextInSequence(Candidate candidate, char nextTerm)
    {
        var (dx, dy) = DirectionOffsets[candidate.Direction];
        int nx = candidate.Row + dx;
        int ny = candidate.Col + dy;

        if (IsValidPosition(nx, ny) && Crossword[nx][ny] == nextTerm)
        {
            var nextCandidate = candidate.Update(nextTerm, (nx, ny));

            return nextCandidate;
        }

        return null;
    }

    public void UpdateCandidates(List<Candidate> candidates)
    {
        Candidates.Clear();
        Candidates.AddRange(candidates);
    }

    private bool IsValidPosition(int x, int y)
    {
        return x >= 0 && x < Crossword.Length && y >= 0 && y < Crossword[0].Length;
    }

    public override void RunSolution()
    {
        throw new NotImplementedException();
    }
}