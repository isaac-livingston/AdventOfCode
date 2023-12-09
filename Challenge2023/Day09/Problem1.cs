using Challenge2023.Common;

namespace Challenge2023.Day09
{
    internal class Problem1 : Day09Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day09");

            stopwatch.Start();

            //LoadData(inputs);

            stopwatch.Stop();

            //ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
