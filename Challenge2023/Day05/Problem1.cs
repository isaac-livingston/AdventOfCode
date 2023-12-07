using Challenge2023.Common;

namespace Challenge2023.Day05
{
    internal class Problem1 : Day05Base
    {
        protected override void DetermineLowestLocation(string seedLine)
        {
            var seedValues = seedLine.Split(':', SPLIT_OPTS)[1]
                                     .Split(' ')
                                     .Select(long.Parse)
                                     .ToArray();

            for (var i = 0; i < seedValues.Length; i++)
            {
                var startingSeed = seedValues[i];

                MapOutSeed(startingSeed, out var location);
                LowestLocation = Math.Min(location, LowestLocation);
            }
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day05");

            stopwatch.Start();

            InitializeData(inputs);

            var lowestLoc = LowestLocation;

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(lowestLoc);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
