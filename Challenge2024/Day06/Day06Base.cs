namespace Challenge2024.Day06;

internal class Day06Base : ProblemBase
{
    public SecurityGrid SecurityGrid { get; } = new SecurityGrid();

    public Cursor SecurityGuard { get; private set; } = default!;

    public void ParseInputs(string[] inputs)
    {
        char[,] grid = new char[inputs.Length, inputs[0].Length];

        for (int y = 0; y < inputs.Length; y++)
        {
            for (int x = 0; x < inputs[0].Length; x++)
            {
                grid[y, x] = inputs[y][x];

                if (inputs[y][x] == '^' || inputs[y][x] == 'v' || inputs[y][x] == '<' || inputs[y][x] == '>')
                {
                    if (SecurityGuard != default!)
                    {
                        throw new InvalidOperationException($"Multiple SecurityGuard symbols found. Existing guard at ({SecurityGuard.X}, {SecurityGuard.Y}).");
                    }

                    SecurityGuard = inputs[y][x] switch
                    {
                        '^' => new Cursor(y, x, DirectionFlag.Up),
                        'v' => new Cursor(y, x, DirectionFlag.Down),
                        '<' => new Cursor(y, x, DirectionFlag.Left),
                        '>' => new Cursor(y, x, DirectionFlag.Right),
                        _ => throw new ArgumentException("Unexpected SecurityGuard direction symbol.")
                    };
                }
            }
        }

        SecurityGrid.InitializeGrid(grid);
    }
}
