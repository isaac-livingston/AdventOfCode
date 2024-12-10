namespace Challenge2024.Day10;

internal class Problem2 : Day10Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day10", false);
        ParseInputs(inputs);

        FindTrailHeads();
        CountCompleteTrails();

        var trailRatingCount = 0;

        for (int h = 0; h < TrailHeads.Count; h++)
        {
            trailRatingCount += TrailHeads.Values.ElementAt(h).Count;
            Console.WriteLine($"[{h}] Head: {TrailHeads.Keys.ElementAt(h)}");
            foreach (var p in TrailHeads.Values.ElementAt(h))
            {
                Console.WriteLine($"\t{p}");
            }
        }

        Console.WriteLine($"Trail Rating Sum: {trailRatingCount}");
    }
}
