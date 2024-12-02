using Challenge.Common;
using Challenge2023.Day13.Models;

namespace Challenge2023.Day13;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day13";

    protected List<Pattern> Patterns { get; private set; } = [];

    protected void LoadData(string[] inputs)
    {
        var patternData = new List<string>();

        for (int i = 0; i < inputs.Length; i++)
        {
            if (inputs[i].Length > 0)
            {
                patternData.Add(inputs[i]);
            }
            else
            {
                Patterns.Add(new Pattern([.. patternData]));
                patternData.Clear();
            }
        }

        Patterns.Add(new Pattern([.. patternData]));
    }

    protected int AnalyzePatterns()
    {
        var tally = 0;

        foreach (var pattern in Patterns)
        {
            var h = GetHorizontalReflectionPosition(pattern);
            var v = 0;

            if (h == 0)
            {
                v = GetVerticalReflectionPosition(pattern);
            }
             

            tally += h + v;
        }

        return tally;
    }

    private static int GetHorizontalReflectionPosition(Pattern pattern)
    {
        var reflectedAt = ReflectionTest(pattern.HorizontalLines);

        reflectedAt *= 100;

        return reflectedAt;
    }

    private static int GetVerticalReflectionPosition(Pattern pattern)
    {
        var reflectedAt = ReflectionTest(pattern.VerticalLines);

        return reflectedAt;
    }

    private static int ReflectionTest(string[] data)
    {
        var reflectionStarts = -1;

        for (int x1 = 0; x1 < data.Length - 1; x1++)
        {
            var x2 = x1 + 1;

            var s0 = data[x1];
            var s1 = data[x2];

            if (s0 == s1)
            {
                reflectionStarts = x1;
                break;
            }
        }

        if (reflectionStarts < 0)
        {
            return 0;
        }

        var reflectionCount = 0;
        var offset = reflectionStarts;

        for (int x1 = reflectionStarts; x1 >= 0; x1--)
        {
            offset++;

            if (offset == data.Length)
            { 
                break;
            }

            var s0 = data[x1];
            var s1 = data[offset];

            if (s0 == s1)
            {
                reflectionCount += 2;
            }
        }

        if (reflectionCount == data.Length || reflectionCount == data.Length - 1)
        {
            return reflectionStarts + 1;
        }

        return 0;
    }
}
