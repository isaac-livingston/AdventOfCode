using Challenge2023.Common;

namespace Challenge2023.Day07
{
    internal class Problem1 : Day07Base
    {
        public override void RunSolution()
        {
            stopwatch.Start();
            var inputs = GetInputs(folder: "day07");

            foreach (var key in Cards.Keys)
            {
                Console.WriteLine($"{key}: {Cards[key]}");
            }

            //InitializeInput(inputs);

            //var solution = GetSolutions();

            stopwatch.Stop();

            //ConsoleTools.PrintSolutionMessage(solution);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
