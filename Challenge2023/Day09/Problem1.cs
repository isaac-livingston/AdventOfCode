using Challenge2023.Common;

namespace Challenge2023.Day09
{
    internal class Problem1 : Day09Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day09");

            //inputs = [
            //    "0 3 6 9 12 15",
            //    "1 3 6 10 15 21",
            //    "10 13 16 21 30 45"
            //];

            stopwatch.Start();

            LoadMeasurements(inputs);

            ExtendMeasurements();

            var solution = GetProblem1Solution();

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
