namespace Challenge2024.Day15;

internal class TheGrid
{
    public List<Obstruction> Obstructions { get; } = [];

    public int MaxRow { get; set; }
    public int MaxColumn { get; set; }

    public void AddObstruction(Obstruction obstruction) => Obstructions.Add(obstruction);

    public bool AttemptMove(Cursor cursor, char direction)
    {
        var (r1, c1) = GridDirections.GetDirection(direction);

        int newRow = cursor.Row + r1;
        int newColumn = cursor.Column + c1;

        var obstruction = Obstructions.FirstOrDefault(o => o.Row == newRow && o.Column == newColumn);
        if (obstruction is null)
        {
            cursor.Step(direction);
            return true;
        }

        if (obstruction is Box)
        {
            if (!CanMoveBoxes(newRow, newColumn, r1, c1)) return false;

            MoveBoxes(newRow, newColumn, r1, c1, direction);

            cursor.Step(direction);
            return true;
        }

        return false;
    }

    private bool CanMoveBoxes(int row, int column, int r1, int c1)
    {
        int nextRow = row;
        int nextColumn = column;

        while (true)
        {
            nextRow += r1;
            nextColumn += c1;
            var obstruction = Obstructions.FirstOrDefault(o => o.Row == nextRow && o.Column == nextColumn);

            if (obstruction is null)
            {
                return true;
            }

            if (obstruction is Wall)
            {
                return false;
            }

            if (obstruction is not Box)
            {
                return false;
            }
        }
    }

    private void MoveBoxes(int row, int column, int r1, int c1, char direction)
    {
        var boxesToMove = new List<Box>();
        int currentRow = row;
        int currentColumn = column;

        while (true)
        {
            var obstruction = Obstructions.FirstOrDefault(o => o.Row == currentRow && o.Column == currentColumn);

            if (obstruction is Box box)
            {
                boxesToMove.Add(box);
            }
            else
            {
                break;
            }

            currentRow += r1;
            currentColumn += c1;
        }

        foreach (var box in boxesToMove.AsEnumerable().Reverse())
        {
            box.Slide(direction);
        }
    }
}
