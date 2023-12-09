using Challenge2023.Common;

namespace Challenge2023.Day09
{
    internal class Problem2 : Day09Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day09");

            stopwatch.Start();

            LoadMeasurements(inputs);

            ExtendMeasurements(intoThePast: true);

            var extensions = Measurements.Values.Select(x => x.Last());

            long solution = 0L;

            for (int i = 0; i < extensions.Count(); i++)
            {
                var extension = extensions.ElementAt(i);
                Array.Reverse(extension);

                var history = extension[0];

                for (int h = 1; h < extension.Length; h++)
                {
                    history = extension[h] - history;
                }

                solution += history;
            }

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
