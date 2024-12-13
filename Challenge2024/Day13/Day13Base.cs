namespace Challenge2024.Day13;


/*
Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279
*/


internal class Day13Base : ProblemBase
{
    public List<Machine> Machines { get; } = new List<Machine>();

    public void ParseInputs(string[] inputs)
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
                machine.A = new Button(
                    int.Parse(buttonA[0].Split('+')[1]),
                    int.Parse(buttonA[1].Split('+')[1]),
                    3);

                var buttonB = inputSet[1].Split(':', StringSplitOptions.TrimEntries)[1]
                                         .Split(',', StringSplitOptions.TrimEntries);
                machine.B = new Button(
                    int.Parse(buttonB[0].Split('+')[1]),
                    int.Parse(buttonB[1].Split('+')[1]),
                    1);

                var prize = inputSet[2].Split(':', StringSplitOptions.TrimEntries)[1]
                                       .Split(',', StringSplitOptions.TrimEntries);
                machine.PrizeX = int.Parse(prize[0].Split('=')[1]);
                machine.PrizeY = int.Parse(prize[1].Split('=')[1]);

                Machines.Add(machine);
                inputSet.Clear();
            }
        }
    }
}

internal class Machine()
{
    public Button A { get; set; } = default!;
    public Button B { get; set; } = default!;

    public int PrizeX { get; set; }
    public int PrizeY { get; set; }

    private int _buttonAPresses;
    private int _buttonBPresses;

    public void Reset()
    {
        _buttonAPresses = 0;
        _buttonBPresses = 0;
    }

    public void PressButtonA()
    {
        _buttonAPresses++;
    }

    public void PressButtonB()
    {
        _buttonBPresses++;
    }

    public int TotalCost => A.TokenCost * _buttonAPresses + B.TokenCost * _buttonBPresses;

    public bool IsWinner()
    {
        int x = A.XIncrement * _buttonAPresses + B.XIncrement * _buttonBPresses;
        int y = A.YIncrement * _buttonAPresses + B.YIncrement * _buttonBPresses;

        return x == PrizeX && y == PrizeY;
    }

    public override string ToString()
    {
        return $"Machine: A: [{A.XIncrement}x,  {A.YIncrement}y], B: [{B.XIncrement}x, {B.YIncrement}y], Prize: {PrizeX}x, {PrizeY}y";
    }
}

internal record Button(int XIncrement, int YIncrement, int TokenCost)
{
    public (int X, int Y) Move(int x, int y) => (x + XIncrement, y + YIncrement);
}