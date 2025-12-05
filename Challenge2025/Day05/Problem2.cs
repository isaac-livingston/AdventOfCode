namespace Challenge2025.Day05;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day05", useTest: false);
        ParseInputs(inputs);

        bool maskAnswer = true;

        // Show animated visualization (maskAnswer: true hides spoilers)
        var result = Visualizer.ShowRangeStatsAnimated(Ranges, maskAnswer: maskAnswer);

        Console.WriteLine(maskAnswer 
            ? "\nAnswer: ***,***,***,***,***" 
            : $"\nAnswer: {result:N0}");
    }
}
