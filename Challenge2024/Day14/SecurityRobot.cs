namespace Challenge2024.Day14;

internal record SecurityRobot
{
    private double _moves = 0;

    public Point Position { get; private set; }

    public Vector2 Direction { get; init; }

    public static int MaxRow { get; private set; }
    public static int MaxColumn { get; private set; }

    public SecurityRobot(Point position, Vector2 direction)
    {
        Position = position;
        Direction = direction;
    }

    public void Advance()
    {
        // Calculate the new position
        int newX = Position.X + (int)Direction.X;
        int newY = Position.Y + (int)Direction.Y;

        // Apply wrapping logic with remaining direction value
        newX = (newX % (MaxRow + 1) + (MaxRow + 1)) % (MaxRow + 1); // Ensure within 0 to MaxRow
        newY = (newY % (MaxColumn + 1) + (MaxColumn + 1)) % (MaxColumn + 1); // Ensure within 0 to MaxColumn

        // Update the position
        Position = new Point(newX, newY);

        // Increment the move counter
        _moves++;
    }

    //public void Advance()
    //{
    //    // Calculate the new position
    //    int newX = Position.X + (int)Direction.X;
    //    int newY = Position.Y + (int)Direction.Y;

    //    // Apply teleportation logic
    //    if (newY > MaxColumn) newX = 0; // Wrap around to the first column
    //    if (newY < 0) newY = MaxColumn; // Wrap around to the last column
    //    if (newX > MaxRow) newX = 0; // Wrap around to the first row
    //    if (newX < 0) newX = MaxRow; // Wrap around to the last row

    //    // Update the position
    //    Position = new Point(newX, newY);

    //    // Increment the move counter
    //    _moves++;
    //}

    public double GetMoves() => _moves;

    public static void SetBoundaries(int maxRow, int maxColumn)
    {
        MaxRow = maxRow;
        MaxColumn = maxColumn;
    }
}
