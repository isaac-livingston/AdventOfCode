namespace Challenge2024.Day02;

internal class Day02Base : ProblemBase
{
    public List<int[]> Reports { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        Reports.AddRange(inputs.Select(input =>
            input.Split(' ', SPLIT_OPTS).Select(int.Parse).ToArray()));
    }

    public static bool ReportIsSafe(int[] report)
    {
        var increasing = true;
        var decreasing = true;

        for (int i = 1; i < report.Length; i++)
        {
            if (report[i] < report[i - 1]) increasing = false;
            if (report[i] > report[i - 1]) decreasing = false;

            if (!increasing && !decreasing) return false;

            var gap = Math.Abs(report[i] - report[i - 1]);
            if (gap < 1 || gap > 3) return false;
        }

        return true;
    }

    public static IEnumerable<int[]> GetDampenedReports(int[] report)
    {
        for (int i = 0; i < report.Length; i++)
        {
            yield return report.Where((_, index) => index != i).ToArray();
        }
    }

    public override void RunSolution()
    {
        throw new NotImplementedException();
    }
}
