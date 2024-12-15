namespace Challenge2024.Day13;

internal class Problem2 : Day13Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day13", false);
        ParseInputs(inputs, 10000000000000);

        var spend = 0D;

        foreach (var machine in Machines)
        {
            Console.WriteLine(machine);

            var winnable = machine.PrizeWinnable();

            if (winnable)
            {
                spend += machine.TotalCost;
            }
        }

        Console.WriteLine($"Total spend: {spend}");
    }
}
