namespace Challenge2024.Day05;

internal class Day05Base : ProblemBase
{
    public List<(int before, int after)> PageOrderRules { get; } = [];

    public List<int[]> UpdateRequests { get; } = [];

    public List<int[]> ValidRequests { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        for(var i = 0; i < inputs.Length; i++)
        {
            if (inputs[i].Contains('|'))
            {
                var parts = inputs[i].Split('|').Select(s => Convert.ToInt32(s)).ToArray();

                PageOrderRules.Add((parts[0], parts[1]));
            }
            else if (inputs[i].Contains(','))
            {
                var parts = inputs[i].Split(',').Select(s => Convert.ToInt32(s)).ToArray();
                UpdateRequests.Add(parts);
            }
            else
            {
                continue;
            }
        }
    }

    public void ValidateRequest(int[] request)
    {
        var ruleSet = PageOrderRules.Where(r => request.Contains(r.before) 
                                             && request.Contains(r.after))
                                    .ToList();

        if (ApplyRuleSetToRequest(request, ruleSet))
        {
            ValidRequests.Add(request);
        }
    }

    private bool ApplyRuleSetToRequest(int[] request, List<(int before, int after)> ruleSet)
    {
        var indexMap = request.Select((value, index) => (value, index))
                              .ToDictionary(x => x.value, x => x.index);

        foreach (var (before, after) in ruleSet)
        {
            if (indexMap[before] >= indexMap[after])
            {
                return false;
            }
        }

        return true;
    }

    public int GetMiddleValue(int[] numbers)
    {
        if (numbers == null || numbers.Length % 2 == 0)
        {
            throw new ArgumentException("Array must be non-null and have an odd count.");
        }

        int middleIndex = numbers.Length / 2;
        return numbers[middleIndex];
    }

    public override void RunSolution()
    {
        throw new NotImplementedException();
    }
}
