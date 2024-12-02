using Challenge.Common;

namespace Challenge2023.Day09
{
    internal class Problem1 : Day09Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day09");

            stopwatch.Start();

            LoadMeasurements(inputs);

            ExtendMeasurements();

            var solution = Measurements.Values.Select(x => x.Last().Sum(y => y)).Sum();

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
