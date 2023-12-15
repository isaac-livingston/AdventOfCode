﻿using Challenge2023.Common;

namespace Challenge2023.Day12;

internal class Problem1 : DayBase
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

