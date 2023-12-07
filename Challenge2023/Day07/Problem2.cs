using Challenge2023.Common;

namespace Challenge2023.Day07
{
    internal class Problem2 : Day07Base
    {
        public override void RunSolution()
        {
            stopwatch.Start();
            var inputs = GetInputs(folder: "day07");

            //InitializeInput(inputs);

            //var solution = GetSolutions();

            stopwatch.Stop();

            //ConsoleTools.PrintSolutionMessage(solution);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
