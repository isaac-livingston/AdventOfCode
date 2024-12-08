namespace Challenge2024.Day07;

internal static class Operators
{
    public static long Multiply(long a, long b) => a * b;

    public static long Add(long a, long b) => a + b;

    public static long Concatinate(long a, long b)
    {
        var result = a * (long)Math.Pow(10, (long)Math.Log10(b) + 1L) + b;

        return result;
    }
}
