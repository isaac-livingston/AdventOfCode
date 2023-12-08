using Challenge2023.Common;

namespace Challenge2023.Day08
{
    internal class Problem1 : Day08Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day08");

            stopwatch.Start();

            LoadData(inputs);

            var solution = TraverseMap((x) => x != "ZZZ", "AAA");

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(solution);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
