namespace Challenge2025.Day11;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day11", useTest: false);
        ParseInputs(inputs);

        var result = CountPathsThrough("svr", "out", "dac", "fft");

        Console.WriteLine($"Answer: {result}");
    }
}
