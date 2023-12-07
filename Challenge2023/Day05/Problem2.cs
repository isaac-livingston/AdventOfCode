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
                    MapSeedToLocation(s, out var location);
                    LowestLocation = Math.Min(location, LowestLocation);
                }
            }
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day05");

            inputs = [
                "seeds: 79 14 55 13",
                "",
                "seed-to-soil map:",
                "50 98 2",
                "52 50 48",
                "",
                "soil-to-fertilizer map:",
                "0 15 37",
                "37 52 2",
                "39 0 15",
                "",
                "fertilizer-to-water map:",
                "49 53 8",
                "0 11 42",
                "42 0 7",
                "57 7 4",
                "",
                "water-to-light map:",
                "88 18 7",
                "18 25 70",
                "",
                "light-to-temperature map:",
                "45 77 23",
                "81 45 19",
                "68 64 13",
                "",
                "temperature-to-humidity map:",
                "0 69 1",
                "1 0 69",
                "",
                "humidity-to-location map:",
                "60 56 37",
                "56 93 4"
            ];

            stopwatch.Start();

            InitializeData(inputs);

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage(LowestLocation);
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
