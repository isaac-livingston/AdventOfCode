namespace Challenge2024.Day13;

internal class Problem1 : Day13Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day13", false);
        ParseInputs(inputs);

        var spend = 0D;

        foreach(var machine in Machines)
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
