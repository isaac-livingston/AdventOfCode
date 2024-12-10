namespace Challenge2024.Day10;

internal class Problem1 : Day10Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day10", false);
        ParseInputs(inputs);

        FindTrailHeads();
        CountCompleteTrails();

        var distinctPathCount = 0;

        for (int h = 0; h < TrailHeads.Count; h++)
        {
            var distinctPaths = TrailHeads.Values.ElementAt(h).DistinctBy(x => x.Steps[9]);
            distinctPathCount += distinctPaths.Count();

            Console.WriteLine($"[{h}] Head: {TrailHeads.Keys.ElementAt(h)}");
            foreach (var p in TrailHeads.Values.ElementAt(h).DistinctBy(x => x.Steps[9]))
            {
                Console.WriteLine($"\t{p}");
            }
        }

        Console.WriteLine($"Distinct Path Count: {distinctPathCount}");
    }
}
