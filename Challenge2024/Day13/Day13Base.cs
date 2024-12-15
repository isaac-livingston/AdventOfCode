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