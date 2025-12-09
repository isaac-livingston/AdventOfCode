namespace Challenge2025.Day09;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day09", useTest: false);
        ParseInputs(inputs);

        long result = FindLargestRectangleArea();

        Console.WriteLine($"Answer: {result}");
    }
}
