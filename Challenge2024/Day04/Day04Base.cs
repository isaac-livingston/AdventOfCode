using static Challenge.Common.Helpers;
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
                        var history = new Dictionary<char, (int, int)>
                            {
                                { term2, (nx, ny) }
                            };

                        candidates.Add(new Candidate(nx, ny, dir, history));
                    }
                }
            }
        }

        UpdateCandidates(candidates);
    }

    public Candidate? FindNextInSequence(Candidate startCandidate, char nextTerm)
    {
        var (dx, dy) = DirectionOffsets[startCandidate.Direction];
        int nx = startCandidate.Row + dx;
        int ny = startCandidate.Col + dy;

        if (IsValidPosition(nx, ny) && Crossword[nx][ny] == nextTerm)
        {
            var history = startCandidate.History;
            history.Add(nextTerm, (nx, ny));

            return new Candidate(nx, ny, startCandidate.Direction, history);
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