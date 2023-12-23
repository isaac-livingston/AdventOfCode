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

        if (Machine == null)
        {
            Console.WriteLine("Machine is null!");
            return;
        }

        (long highs, long lows) solution = (0, 0);

        for (var i = 0; i < 1000; i++)
        {
            solution = Machine.PusbButton();
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"Pulses - High:{solution.highs}, Low:{solution.lows}");
        ConsoleTools.PrintSolutionMessage($"{solution.highs * solution.lows} (High Pulses * Low Pulses)");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

