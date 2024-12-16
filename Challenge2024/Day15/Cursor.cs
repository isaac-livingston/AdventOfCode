namespace Challenge2024.Day15;

internal record Cursor(int Row, int Column)
{
    public int Row { get; private set; } = Row;

    public int Column { get; private set; } = Column;

    public void Step(char direction)
    {
        var (r1, c1) = GridDirections.GetDirection(direction);
        Row += r1;
        Column += c1;
    }
}
