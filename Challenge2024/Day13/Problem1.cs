namespace Challenge2024.Day13;

internal class Problem1 : Day13Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day13", true);
        ParseInputs(inputs);

        foreach(var machine in Machines)
        {
            Console.WriteLine(machine);
        }
    }
}
