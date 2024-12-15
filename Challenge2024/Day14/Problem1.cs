namespace Challenge2024.Day14;

internal class Problem1 : Day14Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day14", false);
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
