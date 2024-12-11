namespace Challenge2024.Day11;

internal class Day11Base : ProblemBase
{
    public Dictionary<long, long> StoneCounts { get; private set; } = new();

    public void ParseInputs(string[] inputs)
    {
        for (int i = 0; i < inputs.Length; i++)
        {
            var stones = inputs[i].Split(' ')
                                  .Select(long.Parse);

            foreach (var stone in stones)
            {
                if (StoneCounts.TryGetValue(stone, out long value))
                {
                    StoneCounts[stone] = ++value;
                }
                else
                {
                    StoneCounts[stone] = 1;
                }
            }
        }
    }

    public void ProcessStones()
    {
        var newStoneCounts = new Dictionary<long, long>();

        foreach (var (stone, count) in StoneCounts)
        {
            var newStones = StoneTools.Blink(stone);

            foreach (var newStone in newStones)
            {
                if (newStoneCounts.ContainsKey(newStone))
                {
                    newStoneCounts[newStone] += count;
                }
                else
                {
                    newStoneCounts[newStone] = count;
                }
            }
        }

        StoneCounts = newStoneCounts;
    }

    public void PrintStones()
    {
        foreach (var (stone, count) in StoneCounts)
        {
            Console.WriteLine($"{stone}: {count}");
        }
    }
}

internal static class StoneTools
{
    public static long[] Blink(long value)
    {
        if (value == 0)
        {
            return [1];
        }

        var (even, digits) = CheckIfNumberHasEvenDigits(value);

        if (even)
        {
            var (left, right) = SplitNumber(value, digits);
            return [left, right];
        }

        return [value * 2024];
    }

    public static (bool even, long digits) CheckIfNumberHasEvenDigits(long number)
    {
        long digits = (long)Math.Log10(number) + 1;

        if (digits % 2 != 0)
        {
            return (false, -1);
        }

        return (true, digits);
    }

    public static (long left, long right) SplitNumber(long number, long digits)
    {
        long halfDigits = digits / 2;
        long divisor = (long)Math.Pow(10, halfDigits);

        long left = number / divisor;
        long right = number % divisor;

        return (left, right);
    }
}
