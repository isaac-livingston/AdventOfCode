namespace Challenge2024.Day06;

internal class Problem2 : Day06Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day06", false);
        ParseInputs(inputs);

        var grid = SecurityGrid.Grid;
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);

        var initialY = SecurityGuard.Y;
        var initialX = SecurityGuard.X;
        var initialDirection = SecurityGuard.ForwardDirection;

        int loopCount = 0;

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                if (grid[y, x] == '.')
                {
                    grid[y, x] = '#';
                    SecurityGrid.InitializeGrid(grid);
                    SecurityGuard = new Cursor(initialY, initialX, initialDirection);

                    if (TestPathForLoopCondition())
                    {
                        //Console.WriteLine($"Adding # at ({x}, {y}) creates a loop condition.");
                        loopCount++;
                    }

                    grid[y, x] = '.';
                }
            }
        }
        Console.WriteLine($"Total loop conditions: {loopCount}");
    }

    private bool TestPathForLoopCondition()
    {
        var visitedSequence = new List<(int x, int y, DirectionFlag direction)>();
        var currentNode = SecurityGuard.SeekNextNodeInForwardDirection(SecurityGrid);

        while (currentNode != null)
        {
            var position = (SecurityGuard.X, SecurityGuard.Y, SecurityGuard.ForwardDirection);
            visitedSequence.Add(position);

            if (visitedSequence.Count >= 8)
            {
                if (IsRepeatingPattern(visitedSequence))
                {
                    return true;
                }
            }

            currentNode = SecurityGuard.SeekNextNodeInForwardDirection(SecurityGrid);
            if (currentNode != null)
            {
                SecurityGuard.TurnRight();
            }
        }

        return false;
    }

    private static bool IsRepeatingPattern(List<(int x, int y, DirectionFlag direction)> sequence)
    {
        int n = sequence.Count;

        for (int length = 4; length <= n / 2; length++)
        {
            var lastPattern = sequence.GetRange(n - length, length);
            var previousPattern = sequence.GetRange(n - 2 * length, length);

            if (lastPattern.SequenceEqual(previousPattern))
            {
                return true;
            }
        }

        return false;
    }
}
