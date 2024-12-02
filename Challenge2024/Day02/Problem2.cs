namespace Challenge2024.Day02;
internal class Problem2 : Day02Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day02", false);
        ParseInputs(inputs);

        int safeReportCount = Reports.Sum(report =>
        {
            if (ReportIsSafe(report))
            {
                return 1;
            }
            
            var dampenedReports = GetDampenedReports(report);

            foreach (var dampenedReport in dampenedReports)
            {
                if (ReportIsSafe(dampenedReport))
                {
                    return 1;
                }
            }

            return 0;
        });

        Console.WriteLine($"Total: {safeReportCount:N0}");
    }
}
