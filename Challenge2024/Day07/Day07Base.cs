namespace Challenge2024.Day07;

using OperationFunction = Func<long, long, long>;

internal class Day07Base : ProblemBase
{
    public List<(long target, int[] terms)> ProblemSet { get; } = [];

    public Dictionary<(long target, int[] terms), long> ValidResults { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        foreach (var input in inputs)
        {
            var parts = input.Split(':', StringSplitOptions.RemoveEmptyEntries);

            var target = Convert.ToInt64(parts[0]);
            var terms = parts[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();

            ProblemSet.Add((target, terms));
        }
    }

    public void EvaluateProblems(List<(long answer, int[] terms)> problemSet, OperationFunction[] operations)
    {
        foreach (var (answer, terms) in problemSet)
        {
            var isValid = Find(terms, operations, 0, 0, answer, operations[0]);

            if (isValid)
            {
                ValidResults[(answer, terms)] = 1;
            }
        }
    }

    private static bool Find(int[] terms, OperationFunction[] operations, int index, long accumulator, long answer, OperationFunction operation)
    {
        if (accumulator > answer)
        {
            return false;
        }

        if (index == terms.Length)
        {
            return accumulator == answer;
        }

        var value = operation(accumulator, terms[index]);

        var result = false;

        for (int op = 0; !result && op < operations.Length; op++)
        {
            result |= Find(terms, operations, index + 1, value, answer, operations[op]);
        }

        return result;
    }
}
