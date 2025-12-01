namespace Challenge2025.Day01;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day01", useTest: false);
        ParseInputs(inputs);

        var steps = Steps;

        var currentPosition = 50;
        var timesZeroReached = 0;

        foreach (var step in steps)
        {
            var (endingPosition, _) = TurnDial(currentPosition, step);

            if (endingPosition == 0)
            {
                timesZeroReached++;
            }

            currentPosition = endingPosition;
        }

        Console.WriteLine($"Answer: {timesZeroReached}");
    }
}
