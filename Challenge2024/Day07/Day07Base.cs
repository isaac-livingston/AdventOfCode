namespace Challenge2024.Day07;

internal class Day07Base : ProblemBase
{
    public List<(long target, int[] terms)> ProblemSet { get; } = [];
    public Dictionary<(long target, int[] terms), long> ValidResults { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        foreach (var input in inputs)
        {
            var parts = input.Split(":", SPLIT_OPTS);
            
            var target = Convert.ToInt64(parts[0]);

            var terms = parts[1].Split(" ", SPLIT_OPTS)
                                .Select(x => Convert.ToInt32(x))
                                .ToArray();

            ProblemSet.Add((target, terms));
        }
    }

    public void EvaluateProblems(List<(long target, int[] terms)> problemSet, bool evalConcat = false)
    {
        foreach (var (target, terms) in problemSet)
        {
            long validCount = evalConcat
                            ? (FindValidResultsWithConcat(terms, 0, terms[0], target, 0) ? 1 : 0)
                            : (FindValidResults(terms, 0, terms[0], target) ? 1 : 0);

            if (validCount > 0)
            {
                Console.WriteLine($"Valid Result: {target}: {string.Join(" ", terms)} => {validCount}");
                ValidResults[(target, terms)] = validCount;
            }
        }
    }

    private static bool FindValidResults(int[] terms, long index, long currentValue, long target)
    {
        if (index == terms.Length - 1)
        {
            var result = currentValue == target;

            return result;
        }

        long nextIndex = index + 1;
        long nextTerm = terms[nextIndex];

        if (FindValidResults(terms, nextIndex, currentValue + nextTerm, target))
            return true;

        if (FindValidResults(terms, nextIndex, currentValue * nextTerm, target))
            return true;

        return false;
    }

    private bool FindValidResultsWithConcat(int[] terms, int index, long currentValue, long target, long concatenated)
    {
        // Early exit: Stop if intermediate results exceed the target
        if (currentValue > target && concatenated > target)
        {
            return false;
        }

        // Base case: All terms processed
        if (index == terms.Length - 1)
        {
            // If either the current value or a valid concatenated value matches the target, return true
            return currentValue == target || (concatenated != 0 && concatenated == target);
        }

        int nextIndex = index + 1;
        long nextTerm = terms[nextIndex];

        //// Check addition
        //if (FindValidResultsWithConcat(terms, nextIndex, currentValue + nextTerm, target, concatenated))
        //    return true;

        //// Check multiplication
        //if (FindValidResultsWithConcat(terms, nextIndex, currentValue * nextTerm, target, concatenated))
        //    return true;

        // Check concatenation of terms directly
        long directConcat = concatenated == 0 ? nextTerm : ConcatenateNumbers(concatenated, nextTerm);
        if (FindValidResultsWithConcat(terms, nextIndex, currentValue, target, directConcat))
            return true;

        // Check addition of a concatenated result
        long concatAndAdd = concatenated == 0 ? nextTerm : ConcatenateNumbers(concatenated, nextTerm);
        if (FindValidResultsWithConcat(terms, nextIndex, currentValue + concatAndAdd, target, 0))
            return true;

        // Check multiplication of a concatenated result
        long concatAndMultiply = concatenated == 0 ? nextTerm : ConcatenateNumbers(concatenated, nextTerm);
        if (FindValidResultsWithConcat(terms, nextIndex, currentValue * concatAndMultiply, target, 0))
            return true;

        // Check concatenation of intermediate result with next term
        long intermediateConcat = ConcatenateNumbers(currentValue, nextTerm);
        if (FindValidResultsWithConcat(terms, nextIndex, intermediateConcat, target, concatenated))
            return true;

        return false;
    }

    private readonly Dictionary<(long num1, long num2), long> concatMemo = [];

    private long ConcatenateNumbers(long num1, long num2)
    {
        if(concatMemo.TryGetValue((num1, num2), out long result))
        {
            return result;
        }

        long multiplier = 1;
        while (multiplier <= num2) multiplier *= 10;

        result = num1 * multiplier + num2;
        concatMemo[(num1, num2)] = result;
        return result;
    }
}
