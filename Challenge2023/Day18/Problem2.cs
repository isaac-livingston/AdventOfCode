using Challenge2023.Common;

namespace Challenge2023.Day18;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        LoadData(inputs, solvePart2: true);

        var solution = ShoeLaceAreaCalculator() + Perimeter / 2 + 1;

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
