using Challenge.Common;

namespace Challenge2023.Day11;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER);

        stopwatch.Start();

        LoadData(inputs, expansionFactor: 1000000 - 1);

        var solution = 0L;

        foreach (var gpair in Universe.GalacticPairs)
        {
            solution += Universe.CalculateShortestPathOfGalaticPair(gpair);
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
