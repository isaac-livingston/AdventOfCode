using Challenge2023.Day05.Models;

namespace Challenge2023.Day05
{
    internal class Problem2 : Day05Base
    {
        protected override void LoadSeeds(string seedLine)
        {
            var seedValues = seedLine.Split(':', SPLIT_OPTS)[1].Split(' ');

            var lowestLoc = long.MaxValue;

            Console.WriteLine("stared seeding:" + stopwatch.ElapsedMilliseconds.ToString());
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

            //Console.WriteLine("ended seeding:" + stopwatch.ElapsedMilliseconds.ToString());
        }

        public override void RunSolution()
        {
            
            stopwatch.Start();
            var inputs = GetInputs(folder: "day05");

            //inputs = [
            //    "seeds: 79 14 55 13",
            //    "",
            //    "seed-to-soil map:",
            //    "50 98 2",
            //    "52 50 48",
            //    "",
            //    "soil-to-fertilizer map:",
            //    "0 15 37",
            //    "37 52 2",
            //    "39 0 15",
            //    "",
            //    "fertilizer-to-water map:",
            //    "49 53 8",
            //    "0 11 42",
            //    "42 0 7",
            //    "57 7 4",
            //    "",
            //    "water-to-light map:",
            //    "88 18 7",
            //    "18 25 70",
            //    "",
            //    "light-to-temperature map:",
            //    "45 77 23",
            //    "81 45 19",
            //    "68 64 13",
            //    "",
            //    "temperature-to-humidity map:",
            //    "0 69 1",
            //    "1 0 69",
            //    "",
            //    "humidity-to-location map:",
            //    "60 56 37",
            //    "56 93 4"
            //];

            InitializeData(inputs);

            var locs = Seeds.Select(x => x.Location);
            var lowestLoc = locs.Min(x => x);
            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("lowest location: " + lowestLoc);
            Console.WriteLine();
            Console.WriteLine("duration: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}
