namespace Challenge2025.Day01;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day01", useTest: false);
        ParseInputs(inputs);

        var steps = Steps;

        var currentPosition = 50;
        var timesZeroEncountered = 0;

        foreach (var step in steps)
        {
            var (endingPosition, encounters) = TurnDial(currentPosition, step);
            timesZeroEncountered += encounters;

            currentPosition = endingPosition;
        }

        Console.WriteLine($"Answer: {timesZeroEncountered}");
    }
}
