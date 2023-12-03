using Challenge2023.Common;

namespace Challenge2023.Day01
{
    internal class Problem2 : ProblemBase
    {
        enum NumberValues
        {
            one = 1,
            two = 2,
            three = 3,
            four = 4,
            five = 5,
            six = 6,
            seven = 7,
            eight = 8,
            nine = 9
        }

        List<int> DecodedResults { get; set; } = [];

        static int? TryToGetDigit(string input, int start, int end)
        {
            var sample = string.Join(null, input.Skip(start).Take(end));

            if (Enum.TryParse<NumberValues>(sample, out var numberValue))
            {
                return (int)numberValue;
            }

            return null;
        }

        static InvalidOperationException HandleUnsupportedInput() => new("No digit was found in the input string, this is not expected.");

        static int SeekLeftDigit(string input, int maxSegment)
        {
            var start = 0;

            while (start < input.Length)
            {
                for (var i = 1; i < maxSegment; i++)
                {
                    var parsed = TryToGetDigit(input, start, i);
                    if (parsed != null)
                    {
                        return parsed.GetValueOrDefault();
                    }
                }
                start++;
            }

            throw HandleUnsupportedInput();
        }

        static int SeekRightDigit(string input, int maxSegment)
        {
            var start = input.Length;

            while (0 < start)
            {
                for (var i = 1; i < maxSegment; i++)
                {
                    var parsed = TryToGetDigit(input, start - i, i);
                    if (parsed != null)
                    {
                        return parsed.GetValueOrDefault();
                    }
                }
                start--;
            }

            throw HandleUnsupportedInput();
        }

        static int GetDecodedTwoDigitValue(string input)
        {
            var max = Enum.GetNames<NumberValues>().Max(x => x.Length);

            var left = SeekLeftDigit(input, max);
            var right = SeekRightDigit(input, max);

            return left * 10 + right;
        }

        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day01");

            for (int i = 0; i < inputs.Length; i++)
            {
                var inputString = inputs[i];

                var decodeResult = GetDecodedTwoDigitValue(inputString);

                DecodedResults.Add(decodeResult);
            }

            Console.WriteLine(DecodedResults.Sum());
        }
    }
}
