namespace Challenge2025.Day03;

internal abstract class DayBase : ProblemBase
{
    protected List<long> PowerBankJoltages = [];

    protected void ParseInputs(string[] inputs, bool greatScott = false)
    {
        int batteriesToKeep = greatScott ? 12 : 2;
        //char 0 to 9 order just like int, no need to parse early
        PowerBankJoltages = [.. inputs.Select(bank => PowerBankJoltage(bank, batteriesToKeep))];
    }

    protected static long PowerBankJoltage(string powerBank, int batteriesToKeep)
    {
        Span<char> result = stackalloc char[batteriesToKeep];
        int startFrom = 0;

        for (int i = 0; i < batteriesToKeep; i++)
        {
            int digitsNeeded = batteriesToKeep - i - 1;
            int lastValidIndex = powerBank.Length - digitsNeeded - 1;

            int bestIndex = startFrom;
            for (int j = startFrom; j <= lastValidIndex; j++)
            {
                if (powerBank[j] > powerBank[bestIndex])
                {
                    bestIndex = j;
                }
            }

            result[i] = powerBank[bestIndex];
            startFrom = bestIndex + 1;
        }

        return long.Parse(result);
    }
}
