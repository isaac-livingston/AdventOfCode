using Challenge2023.Common;
using Challenge2023.Day05.Models;

#nullable disable

namespace Challenge2023.Day05
{
    internal abstract class Day05Base : ProblemBase
    {
        protected long LowestLocation = long.MaxValue;

        protected Map SeedToSoil { get; private set; }
        protected Map SoilToFertilizer { get; private set; }
        protected Map FertilizerToWater { get; private set; }
        protected Map WaterToLight { get; private set; }
        protected Map LightToTemperature { get; private set; }
        protected Map TemperatureToHumidity { get; private set; }
        protected Map HumidityToLocation { get; private set; }

        protected void InitializeData(string[] inputs)
        {
            var sets = DecomposeInputs(inputs);

            if (sets.Count > 1)
            {
                foreach (var set in sets.Skip(1))
                {
                    LoadMap(set);
                }
            }

            if (sets.Count > 0 && sets[0].Count > 0)
            {
                DetermineLowestLocation(sets[0][0]);
            }
        }

        private static List<List<string>> DecomposeInputs(string[] inputs)
        {
            var sets = new List<List<string>>();
            var lineSet = new List<string>();

            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input))
                {
                    if (lineSet.Count > 0)
                    {
                        sets.Add(lineSet);
                        lineSet = [];
                    }
                }
                else
                {
                    lineSet.Add(input);
                }
            }

            if (lineSet.Count > 0)
            {
                sets.Add(lineSet);
            }

            return sets;
        }

        protected abstract void DetermineLowestLocation(string seedLine);

        private void LoadMap(List<string> mapInputs)
        {
            var mapKey = mapInputs.First().Split(' ', SPLIT_OPTS)[0];

            var tables = mapInputs.Skip(1).ToList();

            _ = mapKey switch
            {
                MapNames.SeedToSoil => SeedToSoil = new Map(tables),
                MapNames.SoilToFertilizer => SoilToFertilizer = new Map(tables),
                MapNames.FertilizerToWater => FertilizerToWater = new Map(tables),
                MapNames.WaterToLight => WaterToLight = new Map(tables),
                MapNames.LightToTemperature => LightToTemperature = new Map(tables),
                MapNames.TemperatureToHumidity => TemperatureToHumidity = new Map(tables),
                _ => HumidityToLocation = new Map(tables)
            };
        }

        protected void MapSeedToLocation(long seedId, out long locationId)
        {
            locationId =
                HumidityToLocation[
                    TemperatureToHumidity[
                        LightToTemperature[
                            WaterToLight[
                                FertilizerToWater[
                                    SoilToFertilizer[
                                        SeedToSoil[seedId]]]]]]];
        }
    }
}
