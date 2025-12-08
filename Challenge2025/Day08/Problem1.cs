namespace Challenge2025.Day08;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day08", useTest: false);
        ParseInputs(inputs);
        BuildAllPairs();
        MakeConnections(connectionCount: 1000);

        long result = CalculateTopCircuitsProduct(topN: 3);

        Console.WriteLine($"Answer: {result}");
    }
}
