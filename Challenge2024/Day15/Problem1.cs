namespace Challenge2024.Day15;

internal class Problem1 : Day15Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day15", false);
        ParseInputs(inputs);

        PrintGrid();

        ExecuteInstructions();

        Console.WriteLine(GetCoordinateSum());
    }
}
