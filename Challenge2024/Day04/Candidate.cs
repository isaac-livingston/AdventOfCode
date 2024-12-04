namespace Challenge2024.Day04;

internal record Candidate(int Row, int Col, DirectionFlag Direction, Dictionary<char, (int, int)> History)
{
    public override string ToString() => $"Row: {Row}, Col: {Col}, Direction: {Direction}, History: {string.Join("", History.Keys)}";
}
