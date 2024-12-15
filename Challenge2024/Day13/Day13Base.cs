namespace Challenge2024.Day13;

internal class Day13Base : ProblemBase
{
    public List<Machine> Machines { get; } = [];

    public void ParseInputs(string[] inputs, long offset = 0)
    {
        var inputSet = new List<string>();

        for (int i = 0; i < inputs.Length; i++)
        {
            var input = inputs[i];

            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            inputSet.Add(input);

            if (inputSet.Count == 3)
            {
                var machine = new Machine();

                var buttonA = inputSet[0].Split(':', StringSplitOptions.TrimEntries)[1]
                                         .Split(',', StringSplitOptions.TrimEntries);
                machine.ButtonX = new Button(
                    int.Parse(buttonA[0].Split('+')[1]),
                    int.Parse(buttonA[1].Split('+')[1]),
                    3);

                var buttonB = inputSet[1].Split(':', StringSplitOptions.TrimEntries)[1]
                                         .Split(',', StringSplitOptions.TrimEntries);
                machine.ButtonY = new Button(
                    int.Parse(buttonB[0].Split('+')[1]),
                    int.Parse(buttonB[1].Split('+')[1]),
                    1);

                var prize = inputSet[2].Split(':', StringSplitOptions.TrimEntries)[1]
                                       .Split(',', StringSplitOptions.TrimEntries);
                machine.PrizeX = offset + int.Parse(prize[0].Split('=')[1]);
                machine.PrizeY = offset + int.Parse(prize[1].Split('=')[1]);

                Machines.Add(machine);
                inputSet.Clear();
            }
        }
    }
}

internal class Machine()
{
    public Button ButtonX { get; set; } = default!;
    public Button ButtonY { get; set; } = default!;

    public long PrizeX { get; set; }
    public long PrizeY { get; set; }

    private double _buttonXPresses;
    private double _buttonYPresses;

    public void Reset()
    {
        _buttonXPresses = 0;
        _buttonYPresses = 0;
    }

    // well, I need to brush up on my linear algebra :/
    public bool PrizeWinnable()
    {
        Reset();

        // Prize coordinates (target values)
        double prizeX = PrizeX;
        double prizeY = PrizeY;

        // Button movement increments (coefficients in the system of equations)
        double a1 = ButtonX.XIncrement;
        double b1 = ButtonY.XIncrement;
        double a2 = ButtonX.YIncrement;
        double b2 = ButtonY.YIncrement;

        // System of equations:
        // a1 * x + b1 * y = PrizeX​
        // a2 * x + b2 * y = PrizeY

        // Determinant of the coefficient matrix
        double determinant = a1 * b2 - b1 * a2;

        // Check if the system has a unique solution
        if (determinant == 0)
        {
            return false; // No unique solution (parallel or overlapping lines)
        }

        // Solve for x (number of ButtonX presses) and y (number of ButtonY presses)
        double y = (prizeY * a1 - prizeX * a2) / determinant; // Equivalent to solving for y
        double x = (prizeX - y * b1) / a1;                    // Substitute y to solve for x

        // Check if both solutions are integers
        if (x % 1 != 0 || y % 1 != 0)
        {
            return false;
        }

        // Store the number of button presses if valid
        _buttonXPresses = x;
        _buttonYPresses = y;

        return true;
    }

    public double TotalCost => ButtonX.TokenCost * _buttonXPresses + ButtonY.TokenCost * _buttonYPresses;

    public override string ToString()
    {
        return $"Machine: A: [{ButtonX.XIncrement}x,  {ButtonX.YIncrement}y], B: [{ButtonY.XIncrement}x, {ButtonY.YIncrement}y], Prize: {PrizeX}x, {PrizeY}y";
    }
}

internal record Button(double XIncrement, double YIncrement, int TokenCost);