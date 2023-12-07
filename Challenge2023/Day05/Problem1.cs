using Challenge2023.Common;
using Challenge2023.Day05.Models;

namespace Challenge2023.Day05
{
    internal class Problem1 : Day05Base
    {
        protected override void LoadSeeds(string seedLine)
        {
            var seedValues = seedLine.Split(':', SPLIT_OPTS)[1].Split(' ');
            var lowestLoc = long.MaxValue;

            for (var i = 0; i < seedValues.Length; i++)
            {
                var startingSeed = long.Parse(seedValues[i]);

                MapOutSeed(startingSeed, out var location);
                lowestLoc = Math.Min(location, lowestLoc);
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

            var lowestLoc = Seeds.Select(x => x.Location).Min(x => x);

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(lowestLoc);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
