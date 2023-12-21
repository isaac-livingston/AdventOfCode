using Challenge2023.Common;
using Challenge2023.Day20.Models;

namespace Challenge2023.Day20;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        LoadData(inputs);

        var solution = 0L;
        var iter = 10000000;

        while (!BaseComponent.FirstLowToOutputterFound)
        {
            _ = Machine.CycleMachine(0);
            solution++;

            if (solution % iter == 0)
            {
                ConsoleTools.PrintDurationMessage($"ITER: {solution / iter:N0}x10^7, {(stopwatch.Elapsed.TotalNanoseconds / solution):N0} ns/push, {stopwatch.ElapsedMilliseconds:N0}");
            }
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
