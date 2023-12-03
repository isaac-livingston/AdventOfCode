namespace Challenge2023.Day03
{
    internal class Problem1 : Day03Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day03");

            LoadSybmolsFromMatrix(inputs);

            var digitPoints = GetPointsOfSymbolAdjacentDigits(inputs);

            LoadSymbolAdjacentDigits(inputs, digitPoints);

            Console.WriteLine();
            Console.WriteLine($"Total: {SymbolAdjacentDigits.Values.Select(x => x.partNumber).Sum():N0}");
        }
    }
}
