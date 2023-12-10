using Challenge2023.Common;

namespace Challenge2023.Day10
{
    internal class Problem1 : Day10Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day10");

            stopwatch.Start();

            var solution = 0L;

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
