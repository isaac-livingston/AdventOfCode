namespace Challenge2024.Day10;

internal class Day10Base : ProblemBase
{
    public int[,] TrailMap { get; private set; } = new int[0, 0];

    public Dictionary<(int row, int col), List<Path>> TrailHeads = [];

    private readonly (int y, int x)[] _directions = [(-1, 0), (1, 0), (0, -1), (0, 1)];

    public void ParseInputs(string[] inputs)
    {
        var mapHeight = inputs.Length;
        var mapWidth = inputs[0].Length;

        TrailMap = new int[mapHeight, mapWidth];

        for (var row = 0; row < mapHeight; row++)
        {
            for (var col = 0; col < mapWidth; col++)
            {
                TrailMap[row, col] = int.Parse(inputs[row][col].ToString());
            }
        }
    }

    public void FindTrailHeads()
    {
        for (var row = 0; row < TrailMap.GetLength(0); row++)
        {
            for (var col = 0; col < TrailMap.GetLength(1); col++)
            {
                if (TrailMap[row, col] == 0)
                {
                    TrailHeads.Add((row, col), []);
                }
            }
        }
    }

    public void CountCompleteTrails()
    {
        foreach (var trailHead in TrailHeads.Keys.ToList())
        {
            var visited = new HashSet<(int row, int col)>();
            var currentPath = new Path { Steps = [new Step(trailHead.row, trailHead.col, 0)] };

            DFS(trailHead.row, trailHead.col, 0, visited, currentPath, TrailHeads[trailHead]);
        }
    }

    private void DFS(int row, int col, int currentNumber, HashSet<(int row, int col)> visited, Path currentPath, List<Path> completedPaths)
    {
        if (currentNumber == 9)
        {
            completedPaths.Add(new Path { Steps = new List<Step>(currentPath.Steps) });
            return;
        }

        visited.Add((row, col));

        foreach (var (y, x) in _directions)
        {
            var newRow = row + y;
            var newCol = col + x;

            if (IsValid(newRow, newCol, currentNumber + 1) && !visited.Contains((newRow, newCol)))
            {
                currentPath.Steps.Add(new Step(newRow, newCol, currentNumber + 1));
                DFS(newRow, newCol, currentNumber + 1, visited, currentPath, completedPaths);
                currentPath.Steps.RemoveAt(currentPath.Steps.Count - 1);
            }
        }

        visited.Remove((row, col));
    }

    private bool IsValid(int row, int col, int targetNumber)
    {
        return row >= 0 && row < TrailMap.GetLength(0) &&
               col >= 0 && col < TrailMap.GetLength(1) &&
               TrailMap[row, col] == targetNumber;
    }
}
