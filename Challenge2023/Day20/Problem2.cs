using Challenge2023.Common;
using Challenge2023.Day20.Models;

namespace Challenge2023.Day20;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        Console.WriteLine("Reading inputs...");
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        Console.WriteLine("Building the machine...");
        BuildMachine(inputs);

        var solution = 0L;
        var iter = 1000000;

        Console.WriteLine($"Pushing the button, reporting every {iter:N0} pushes...");
        while (true)
        {
            _ = Machine.PusbButton();
            solution++;

            if (solution % iter == 0)
            {
                ConsoleTools.PrintIterationMessage("pushes", solution, stopwatch);
            }
        }

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
