using Challenge2023.Common;
using Challenge2023.Day05.Models;

#nullable disable

namespace Challenge2023.Day05
{
    internal abstract class Day05Base : ProblemBase
    {
        protected long LowestLocation = long.MaxValue;

        protected readonly Dictionary<string, Map> ReverseMaps = [];

        protected Map SeedToSoil { get; set; }
        protected Map SoilToFertilizer { get; set; }
        protected Map FertilizerToWater { get; set; }
        protected Map WaterToLight { get; set; }
        protected Map LightToTemperature { get; set; }
        protected Map TemperatureToHumidity { get; set; }
        protected Map HumidityToLocation { get; set; }

        protected void InitializeData(string[] inputs)
        {
            var sets = DecomposeInputs(inputs);

            foreach (var set in sets.Skip(1))
            {
                LoadMap(set);
            }

            DetermineLowestLocation(sets[0][0]);
        }

        private List<List<string>> DecomposeInputs(string[] inputs)
        {
            var sets = new List<List<string>>
            {
                new() {
                    inputs[0]
                }
            };

            var lineSet = new List<string>();

            for (var i = 1; i < inputs.Length; i++)
            {
                var input = inputs[i];

                if (string.IsNullOrWhiteSpace(input))
                {
                    if (lineSet.Count > 0)
                    {
                        sets.Add(lineSet);
                    }
                    lineSet = [];
                }
                else
                {
                    lineSet.Add(input);
                }
            }

            sets.Add(lineSet);

            return sets;
        }

        protected abstract void DetermineLowestLocation(string seedLine);

        private void LoadMap(List<string> mapInputs)
        {
            var mapKey = mapInputs.First().Split(' ', SPLIT_OPTS)[0];

            var tables = mapInputs.Skip(1).ToList();

            switch(mapKey)
            {
                case "seed-to-soil":
                    SeedToSoil = new Map(tables);
                    break;
                case "soil-to-fertilizer":
                    SoilToFertilizer = new Map(tables);
                    break;
                case "fertilizer-to-water":
                    FertilizerToWater = new Map(tables);
                    break;
                case "water-to-light":
                    WaterToLight = new Map(tables);
                    break;
                case "light-to-temperature":
                    LightToTemperature = new Map(tables);
                    break;
                case "temperature-to-humidity":
                    TemperatureToHumidity = new Map(tables);
                    break;
                default:
                    HumidityToLocation = new Map(tables);
                    break;
            }
        }

        protected void MapOutSeed(long seedValue, out long location)
        {
            location =  
                HumidityToLocation[
                    TemperatureToHumidity[
                        LightToTemperature[
                            WaterToLight[
                                FertilizerToWater[
                                    SoilToFertilizer[
                                        SeedToSoil[seedValue]]]]]]];
        }
    }
}
