using Challenge2023.Common;

namespace Challenge2023.Day23;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        Console.WriteLine("");
        Console.WriteLine("Reading inputs...");

        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        var solution = 0L;

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
