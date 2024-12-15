namespace Challenge2024.Day13;

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
