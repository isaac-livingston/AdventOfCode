namespace Challenge2025.Day08;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day08", useTest: false);
        ParseInputs(inputs);
        BuildAllPairs();

        var lastPair = ConnectUntilSingleCircuit();

        long result = (long)lastPair.A.X * lastPair.B.X;

        Console.WriteLine($"Answer: {result}");
    }
}
