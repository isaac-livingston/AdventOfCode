using Challenge2023.Common;

namespace Challenge2023.Day20;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: true);

        stopwatch.Start();

        LoadData(inputs);

        (long highs, long lows) solution = (0, 0);

        for (var i = 0; i <2; i++)
        {
            var (highs, lows) = Machine.CycleMachine(0);
            solution.highs += highs;
            solution.lows += lows;
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

