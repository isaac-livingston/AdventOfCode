namespace Challenge2025.Day04;

internal abstract class DayBase : ProblemBase
{
    protected int[,] Grid { get; set; } = new int[0, 0];
    protected List<GridPoint> PaperRolls { get; set; } = [];

    protected void ParseInputs(string[] inputs)
    {
        var rows = inputs.Length;
        var cols = inputs[0].Length;

        Grid = new int[rows, cols];
        
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                var element = inputs[r][c];
                if (element == '@')
                {
                    PaperRolls.Add(new(r, c));
                    Grid[r, c] = 0;
                }
                else
                {
                    Grid[r, c] = 1;
                }
            }
        }
    }

    protected readonly record struct GridPoint(int R, int C);

    private static readonly GridPoint[] directions =
    [
        new(-1, 0),  //north
        new(-1, 1),  //north-east
        new(-1, -1), //north-west
        new(0, 1),   //east
        new(0, -1),  //west
        new(1, 0),   //south
        new(1, 1),   //south-east
        new(1, -1)   //south-west
    ];

    private int CountEmptyNeighbors(GridPoint roll)
    {
        var count = 0;
        var maxR = Grid.GetLength(0);
        var maxC = Grid.GetLength(1);

        foreach (var dir in directions)
        {
            var r = roll.R + dir.R;
            var c = roll.C + dir.C;

            if ((uint)r < maxR && (uint)c < maxC && Grid[r, c] == 0)
            {
                count++;
            }
        }
        return count;
    }

    private bool IsMoveable(GridPoint roll) => CountEmptyNeighbors(roll) < 4;

    protected int MoveableRolls() => PaperRolls.Count(IsMoveable);

    protected int RemoveRolls()
    {
        var totalRemoved = 0;

        while (true)
        {
            var rollsToRemove = PaperRolls.Where(IsMoveable).ToList();

            if (rollsToRemove.Count == 0)
            {
                break;
            }

            foreach (var roll in rollsToRemove)
            {
                Grid[roll.R, roll.C] = 1;
                PaperRolls.Remove(roll);
            }

            totalRemoved += rollsToRemove.Count;
        }

        return totalRemoved;
    }

    /// <summary>
    /// Removes rolls while capturing frames for GIF animation
    /// </summary>
    /// <param name="options">Frame rendering options (colors, sizes, etc.)</param>
    /// <param name="outputGifPath">Path for the output GIF file</param>
    /// <param name="frameDelayMs">Delay between frames in milliseconds</param>
    /// <param name="captureEveryNIterations">Capture a frame every N iterations (1 = every iteration)</param>
    /// <returns>Total number of rolls removed</returns>
    protected int RemoveRollsWithGif(
        FrameRenderOptions? options = null, 
        string? outputGifPath = null,
        int frameDelayMs = 100,
        int captureEveryNIterations = 1)
    {
        var renderer = new FrameRenderer(options);
        var totalRemoved = 0;
        var iteration = 0;

        // Capture initial state
        renderer.RenderFrame(
            Grid, 
            PaperRolls.Select(p => (p.R, p.C)).ToList()
        );

        while (true)
        {
            var rollsToRemove = PaperRolls.Where(IsMoveable).ToList();

            if (rollsToRemove.Count == 0)
            {
                break;
            }

            // Capture frame with highlighted rolls before removal
            if (iteration % captureEveryNIterations == 0)
            {
                renderer.RenderFrame(
                    Grid,
                    PaperRolls.Select(p => (p.R, p.C)).ToList(),
                    rollsToRemove.Select(p => (p.R, p.C)).ToList()
                );
            }

            foreach (var roll in rollsToRemove)
            {
                Grid[roll.R, roll.C] = 1;
                PaperRolls.Remove(roll);
            }

            totalRemoved += rollsToRemove.Count;
            iteration++;

            // Capture frame after removal
            if (iteration % captureEveryNIterations == 0)
            {
                renderer.RenderFrame(
                    Grid,
                    PaperRolls.Select(p => (p.R, p.C)).ToList()
                );
            }
        }

        // Capture final state
        renderer.RenderFrame(
            Grid,
            PaperRolls.Select(p => (p.R, p.C)).ToList()
        );

        // Generate GIF
        var gifPath = outputGifPath ?? Path.Combine(renderer.OutputDirectory, "..", "day04_animation.gif");
        var framePaths = renderer.GetFramePaths();
        
        if (framePaths.Length > 0)
        {
            Console.WriteLine($"Generating GIF from {framePaths.Length} frames...");
            GifWriter.Create(gifPath, framePaths, frameDelayMs);
            Console.WriteLine($"GIF saved to: {gifPath}");
        }

        return totalRemoved;
    }
}