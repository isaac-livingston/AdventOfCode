using Challenge2023.Common;

namespace Challenge2023.Day05
{
    internal class Problem2 : Day05Base
    {
        protected override void DetermineLowestLocation(string seedLine)
        {
            var seedValues = seedLine.Split(':', SPLIT_OPTS)[1]
                                     .Split(' ')
                                     .Select(long.Parse)
                                     .ToArray();

            for (var i = 0; i < seedValues.Length; i += 2)
            {
                var startingSeed = seedValues[i];
                long endSeed = startingSeed + seedValues[i + 1];

                for (var s = startingSeed; s < endSeed; s++)
                {
                    MapOutSeed(s, out var location);
                    LowestLocation = Math.Min(location, LowestLocation);
                }
            }
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day05");

            stopwatch.Start();

            InitializeData(inputs);

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(LowestLocation);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
