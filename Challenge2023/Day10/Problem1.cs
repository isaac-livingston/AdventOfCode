using Challenge.Common;

namespace Challenge2023.Day10;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER);

        stopwatch.Start();

        LoadData(inputs);

        var runFromEast = TraverseNetwork(comingInFrom: EAST);

        var solution = runFromEast.Values.ElementAt((runFromEast.Count / 2) + 1);

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

