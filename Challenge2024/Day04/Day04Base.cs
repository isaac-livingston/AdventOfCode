namespace Challenge2024.Day04;

internal class Day04Base : ProblemBase
{
    public char[][] Crossword { get; private set; } = [];

    public List<Candidate> Candidates { get; } = [];

    public static Dictionary<DirectionFlag, (int dx, int dy)> DirectionOffsets => new()
    {
        { DirectionFlag.None, (0, 0) },
        { DirectionFlag.Up, (-1, 0) },
        { DirectionFlag.Down, (1, 0) },
        { DirectionFlag.Left, (0, -1) },
        { DirectionFlag.Right, (0, 1) },
        { DirectionFlag.Up | DirectionFlag.Left, (-1, -1) },
        { DirectionFlag.Up | DirectionFlag.Right, (-1, 1) },
        { DirectionFlag.Down | DirectionFlag.Left, (1, -1) },
        { DirectionFlag.Down | DirectionFlag.Right, (1, 1) },
    };

    public void ParseInputs(string[] inputs)
    {
        Crossword = inputs.Select(x => x.ToCharArray())
                          .ToArray();
    }

    public void InitializeCandidates(char term, char term2, DirectionFlag direction)
    {
        var candidates = new List<Candidate>();

        for (int x = 0; x < Crossword.Length; x++)
        {
            for (int y = 0; y < Crossword[x].Length; y++)
            {
                if (Crossword[x][y] == term)
                {
                    var directionsToCheck = direction == DirectionFlag.None
                                          ? DirectionOffsets.Keys.ToArray()
                                          : [direction];

                    foreach (var dir in directionsToCheck)
                    {
                        var (dx, dy) = DirectionOffsets[dir];
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
        }

        UpdateCandidates(candidates);
    }

    public void UpdateCandidates(List<Candidate> candidates)
    {
        Candidates.Clear();
        Candidates.AddRange(candidates);
    }

    public Candidate? FindNextInSequence(Candidate startCandidate, char nextTerm)
    {
        var (dx, dy) = DirectionOffsets[startCandidate.Direction];
        int nx = startCandidate.Row + dx;
        int ny = startCandidate.Col + dy;

        if (IsValidPosition(nx, ny) && Crossword[nx][ny] == nextTerm)
        {
            var history = startCandidate.history;
            history.Add(nextTerm, (nx, ny));

            return new Candidate(nx, ny, startCandidate.Direction, history);
        }

        return null;
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

internal record Candidate(int Row, int Col, DirectionFlag Direction, Dictionary<char,(int,int)> history)
{
    public override string ToString() => $"Row: {Row}, Col: {Col}, Direction: {Direction}, History: {string.Join("", history.Keys)}";
}

[Flags]
internal enum DirectionFlag : int
{
    None = 0,
    Up =  1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3
}