using System.Text;

namespace AdventOfCode.Day01
{
    internal class Problem2 : IProblem
    {
        static string[] GetInputs()
        {
            return File.ReadAllLines(@"input.txt", Encoding.UTF8);
        }

        static Dictionary<string, int> LookupTable => new()
        {
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            { "1", 1 },
            { "2", 2 },
            { "3", 3 },
            { "4", 4 },
            { "5", 5 },
            { "6", 6 },
            { "7", 7 },
            { "8", 8 },
            { "9", 9 },
        };

        List<int> DecodedResults { get; set; } = [];

        static int? TryToGetDigit(string input, int start, int end)
        {
            var sample = string.Join(null, input.Skip(start).Take(end));

            if (LookupTable.TryGetValue(sample, out int value))
            {
                return value;
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
            var max = LookupTable.Keys.Select(x => x.Length).Max() + 1;

            var left = SeekLeftDigit(input, max);
            var right = SeekRightDigit(input, max);

            return left * 10 + right;
        }

        public void RunSolution(params object[] vars)
        {
            var inputs = GetInputs();

            var sb = new StringBuilder();

            for (int i = 0; i < inputs.Length; i++)
            {
                var inputString = inputs[i];

                var decodeResult = GetDecodedTwoDigitValue(inputString);

                DecodedResults.Add(decodeResult);

                sb.AppendLine($"{inputString}\t{decodeResult}");
            }

            File.WriteAllText(@"I:\Projects\Insight\Development\2023\AdventOfCode_2023\data\day01\output.txt", sb.ToString());

            Console.WriteLine(DecodedResults.Sum());
        }
    }
}
