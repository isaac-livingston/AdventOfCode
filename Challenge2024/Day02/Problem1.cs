namespace Challenge2024.Day02;

internal class Problem1 : Day02Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day02", false);
        ParseInputs(inputs);

        int safeReportCount = Reports.AsParallel().Sum(report =>
        {
            if (ReportIsSafe(report))
            {
                return 1;
            }
            return 0;
        });

        Console.WriteLine($"Total: {safeReportCount:N0}");
    }
}
