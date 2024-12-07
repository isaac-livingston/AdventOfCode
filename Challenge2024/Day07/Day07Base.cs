namespace Challenge2024.Day07;

internal class Day07Base : ProblemBase
{
    public List<(long result, int[] terms)> ProblemSet { get; } = [];
    public Dictionary<(long result, int[] terms), long> ValidResultCounts { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        foreach (var input in inputs)
        {
            var parts = input.Split(":", SPLIT_OPTS);
            
            var result = Convert.ToInt64(parts[0]);

            var terms = parts[1].Split(" ", SPLIT_OPTS)
                                .Select(x => Convert.ToInt32(x))
                                .ToArray();

            ProblemSet.Add((result, terms));
        }
    }

    public void EvaluateProblems()
    {
        foreach (var (result, terms) in ProblemSet)
        {
            long validCount = FindValidResults(terms, 0, terms[0], result);
            ValidResultCounts[(result, terms)] = validCount;
        }
    }

    private static long FindValidResults(int[] terms, long index, long currentValue, long target)
    {
        if (index == terms.Length - 1)
        {
            var result = currentValue == target 
                       ? 1 
                       : 0;

            return result;
        }

        long nextIndex = index + 1;
        long nextTerm = terms[nextIndex];

        long addCount = FindValidResults(terms, nextIndex, currentValue + nextTerm, target);
        long mulCount = FindValidResults(terms, nextIndex, currentValue * nextTerm, target);

        return addCount + mulCount;
    }
}
