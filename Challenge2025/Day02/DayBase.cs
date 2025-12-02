using System.Collections.ObjectModel;

namespace Challenge2025.Day02;

internal abstract class DayBase : ProblemBase
{
    protected Collection<long> InvalidCodes = [];

    protected void ParseInputs(string[] inputs, bool limitCheckToMod2 = false)
    {
        var chunks = inputs[0].Split(',');

        foreach (var chunk in chunks)
        {
            var m = chunk.Split('-');
            var a = long.Parse(m[0]);
            var b = long.Parse(m[1]);

            for (long i = a; i <= b; i++)
            {
                if (IsInvalidCode(i, limitCheckToMod2))
                {
                    InvalidCodes.Add(i);
                }
            }
        }
    }

    protected static bool IsInvalidCode(long code, bool limitCheckToMod2 = false)
    {
        var codeStr = code.ToString();

        var divisors = limitCheckToMod2 
                     ? [2] 
                     : GetDivisors(codeStr.Length);

        foreach (var divisor in divisors)
        {
            if (divisor == 1)
            {
                continue;
            }

            if (codeStr.Length % divisor != 0)
            {
                continue;
            }

            var chunkSize = codeStr.Length / (int)divisor;

            bool isInvalid = true;
            for (int i = 1; i < (int)divisor && isInvalid; i++)
            {
                for (int j = 0; j < chunkSize; j++)
                {
                    if (codeStr[j] != codeStr[i * chunkSize + j])
                    {
                        isInvalid = false;
                        break;
                    }
                }
            }

            if (isInvalid)
            {
                return true;
            }
        }

        return false;
    }

    protected static IEnumerable<long> GetDivisors(long number)
    {
        for (long i = 1; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
            {
                yield return i;
                if (i != number / i)
                {
                    yield return number / i;
                }
            }
        }
    }
}
