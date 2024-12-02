namespace Challenge2024.Day02;

internal class Day02Base : ProblemBase
{
    public List<int[]> Reports = [];

    public void ParseInputs(string[] inputs)
    {
        foreach (var input in inputs)
        {
            var parts = input.Split(" ", SPLIT_OPTS);
            Reports.Add(parts.Select(int.Parse).ToArray());
        }
    }

    public bool ReportIsSafe(int[] report)
    {
        var allIncreasing = IncreasingProgression(report);
        var allDecreasing = DecreasingProgression(report);
        var allInRange = CheckGapRange(report, 1, 3);

        return (allIncreasing || allDecreasing) && allInRange;
    }

    private bool IncreasingProgression(int[] values)
    {
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] < values[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    private bool DecreasingProgression(int[] values)
    {
        for (int i = 1; i < values.Length; i++)
        {
            if (values[i] > values[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    private bool CheckGapRange(int[] values, int minGap, int maxGap)
    {
        for (int i = 1; i < values.Length; i++)
        {
            var gap = Math.Abs(values[i] - values[i - 1]);

            if (gap < minGap || gap > maxGap)
            {
                return false;
            }
        }
        return true;
    }

    public List<int[]> GetDampenedReports(int[] report)
    {
        var reports = new List<int[]>();

        for (int  i = 0;  i < report.Length;  i++)
        {
            var subreport = report.Take(i)
                                  .Concat(report.Skip(i + 1))
                                  .ToArray();

            reports.Add(subreport);
        }

        return reports;
    }

    public override void RunSolution()
    {
        throw new NotImplementedException();
    }
}
