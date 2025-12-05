namespace Challenge2025.Day05;

internal abstract class DayBase : ProblemBase
{
    protected List<(long Start, long End)> Ranges = [];
    protected List<long> Codes = [];

    protected void ParseInputs(string[] inputs)
    {
        
        var fillCodes = false;

        for(var i = 0; i < inputs.Length; i++)
        {
            if(string.IsNullOrWhiteSpace(inputs[i]))
            {
                fillCodes = true;
                continue;
            }

            if(fillCodes)
            {
                Codes.Add(long.Parse(inputs[i]));
            }
            else
            {
                var parts = inputs[i].Split('-', StringSplitOptions.RemoveEmptyEntries);
                var start = long.Parse(parts[0]);
                var end = long.Parse(parts[1]);
                Ranges.Add((start, end));
            }
        }   
    }

    protected long CountFreshCodes()
    {
        var freshCodes = new List<long>();
        foreach (var code in Codes)
        {
            if (Ranges.Any(r => r.Start <= code && r.End >= code))
            {
                freshCodes.Add(code);
            }
        }
        return freshCodes.Count;
    }

    protected long CountAllFreshProductCodes()
    {
        var sorted = Ranges.OrderBy(r => r.Start).ToList();
        
        var merged = new List<(long Start, long End)>();
        foreach (var range in sorted)
        {
            if (merged.Count == 0 || merged[^1].End < range.Start - 1)
            {
                merged.Add(range);
            }
            else
            {
                var (Start, End) = merged[^1];
                merged[^1] = (Start, Math.Max(End, range.End));
            }
        }

        long total = 0;
        foreach (var (Start, End) in merged)
        {
            total += End - Start + 1;
        }

        return total;
    }
}
