namespace Challenge2024.Day10;

internal record Step(int Row, int Col, int Number)
{
    public override string ToString() => $"({Row}, {Col})";
}
