using SkiaSharp;

namespace Challenge2025.Day04;

/// <summary>
/// Renders grid frames for GIF animation using SkiaSharp
/// </summary>
public class FrameRenderer
{
    private readonly FrameRenderOptions _options;
    private readonly string _outputDirectory;
    private int _frameCounter = 0;

    public FrameRenderer(FrameRenderOptions? options = null, string? outputDirectory = null)
    {
        _options = options ?? new FrameRenderOptions();
        _outputDirectory = outputDirectory ?? Path.Combine(Directory.GetCurrentDirectory(), "output", "day04", "frames");
        
        if (Directory.Exists(_outputDirectory))
        {
            Directory.Delete(_outputDirectory, recursive: true);
        }
        Directory.CreateDirectory(_outputDirectory);
    }

    public string OutputDirectory => _outputDirectory;

    /// <summary>
    /// Renders the current grid state to a PNG file and returns the file path
    /// </summary>
    public string RenderFrame(int[,] grid, List<(int R, int C)> paperRolls, List<(int R, int C)>? highlightedRolls = null)
    {
        var rows = grid.GetLength(0);
        var cols = grid.GetLength(1);
        
        var width = cols * _options.CellSize + _options.Padding * 2;
        var height = rows * _options.CellSize + _options.Padding * 2;

        using var bitmap = new SKBitmap(width, height);
        using var canvas = new SKCanvas(bitmap);

        // Background
        canvas.Clear(_options.BackgroundColor);

        // Draw grid cells
        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < cols; c++)
            {
                var x = _options.Padding + c * _options.CellSize;
                var y = _options.Padding + r * _options.CellSize;
                var rect = new SKRect(x, y, x + _options.CellSize - _options.CellSpacing, y + _options.CellSize - _options.CellSpacing);

                var color = grid[r, c] == 1 ? _options.WallColor : _options.EmptyColor;
                
                using var paint = new SKPaint
                {
                    Color = color,
                    Style = SKPaintStyle.Fill,
                    IsAntialias = _options.AntiAlias
                };
                
                if (_options.RoundedCorners > 0)
                {
                    canvas.DrawRoundRect(rect, _options.RoundedCorners, _options.RoundedCorners, paint);
                }
                else
                {
                    canvas.DrawRect(rect, paint);
                }
            }
        }

        // Draw paper rolls
        foreach (var roll in paperRolls)
        {
            var x = _options.Padding + roll.C * _options.CellSize;
            var y = _options.Padding + roll.R * _options.CellSize;
            var rect = new SKRect(x, y, x + _options.CellSize - _options.CellSpacing, y + _options.CellSize - _options.CellSpacing);

            var isHighlighted = highlightedRolls?.Any(h => h.R == roll.R && h.C == roll.C) ?? false;
            var color = isHighlighted ? _options.HighlightRollColor : _options.RollColor;

            using var paint = new SKPaint
            {
                Color = color,
                Style = SKPaintStyle.Fill,
                IsAntialias = _options.AntiAlias
            };

            if (_options.RoundedCorners > 0)
            {
                canvas.DrawRoundRect(rect, _options.RoundedCorners, _options.RoundedCorners, paint);
            }
            else
            {
                canvas.DrawRect(rect, paint);
            }

            // Optional: Draw roll symbol
            if (_options.DrawRollSymbol)
            {
                using var symbolPaint = new SKPaint
                {
                    Color = _options.RollSymbolColor,
                    IsAntialias = _options.AntiAlias
                };
                
                using var font = new SKFont(
                    SKTypeface.FromFamilyName("Arial", SKFontStyleWeight.Bold, SKFontStyleWidth.Normal, SKFontStyleSlant.Upright),
                    _options.CellSize * 0.6f
                );

                var centerX = x + (_options.CellSize - _options.CellSpacing) / 2f;
                var centerY = y + (_options.CellSize - _options.CellSpacing) / 2f + font.Size / 3f;
                canvas.DrawText("@", centerX, centerY, SKTextAlign.Center, font, symbolPaint);
            }
        }

        // Save frame
        var framePath = Path.Combine(_outputDirectory, $"frame_{_frameCounter:D4}.png");
        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using var stream = File.OpenWrite(framePath);
        data.SaveTo(stream);

        _frameCounter++;
        return framePath;
    }

    /// <summary>
    /// Gets all frame paths in order
    /// </summary>
    public string[] GetFramePaths()
    {
        return Directory.GetFiles(_outputDirectory, "frame_*.png")
                       .OrderBy(f => f)
                       .ToArray();
    }
}

/// <summary>
/// Configuration options for frame rendering
/// </summary>
public class FrameRenderOptions
{
    /// <summary>Size of each cell in pixels</summary>
    public int CellSize { get; set; } = 10;

    /// <summary>Padding around the grid in pixels</summary>
    public int Padding { get; set; } = 5;

    /// <summary>Spacing between cells in pixels</summary>
    public int CellSpacing { get; set; } = 1;

    /// <summary>Rounded corner radius (0 for square corners)</summary>
    public float RoundedCorners { get; set; } = 2f;

    /// <summary>Enable anti-aliasing for smoother rendering</summary>
    public bool AntiAlias { get; set; } = true;

    /// <summary>Draw @ symbol on rolls</summary>
    public bool DrawRollSymbol { get; set; } = false;

    // Colors
    /// <summary>Background color of the canvas</summary>
    public SKColor BackgroundColor { get; set; } = new SKColor(30, 30, 40); // Dark blue-gray

    /// <summary>Color for wall cells (value = 1)</summary>
    public SKColor WallColor { get; set; } = new SKColor(60, 60, 70); // Slightly lighter gray

    /// <summary>Color for empty cells (value = 0)</summary>
    public SKColor EmptyColor { get; set; } = new SKColor(20, 20, 25); // Very dark

    /// <summary>Color for paper rolls</summary>
    public SKColor RollColor { get; set; } = new SKColor(0, 200, 100); // Bright green

    /// <summary>Color for highlighted/about-to-be-removed rolls</summary>
    public SKColor HighlightRollColor { get; set; } = new SKColor(255, 100, 100); // Red

    /// <summary>Color for the @ symbol on rolls</summary>
    public SKColor RollSymbolColor { get; set; } = SKColors.White;

    // Preset themes
    public static FrameRenderOptions Default => new();

    public static FrameRenderOptions Matrix => new()
    {
        BackgroundColor = SKColors.Black,
        WallColor = new SKColor(0, 30, 0),
        EmptyColor = new SKColor(0, 10, 0),
        RollColor = new SKColor(0, 255, 0),
        HighlightRollColor = new SKColor(255, 255, 0),
        RoundedCorners = 0
    };

    public static FrameRenderOptions Ocean => new()
    {
        BackgroundColor = new SKColor(10, 30, 60),
        WallColor = new SKColor(30, 80, 120),
        EmptyColor = new SKColor(5, 20, 40),
        RollColor = new SKColor(100, 200, 255),
        HighlightRollColor = new SKColor(255, 150, 100),
        RoundedCorners = 4
    };

    public static FrameRenderOptions Sunset => new()
    {
        BackgroundColor = new SKColor(40, 20, 40),
        WallColor = new SKColor(80, 40, 60),
        EmptyColor = new SKColor(30, 15, 30),
        RollColor = new SKColor(255, 180, 50),
        HighlightRollColor = new SKColor(255, 80, 80),
        RoundedCorners = 3
    };

    public static FrameRenderOptions Retro => new()
    {
        BackgroundColor = new SKColor(40, 40, 40),
        WallColor = new SKColor(80, 80, 80),
        EmptyColor = new SKColor(20, 20, 20),
        RollColor = new SKColor(255, 255, 255),
        HighlightRollColor = new SKColor(255, 0, 255),
        RoundedCorners = 0,
        CellSpacing = 0
    };

    public static FrameRenderOptions Neon => new()
    {
        BackgroundColor = new SKColor(10, 5, 20),
        WallColor = new SKColor(40, 20, 60),
        EmptyColor = new SKColor(5, 2, 10),
        RollColor = new SKColor(0, 255, 255),
        HighlightRollColor = new SKColor(255, 0, 128),
        RoundedCorners = 2
    };

    public static FrameRenderOptions Xi => new()
    {
        BackgroundColor = new SKColor(0x0a, 0x0e, 0x1a),    // bg-primary
        WallColor = new SKColor(0x2d, 0x3a, 0x5f),          // bg-tertiary
        EmptyColor = new SKColor(0x1a, 0x1a, 0x2e),         // bg-secondary
        RollColor = new SKColor(0xff, 0x3a, 0x00),          // xi-orange
        HighlightRollColor = new SKColor(0xff, 0x58, 0x80), // xi-magenta
        RollSymbolColor = new SKColor(0xe1, 0xd5, 0xb9),    // xi-sand
        RoundedCorners = 2
    };

    public static FrameRenderOptions XiPurple => new()
    {
        BackgroundColor = new SKColor(0x0a, 0x0e, 0x1a),    // bg-primary
        WallColor = new SKColor(0x3c, 0x1f, 0x56),          // xi-purple-dark
        EmptyColor = new SKColor(0x1a, 0x1a, 0x2e),         // bg-secondary
        RollColor = new SKColor(0x7a, 0x3f, 0xa8),          // xi-purple-bright
        HighlightRollColor = new SKColor(0xff, 0x55, 0x22), // xi-orange-bright
        RollSymbolColor = new SKColor(0xe1, 0xd5, 0xb9),    // xi-sand
        RoundedCorners = 2
    };
}
