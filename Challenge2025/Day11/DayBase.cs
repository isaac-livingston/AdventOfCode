namespace Challenge2025.Day11;

internal abstract class DayBase : ProblemBase
{
    protected Dictionary<string, List<string>> Graph { get; } = [];

    protected void ParseInputs(string[] inputs)
    {
        Graph.Clear();

        foreach (var line in inputs)
        {
            var parts = line.Split(':');
            var device = parts[0].Trim();
            var outputs = parts[1].Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            Graph[device] = outputs;
        }
    }

    protected long CountPaths(string start, string end)
    {
        // Memoized path counting for DAG
        var memo = new Dictionary<string, long>();
        return CountPathsMemo(start, end, memo);
    }

    private long CountPathsMemo(string current, string end, Dictionary<string, long> memo)
    {
        if (current == end) return 1;
        if (memo.TryGetValue(current, out var cached)) return cached;
        if (!Graph.TryGetValue(current, out var outputs)) return 0;

        long count = 0;
        foreach (var next in outputs)
        {
            count += CountPathsMemo(next, end, memo);
        }

        memo[current] = count;
        return count;
    }

    protected long CountPathsThrough(string start, string end, string required1, string required2)
    {
        // For paths that must go through both required1 and required2:
        // We need to count paths: start -> {req1, req2} -> {req2, req1} -> end
        // Since order doesn't matter, we count both orderings

        // Paths: start -> req1 -> req2 -> end
        var pathsVia1Then2 = CountPaths(start, required1) 
                          * CountPaths(required1, required2) 
                          * CountPaths(required2, end);

        // Paths: start -> req2 -> req1 -> end
        var pathsVia2Then1 = CountPaths(start, required2) 
                          * CountPaths(required2, required1) 
                          * CountPaths(required1, end);

        return pathsVia1Then2 + pathsVia2Then1;
    }
}
