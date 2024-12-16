namespace Challenge2024.Day15;

internal class Day15Base : ProblemBase
{
    public Cursor Robot { get; private set; } = new Cursor(0, 0);
    public TheGrid TheGrid { get; } = new TheGrid();
    
    private string _instructions = string.Empty;

    public void ParseInputs(string[] inputs)
    {
        var instructionMode = false;

        var maxRow = 0;
        var maxColumn = 0;
        var columnCount = true;

        for (int r = 0; r < inputs.Length; r++)
        {
            var input = inputs[r];
            
            if (input.Length == 0)
            {
                instructionMode = true;
            }

            if (instructionMode)
            {
                _instructions += input;
            }
            else
            {
                maxRow++;
                for (int c = 0; c < input.Length; c++)
                {
                    switch (input[c])
                    {
                        case '#':
                            TheGrid.AddObstruction(new Wall(r, c));
                            break;
                        case 'O':
                            TheGrid.AddObstruction(new Box(r, c));
                            break;
                        case '@':
                            Robot = new Cursor(r, c);
                            break;
                    }

                    if(columnCount)
                    {
                        maxColumn++;
                    }
                }
                columnCount = false;
            }
        }

        TheGrid.MaxColumn = maxColumn;
        TheGrid.MaxRow = maxRow;
    }

    public void ExecuteInstructions()
    {
        foreach (var c in _instructions)
        {
            TheGrid.AttemptMove(Robot, c);
        }
    }

    public void PrintGrid()
    {
        var maxRow = TheGrid.MaxRow;
        var maxColumn = TheGrid.MaxColumn;

        Console.WriteLine($"Max Row: {maxRow}, Max Column: {maxColumn}");

        for (int r = 0; r <= maxRow; r++)
        {
            for (int c = 0; c <= maxColumn; c++)
            {
                var obstruction = TheGrid.Obstructions.FirstOrDefault(o => o.Row == r && o.Column == c);
                if (obstruction is null)
                {
                    Console.Write('.');
                }
                else if(obstruction is Box)
                {
                    Console.Write('O');
                }
                else if (obstruction is Wall)
                {
                    Console.Write('#');
                }
            }
            Console.WriteLine();
        }
    }

    public long GetCoordinateSum()
    {
        return TheGrid.Obstructions.OfType<Box>().Sum(b => b.GPSCoordinate);
    }
}