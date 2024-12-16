namespace Challenge2024.Day15;

internal record Obstruction
{ 
    public Obstruction(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public int Row { get; protected set; }

    public int Column { get; protected set; }
}

internal record Box(int Row, int Column) : Obstruction(Row, Column)
{
    public long GPSCoordinate => 100 * Row + Column;

    public void Slide(char direction)
    {
        var (rowDelta, colDelta) = GridDirections.GetDirection(direction);
        Row += rowDelta;
        Column += colDelta;
    }
}

internal record Wall(int Row, int Column) : Obstruction(Row, Column);
