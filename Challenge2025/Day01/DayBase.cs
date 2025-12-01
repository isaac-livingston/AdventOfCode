using System.Collections.ObjectModel;

namespace Challenge2025.Day01;

internal abstract class DayBase : ProblemBase
{
    // Shared state between Problem1 and Problem2 goes here
    protected Collection<int> Steps = [];

    protected void ParseInputs(string[] inputs)
    {
        foreach(var input in inputs)
        {
            var m = input.Trim()[0] == 'L' ? -1 : 1;
            var x = int.Parse(input[1..]) * m;
            Steps.Add(x);
        }
    }

    protected (int endingPosition, int touchedZero) TurnDial(int currentPosition, int steps)
    {
        var click = steps > 0 ? 1 : -1;
        var clicksToTake = Math.Abs(steps);
        var touchedZero = 0;

        for (var clicks = 0; clicks < clicksToTake; clicks++)
        {
            currentPosition += click;

            if (currentPosition < 0)
            {
                currentPosition = 99;
            }
            else if (currentPosition > 99)
            {
                currentPosition = 0;
            }

            if (currentPosition == 0)
            {
                touchedZero++;
            }
        }

        return (currentPosition, touchedZero);
    }
}
