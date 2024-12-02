using Challenge.Common;
using System.Text;
using System.Text.RegularExpressions;

namespace Challenge2023.Day12
{
    /**
     * This is a clone of...
     * https://github.com/rtrinh3/AdventOfCode/blob/2139838074181b65b0ab98a2ce4c53107e3e6b69/Aoc2023/Day12.cs
    **/

    internal abstract class DayBaseXII : ProblemBase
    {
        protected const string DAY_FOLDER = "Day12";

        private record Row(string mask, int[] runs);
        private enum CellType { DOT, POUND, FLEX };
        private record CellRun(CellType type, int length);

        private Row[] Records;

        protected void LoadData(string[] inputs)
        {
            var rows = new List<Row>();

            for(int i = 0; i < inputs.Length; i++)
            {
                var parts = inputs[i].Split(' ');
                var runs = parts[1].Split(',').Select(int.Parse).ToArray();
                rows.Add(new Row(parts[0], runs));
            }

            Records = [.. rows];
        }

        protected long Part2()
        {
            var unfoldedRecords = Records.Select(row =>
            {
                string unfoldedMask = string.Join('?', Enumerable.Repeat(row.mask, 5));
                int[] unfoldedRuns = [.. row.runs, .. row.runs, .. row.runs, .. row.runs, .. row.runs];
                return new Row(unfoldedMask, unfoldedRuns);
            }).ToArray();
            return unfoldedRecords.Sum(DoPuzzle);
        }

        private long DoPuzzle(Row row)
        {
            var expandedRuns = new List<CellRun>
            {
                new(CellType.FLEX, 0)
            };

            for (int i = 0; i < row.runs.Length - 1; i++)
            {
                expandedRuns.Add(new CellRun(CellType.POUND, row.runs[i]));
                expandedRuns.Add(new CellRun(CellType.DOT, 1));
                expandedRuns.Add(new CellRun(CellType.FLEX, 0));
            }

            expandedRuns.Add(new CellRun(CellType.POUND, row.runs.Last()));
            expandedRuns.Add(new CellRun(CellType.FLEX, 0));

            Func<int, int, Regex> GetValidator = Memoization.Make((int startIndex, int length) =>
            {
                var patternBuilder = new StringBuilder(@"^");
                string substr = row.mask.Substring(startIndex, length);
                var escapes = substr.Select(c =>
                {
                    return c switch
                    {
                        '.' => @"\.",
                        '#' => @"#",
                        '?' => @".",
                        _ => throw new Exception("What is this mask")
                    };
                });
                patternBuilder.AppendJoin("", escapes);
                patternBuilder.Append('$');
                string pattern = patternBuilder.ToString();
                return new Regex(pattern);
            });

            Func<int, int, long> DoPuzzleRecurse = (x, y) => throw new NotImplementedException("Stub for recursive function");
            
            DoPuzzleRecurse = Memoization.Make((int maskIndex, int runIndex) =>
            {
                
                if (runIndex == expandedRuns.Count)
                {
                    return (maskIndex == row.mask.Length) ? 1 : 0;
                }
                
                var run = expandedRuns[runIndex];
                
                char runChar = run.type switch
                {
                    CellType.DOT => '.',
                    CellType.POUND => '#',
                    CellType.FLEX => '.',
                    _ => throw new Exception("What is this cell")
                };
                
                IEnumerable<int> runLengths = run.type switch
                {
                    CellType.DOT => [run.length],
                    CellType.POUND => [run.length],
                    CellType.FLEX => Enumerable.Range(0, 1 + row.mask.Length - maskIndex - expandedRuns.Skip(runIndex).Sum(r => r.length)),
                    _ => throw new Exception("What is this cell")
                };
                
                long sum = 0;
                
                foreach (var len in runLengths)
                {
                    string runString = string.Join("", Enumerable.Repeat(runChar, len));
                    var validator = GetValidator(maskIndex, len);
                    if (validator.IsMatch(runString))
                    {
                        sum += DoPuzzleRecurse(maskIndex + len, runIndex + 1);
                    }
                }

                return sum;
            });

            var answer = DoPuzzleRecurse(0, 0);
            return answer;
        }
    }
}
