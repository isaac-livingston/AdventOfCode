using Challenge.Common;

namespace Challenge2023.Day06
{
    internal class Problem2 : Day06Base
    {
        protected override void InitializeInput(string[] inputs)
        {
            Times.Add(double.Parse(inputs[0].Split(':', SPLIT_OPTS)[1].Replace(" ", null)));
            RecordDistances.Add(double.Parse(inputs[1].Split(':', SPLIT_OPTS)[1].Replace(" ", null)));
        }

        public override void RunSolution()
        {
            stopwatch.Start();
            var inputs = GetInputs(folder: "day06");

            InitializeInput(inputs);

            var solution = GetSolutions();

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(solution);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
