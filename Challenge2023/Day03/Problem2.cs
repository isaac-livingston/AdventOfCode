namespace Challenge2023.Day03
{
    internal class Problem2 : Day03Base
    {
        long GetGearRatioOutput()
        {
            var gearDigits = SymbolAdjacentDigits.Values.Where(x => x.anchorSymbol.Value == '*')
                                                        .GroupBy(x => x.anchorSymbol.Point)
                                                        .Where(x => x.Count() == 2)
                                                        .Select(x =>
                                                        {
                                                            var ratio = 1;
                                                            foreach (var (partNumber, anchorSymbol) in x)
                                                            {
                                                                ratio *= partNumber;
                                                            }
                                                            return ratio;
                                                        });

            return gearDigits.Sum();
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day03");

            LoadSybmolsFromMatrix(inputs);

            var digitPoints = GetPointsOfSymbolAdjacentDigits(inputs);

            LoadSymbolAdjacentDigits(inputs, digitPoints);

            var gearRatio = GetGearRatioOutput();

            Console.WriteLine();
            
            Console.WriteLine($"Gear Ratio: {gearRatio:N0}");
        }
    }
}
