namespace Challenge2024.Day03;
internal class Problem2 : Day03Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day03", false);
        ParseInputs(inputs);

        Program.Split("do()", SPLIT_OPTS)
               .Select(x => x.Split("don't()", SPLIT_OPTS).First())
               .ToList()
               .ForEach(LoadInstruction);
        
        var mulTotal = MemorySets.Select(x => x.Item1 * x.Item2)
                                 .Sum();

        Console.WriteLine($"Total: {mulTotal:N0}");
    }
}
