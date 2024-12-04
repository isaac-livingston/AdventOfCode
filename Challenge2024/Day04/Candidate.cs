namespace Challenge2024.Day04;

internal record Candidate(int Row, int Col, DirectionFlag Direction)
{
    public Dictionary<char, (int, int)> History { get; private set; } = [];

    //public Candidate(int Row, int Col, DirectionFlag Direction, Dictionary<char, (int, int)>? initialHistory = null) : this(Row, Col, Direction)
    //{
    //    History = initialHistory ?? [];
    //}

    public Candidate(char key, (int, int) value, DirectionFlag Direction) : this(value.Item1, value.Item2, Direction)
    {
        History[key] = value;
    }

    public Candidate Update(char key, (int, int) value)
    {
        History[key] = value;
        return this with { Row = value.Item1, Col = value.Item2 };
    }

    public override string ToString() => $"Row: {Row}, Col: {Col}, Direction: {Direction}, History: {string.Join("", History.Keys)}";
}
