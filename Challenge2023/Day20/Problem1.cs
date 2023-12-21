using Challenge2023.Common;
using Challenge2023.Day20.Models;

namespace Challenge2023.Day20;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        LoadData(inputs);

        for (var i = 0; i < 1000; i++)
        {
            _ = Machine.CycleMachine(0);
        }

        stopwatch.Stop();

        (long highs, long lows) solution = (BaseComponent.HighPulseCount, BaseComponent.LowPulseCount);

        ConsoleTools.PrintSolutionMessage($"H:{solution.highs} L:{solution.lows}");
        ConsoleTools.PrintSolutionMessage($"T:{solution.highs  * solution.lows}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

