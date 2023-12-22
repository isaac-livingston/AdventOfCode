using Challenge2023.Common;
using Challenge2023.Day20.Models;

namespace Challenge2023.Day20;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        BuildMachine(inputs);

        (long highs, long lows) solution = (0, 0);

        for (var i = 0; i < 1000; i++)
        {
            solution = Machine.PusbButton();
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"H:{solution.highs} L:{solution.lows}");
        ConsoleTools.PrintSolutionMessage($"T:{solution.highs  * solution.lows}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

