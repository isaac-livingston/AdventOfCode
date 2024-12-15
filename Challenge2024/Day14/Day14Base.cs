using System.Text.RegularExpressions;

namespace Challenge2024.Day14;

internal partial class Day14Base : ProblemBase
{
    private const string RXG_ROW = "row";
    private const string RXG_COL = "col";
    private const string RXG_DELTA_ROW = "deltaRow";
    private const string RXG_DELTA_COL = "deltaCol";

    public List<SecurityRobot> DeltaForce { get; } = [];

    public void ParseInputs(string[] inputs, int maxRow, int maxColumn)
    {
        for (int row = 0; row < inputs.Length; row++)
        {
            var line = inputs[row];
            var bot = ParseSecurityRobot(line);
            DeltaForce.Add(bot);
        }

        SecurityRobot.SetBoundaries(maxRow, maxColumn);
    }

    private static SecurityRobot ParseSecurityRobot(string input)
    {
        var match = LineParser().Match(input);
        if (!match.Success)
        {
            throw new FormatException("Invalid input format. Expected: 'p=row,col v=deltaRow,deltaCol'");
        }

        int row = int.Parse(match.Groups[RXG_ROW].Value);
        int column = int.Parse(match.Groups[RXG_COL].Value);
        int deltaRow = int.Parse(match.Groups[RXG_DELTA_ROW].Value);
        int deltaCol = int.Parse(match.Groups[RXG_DELTA_COL].Value);

        return new SecurityRobot(new Point(row, column), new Vector2(deltaRow, deltaCol));
    }

    public void AdvanceDeltaForce()
    {
        foreach (var bot in DeltaForce)
        {
            bot.Advance();
        }
    }

    public void PrintDeltaForce()
    {
        var maxRow = SecurityRobot.MaxRow;
        var maxColumn = SecurityRobot.MaxColumn;
        var botGroups = DeltaForce.GroupBy(bot => bot.Position);

        for (int row = 0; row <= maxRow; row++)
        {
            for (int column = 0; column <= maxColumn; column++)
            {
                var botsAtPoint = botGroups.Sum(group => group.Key == new Point(row, column) ? group.Count() : 0);
                if (botsAtPoint > 0)
                {
                    Console.Write(botsAtPoint);
                }
                else
                {
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
    }

    public (int q0, int q1, int q2, int q3) GetDeltaForceQuadrants(bool print = false)
    {
        var maxRow = SecurityRobot.MaxRow;
        var maxColumn = SecurityRobot.MaxColumn;
        var middleRow = maxRow / 2;
        var middleColumn = maxColumn / 2;

        var botGroups = DeltaForce.GroupBy(bot => bot.Position);

        // Define the quadrant ranges
        var quadrants = GetQuadrantRanges(maxRow, maxColumn);

        // Use LINQ to group robots by quadrant
        var quadrantGroups = DeltaForce.GroupBy(bot => quadrants.Select((range, index) => new { Range = range, Quadrant = index })
                                                                .FirstOrDefault(q => bot.Position.X >= q.Range.rowStart && bot.Position.X <= q.Range.rowEnd &&
                                                                                     bot.Position.Y >= q.Range.colStart && bot.Position.Y <= q.Range.colEnd)?.Quadrant)
                                                                .Where(group => group.Key.HasValue) // Exclude bots that don't fall into any quadrant
                                                                .ToDictionary(group => group.Key!.Value, group => group.ToList());

        if (print)
        {
            for (int row = 0; row <= maxRow; row++)
            {
                for (int column = 0; column <= maxColumn; column++)
                {
                    // Print an empty space if crossing the middle row or column
                    if (row == middleRow || column == middleColumn)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        // Count the number of bots at the current point
                        var botsAtPoint = botGroups.Sum(group => group.Key == new Point(row, column) ? group.Count() : 0);
                        if (botsAtPoint > 0)
                        {
                            Console.Write(botsAtPoint);
                        }
                        else
                        {
                            Console.Write(".");
                        }
                    }
                }
                Console.WriteLine();
            }
        }

        var q0 = quadrantGroups[0].Count;
        var q1 = quadrantGroups[1].Count;
        var q2 = quadrantGroups[2].Count;
        var q3 = quadrantGroups[3].Count;

        return (q0, q1, q2, q3);
    }

    public static (int rowStart, int rowEnd, int colStart, int colEnd)[] GetQuadrantRanges(int maxRow, int maxCol)
    {
        int middleY = maxRow / 2;
        int middleX = maxCol / 2;

        return
        [
            (0, middleY - 1, middleX + 1, maxCol),
            (0, middleY - 1, 0, middleX - 1),
            (middleY + 1, maxRow, 0, middleX - 1),
            (middleY + 1, maxRow, middleX + 1, maxCol)
        ];
    }

    [GeneratedRegex($@"p=(?<{RXG_COL}>\d+),(?<{RXG_ROW}>\d+)\s+v=(?<{RXG_DELTA_COL}>-?\d+),(?<{RXG_DELTA_ROW}>-?\d+)", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex LineParser();
}