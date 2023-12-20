using Challenge2023.Common;

namespace Challenge2023.Day18;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: true);

        stopwatch.Start();

        LoadData(inputs);

        var solution = ComputeTrench();// Lagoon.Queue.Count;

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

