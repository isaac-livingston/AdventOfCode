using Challenge.Common;

namespace Challenge2023.Day12;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day12";

    protected bool Part2 = false;

    protected int CountArrangements(string row)
    {
        var parts = row.Split(' ');
        string pattern = !Part2 ? parts[0] : $"{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}?{parts[0]}";
        string groupData = !Part2 ? parts[1] : $"{parts[1]},{parts[1]},{parts[1]},{parts[1]},{parts[1]}";
        int[] groupSizes = groupData.Split(',').Select(int.Parse).ToArray();

        var validCombinations = FindValidCombinations(pattern, groupSizes);
        return validCombinations.Count;
    }

    private static List<string> FindValidCombinations(string pattern, int[] groupSizes)
    {
        var combinations = GenerateCombinations(pattern);
        return combinations.Where(combination => IsValidCombination(combination, groupSizes)).ToList();
    }

    private static List<string> GenerateCombinations(string pattern)
    {
        return GenerateCombinationsRecursive(pattern, 0, "");
    }

    private static List<string> GenerateCombinationsRecursive(string pattern, int index, string current)
    {
        if (index == pattern.Length)
        {
            return [current];
        }

        var results = new List<string>();
        if (pattern[index] == '?')
        {
            results.AddRange(GenerateCombinationsRecursive(pattern, index + 1, current + '#'));
            results.AddRange(GenerateCombinationsRecursive(pattern, index + 1, current + '.'));
        }
        else
        {
            results.AddRange(GenerateCombinationsRecursive(pattern, index + 1, current + pattern[index]));
        }

        return results;
    }

    private static bool IsValidCombination(string combination, int[] groupSizes)
    {
        List<int> counts = [];
        int currentCount = 0;
        foreach (char c in combination)
        {
            if (c == '#')
            {
                currentCount++;
            }
            else if (currentCount > 0)
            {
                counts.Add(currentCount);
                currentCount = 0;
            }
        }
        if (currentCount > 0)
        {
            counts.Add(currentCount);
        }

        return counts.SequenceEqual(groupSizes);
    }
}
