using Challenge2023.Common;

namespace Challenge2023.Day08
{
    internal class Problem2 : Day08Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day08");

            stopwatch.Start();

            LoadData(inputs);

            var solution = Problem2Doer();

            stopwatch.Stop();

            ConsoleTools.PrintAttributionMessage("mohammedsouleymane");
            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
