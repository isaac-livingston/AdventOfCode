using Challenge.Common;

namespace Challenge2023.Day13;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        LoadData(inputs);

        //var sb = new StringBuilder();

        //foreach (var pattern in Patterns)
        //{
        //    for (int r = 0; r < pattern.HorizontalLines.Length; r++)
        //    {
        //        sb.AppendLine(pattern.HorizontalLines[r]);
        //    }
        //    sb.AppendLine();
        //    for (int c = 0; c < pattern.VerticalLines.Length; c++)
        //    {
        //        sb.AppendLine(pattern.VerticalLines[c]);
        //    }
        //    sb.AppendLine();
        //}
        //var path = Path.Combine(@"C:\aoc", "mirrors.txt");
        //File.WriteAllText(path, sb.ToString());

        var solution = AnalyzePatterns();

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

