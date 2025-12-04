namespace Challenge2025.Day04;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day04", useTest: false);
        ParseInputs(inputs);

        // Set to true to generate a GIF animation
        var generateGif = true;

        int result;
        
        if (generateGif)
        {
            // Choose your theme! Options: Default, Matrix, Ocean, Sunset, Retro, Neon
            var theme = FrameRenderOptions.XiPurple;
            
            // Or customize your own:
            // var theme = new FrameRenderOptions
            // {
            //     CellSize = 8,
            //     BackgroundColor = new SkiaSharp.SKColor(10, 10, 20),
            //     RollColor = new SkiaSharp.SKColor(0, 255, 128),
            //     HighlightRollColor = new SkiaSharp.SKColor(255, 50, 50),
            //     WallColor = new SkiaSharp.SKColor(50, 50, 60),
            //     EmptyColor = new SkiaSharp.SKColor(20, 20, 30),
            //     RoundedCorners = 2,
            //     DrawRollSymbol = false
            // };

            result = RemoveRollsWithGif(
                options: theme,
                frameDelayMs: 30,           // Speed of animation
                captureEveryNIterations: 1   // Capture every iteration (increase to skip frames for large grids)
            );
        }
        else
        {
            result = RemoveRolls();
        }

        Console.WriteLine($"Answer: {result}");
    }
}
