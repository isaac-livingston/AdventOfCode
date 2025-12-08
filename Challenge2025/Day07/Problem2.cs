namespace Challenge2025.Day07;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day07", useTest: false);
        ParseInputs(inputs);

        var finalBeams = SimulateBeam();

        // Each beam's rank represents the number of timelines at that position
        // Sum all ranks to get total timelines
        long result = finalBeams.Sum(b => b.Rank);

        Console.WriteLine($"Final beam count: {finalBeams.Count}");
        Console.WriteLine($"Answer: {result}");
    }
}
