﻿using Challenge.Common;

namespace Challenge2023.Day19;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER);

        stopwatch.Start();

        var solution = 0L;

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
