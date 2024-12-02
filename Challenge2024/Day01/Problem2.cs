namespace Challenge2024.Day01;

internal class Problem2 : Day01Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day01");
        ParseInputs(inputs);

        var lefts = Lefts.GroupBy(x => x)
                         .ToDictionary(x => x.Key, x => x.Count());

        var rights = Rights.GroupBy(x => x)
                           .ToDictionary(x => x.Key, x => x.Count());

        int similarity = lefts.Sum(left =>
        {
            if (rights.TryGetValue(left.Key, out int rightCount))
            {
                return left.Key * left.Value * rightCount;
            }

            return 0;
        });

        Console.WriteLine($"Total: {similarity:N0}");
    }
}
