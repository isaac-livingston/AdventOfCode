using Spectre.Console;
using SpectreColor = Spectre.Console.Color;

namespace Challenge2025.Day05;

internal class Visualizer
{
    public static long ShowRangeStatsAnimated(List<(long Start, long End)> originalRanges, int delayMs = 30, bool maskAnswer = false)
    {
        var sorted = originalRanges.OrderBy(r => r.Start).ToList();
        var merged = new List<(long Start, long End)>();
        int mergeCount = 0;
        long finalFreshCodes = 0;

        AnsiConsole.Live(CreateDisplay(originalRanges.Count, 0, 0, 0, 0, 0, 0, false, maskAnswer))
            .Start(ctx =>
            {
                foreach (var range in sorted)
                {
                    if (merged.Count == 0 || merged[^1].End < range.Start - 1)
                    {
                        merged.Add(range);
                    }
                    else
                    {
                        var last = merged[^1];
                        merged[^1] = (last.Start, Math.Max(last.End, range.End));
                        mergeCount++;
                    }

                    // Calculate current stats
                    long totalFreshCodes = merged.Sum(r => r.End - r.Start + 1);
                    long minValue = merged.Min(r => r.Start);
                    long maxValue = merged.Max(r => r.End);
                    long totalSpan = maxValue - minValue + 1;
                    double coveragePercent = (double)totalFreshCodes / totalSpan * 100;

                    // Show real values during animation
                    ctx.UpdateTarget(CreateDisplay(
                        originalRanges.Count,
                        merged.Count,
                        mergeCount,
                        totalFreshCodes,
                        minValue,
                        maxValue,
                        coveragePercent,
                        false,
                        maskAnswer
                    ));

                    finalFreshCodes = totalFreshCodes;
                    Thread.Sleep(delayMs);
                }

                // Final update with mask if requested
                if (maskAnswer)
                {
                    long minValue = merged.Min(r => r.Start);
                    long maxValue = merged.Max(r => r.End);
                    long totalSpan = maxValue - minValue + 1;
                    double coveragePercent = (double)finalFreshCodes / totalSpan * 100;

                    ctx.UpdateTarget(CreateDisplay(
                        originalRanges.Count,
                        merged.Count,
                        mergeCount,
                        finalFreshCodes,
                        minValue,
                        maxValue,
                        coveragePercent,
                        true,
                        maskAnswer
                    ));
                }
            });

        // Final flourish - show top ranges
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[bold green]✓ Merge Complete![/]").RuleStyle("green"));
        
        ShowTopRanges(merged);

        return finalFreshCodes;
    }

    private static string MaskFreshCodes(long number, bool mask) =>
        mask ? $"[dim italic]***,***,***,***,***[/]" : $"{number:N0}";

    private static Table CreateDisplay(int inputCount, int mergedCount, int mergeOps, 
        long freshCodes, long minVal, long maxVal, double coverage, bool maskFreshCodes = false, bool maskAnswer = false)
    {
        var maskStatus = maskAnswer ? "[red]masked[/]" : "[green]unmasked[/]";
        var table = new Table()
            .Border(TableBorder.Rounded)
            .BorderColor(SpectreColor.Blue)
            .Title($"[bold white]Day 05 - Fresh Code Analysis [[[/]{maskStatus}[bold white]]][/]")
            .AddColumn(new TableColumn("[bold blue]Metric[/]").LeftAligned())
            .AddColumn(new TableColumn("[bold green]Value[/]").RightAligned());

        // Progress indicator
        var progress = mergedCount + mergeOps;
        var progressBar = new string('█', Math.Min(progress * 30 / Math.Max(inputCount, 1), 30));
        var remaining = new string('░', 30 - progressBar.Length);
        
        table.AddRow("[yellow]Progress[/]", $"[green]{progressBar}[/][dim]{remaining}[/] {progress}/{inputCount}");
        table.AddRow(new Rule());
        table.AddRow("Input ranges", $"[yellow]{inputCount:N0}[/]");
        table.AddRow("Current merged", $"[cyan]{mergedCount:N0}[/]");
        table.AddRow("Overlaps found", $"[red]{mergeOps:N0}[/]");
        table.AddRow(new Rule());
        
        if (minVal > 0)
        {
            table.AddRow("Min value", $"[dim]{minVal:N0}[/]");
            table.AddRow("Max value", $"[dim]{maxVal:N0}[/]");
            table.AddRow(new Rule());
            table.AddRow("[bold]Fresh codes[/]", $"[bold green]{MaskFreshCodes(freshCodes, maskFreshCodes)}[/]");
            table.AddRow("[bold]Coverage[/]", $"[bold magenta]{coverage:F2}%[/]");
        }

        return table;
    }

    private static void ShowTopRanges(List<(long Start, long End)> merged)
    {
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[bold]Top 5 Largest Ranges[/]").RuleStyle("grey"));

        var topRanges = merged
            .Select(r => (Range: r, Size: r.End - r.Start + 1))
            .OrderByDescending(x => x.Size)
            .Take(5)
            .ToList();

        var rangeTable = new Table()
            .Border(TableBorder.Simple)
            .AddColumn("Range")
            .AddColumn(new TableColumn("Size").RightAligned());

        foreach (var (range, size) in topRanges)
        {
            rangeTable.AddRow(
                $"[cyan]{range.Start:N0}[/] - [cyan]{range.End:N0}[/]",
                $"[green]{size:N0}[/]"
            );
        }

        AnsiConsole.Write(rangeTable);
    }

    public static void ShowRangeStats(List<(long Start, long End)> originalRanges)
    {
        // Sort and merge ranges
        var sorted = originalRanges.OrderBy(r => r.Start).ToList();
        var merged = new List<(long Start, long End)>();
        
        foreach (var range in sorted)
        {
            if (merged.Count == 0 || merged[^1].End < range.Start - 1)
            {
                merged.Add(range);
            }
            else
            {
                var last = merged[^1];
                merged[^1] = (last.Start, Math.Max(last.End, range.End));
            }
        }

        // Calculate stats
        long totalFreshCodes = merged.Sum(r => r.End - r.Start + 1);
        long minValue = merged.Min(r => r.Start);
        long maxValue = merged.Max(r => r.End);
        long totalSpan = maxValue - minValue + 1;
        double coveragePercent = (double)totalFreshCodes / totalSpan * 100;

        // Create the panel
        var table = new Table()
            .Border(TableBorder.Rounded)
            .AddColumn(new TableColumn("[bold blue]Metric[/]").LeftAligned())
            .AddColumn(new TableColumn("[bold green]Value[/]").RightAligned());

        table.AddRow("Input ranges", $"[yellow]{originalRanges.Count:N0}[/]");
        table.AddRow("After merge", $"[yellow]{merged.Count:N0}[/]");
        table.AddRow("Ranges merged", $"[dim]{originalRanges.Count - merged.Count:N0}[/]");
        table.AddRow(new Rule());
        table.AddRow("Min value", $"[cyan]{minValue:N0}[/]");
        table.AddRow("Max value", $"[cyan]{maxValue:N0}[/]");
        table.AddRow("Total span", $"[cyan]{totalSpan:N0}[/]");
        table.AddRow(new Rule());
        table.AddRow("[bold]Total fresh codes[/]", $"[bold green]{totalFreshCodes:N0}[/]");

        AnsiConsole.Write(new Panel(table)
            .Header("[bold white] 🔢 Fresh Code Range Analysis [/]")
            .BorderColor(SpectreColor.Blue));

        // Coverage bar
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[bold]Coverage[/]").RuleStyle("grey"));
        
        var barChart = new BarChart()
            .Width(60)
            .Label($"[bold]Range coverage: {coveragePercent:F2}%[/]")
            .AddItem("Covered", coveragePercent, SpectreColor.Green)
            .AddItem("Gaps", 100 - coveragePercent, SpectreColor.Grey);

        AnsiConsole.Write(barChart);

        // Show top 5 largest merged ranges
        AnsiConsole.WriteLine();
        AnsiConsole.Write(new Rule("[bold]Top 5 Largest Ranges[/]").RuleStyle("grey"));

        var topRanges = merged
            .Select(r => (Range: r, Size: r.End - r.Start + 1))
            .OrderByDescending(x => x.Size)
            .Take(5)
            .ToList();

        var rangeTable = new Table()
            .Border(TableBorder.Simple)
            .AddColumn("Range")
            .AddColumn(new TableColumn("Size").RightAligned());

        foreach (var (range, size) in topRanges)
        {
            rangeTable.AddRow(
                $"[cyan]{range.Start:N0}[/] - [cyan]{range.End:N0}[/]",
                $"[green]{size:N0}[/]"
            );
        }

        AnsiConsole.Write(rangeTable);
    }
}
