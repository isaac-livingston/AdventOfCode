using Challenge2023.Common;
using Challenge2023.Day05.Models;

namespace Challenge2023.Day05
{
    internal class Problem2 : Day05Base
    {
        protected override void LoadSeeds(string seedLine)
        {
            var seedValues = seedLine.Split(':', SPLIT_OPTS)[1].Split(' ');

            var lowestLoc = long.MaxValue;

            for (var i = 0; i < seedValues.Length; i += 2)
            {
                var startingSeed = long.Parse(seedValues[i]);
                long endSeed = startingSeed + long.Parse(seedValues[i + 1]) - 1;

                for (var s = startingSeed; s < endSeed + 1; s++)
                {
                    MapOutSeed(s, out var location);
                    lowestLoc = Math.Min(location, lowestLoc);
                }
            }

            Seeds.Add(new Seed(0) 
            {
                Location = lowestLoc 
            });
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day05");

            stopwatch.Start();

            InitializeData(inputs);

            var locs = Seeds.Select(x => x.Location);
            var lowestLoc = locs.Min(x => x);

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(lowestLoc);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
