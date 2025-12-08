using System.Text;

namespace Challenge2025.Day06;

internal abstract class DayBase : ProblemBase
{
    protected List<(char Operator, List<long> ColumnValues)> TrimmedColumns = [];

    protected List<(char Operator, List<long> GroupValues)> DenseGroups = [];

    protected void ParseInputs(string[] inputs)
    {
        var operatorLine = inputs[^1];
        var dataRows = inputs[..^1];

        // Find operator positions and their column spans
        var columnSpans = ParseOperatorColumnSpans(operatorLine);

        // Parse Collection 1: Trimmed number grid
        TrimmedColumns = ParseTrimmedColumns(dataRows, columnSpans);

        // Parse Collection 2: Dense character columns grouped by whitespace
        DenseGroups = ParseDenseGroups(dataRows, columnSpans);

        Console.WriteLine($"Trimmed columns count: {TrimmedColumns.Count}");
        Console.WriteLine($"Dense groups count: {DenseGroups.Count}");
    }

    private static List<(char Operator, int StartCol, int EndCol)> ParseOperatorColumnSpans(string operatorLine)
    {
        var spans = new List<(char Operator, int StartCol, int EndCol)>();

        int i = 0;
        while (i < operatorLine.Length)
        {
            while (i < operatorLine.Length && char.IsWhiteSpace(operatorLine[i]))
            {
                i++;
            }

            if (i >= operatorLine.Length)
            {
                break;
            }

            char op = operatorLine[i];
            int startCol = i;

            i++;
            while (i < operatorLine.Length && char.IsWhiteSpace(operatorLine[i]))
            {
                i++;
            }

            int endCol = (i < operatorLine.Length) ? i : operatorLine.Length;

            spans.Add((op, startCol, endCol));
        }

        return spans;
    }

    private static List<(char Operator, List<long> ColumnValues)> ParseTrimmedColumns(string[] dataRows, List<(char Operator, int StartCol, int EndCol)> columnSpans)
    {
        var result = new List<(char Operator, List<long> ColumnValues)>();

        foreach (var (op, startCol, endCol) in columnSpans)
        {
            var columnValues = new List<long>();

            foreach (var row in dataRows)
            {
                int actualEnd = Math.Min(endCol, row.Length);
                int actualStart = Math.Min(startCol, row.Length);

                if (actualStart >= row.Length)
                {
                    columnValues.Add(0);
                    continue;
                }

                var cellValue = row[actualStart..actualEnd].Trim();
                if (string.IsNullOrEmpty(cellValue))
                {
                    columnValues.Add(0);
                }
                else
                {
                    columnValues.Add(long.Parse(cellValue));
                }
            }

            result.Add((op, columnValues));
        }

        return result;
    }

    private static List<(char Operator, List<long> GroupValues)> ParseDenseGroups(string[] dataRows, List<(char Operator, int StartCol, int EndCol)> columnSpans)
    {
        var result = new List<(char Operator, List<long> GroupValues)>();

        foreach (var (op, startCol, endCol) in columnSpans)
        {
            int spanWidth = endCol - startCol;
            var charColumns = new List<List<char>>();

            for (int colOffset = 0; colOffset < spanWidth; colOffset++)
            {
                var charColumn = new List<char>();
                int absoluteCol = startCol + colOffset;

                foreach (var row in dataRows)
                {
                    if (absoluteCol < row.Length)
                        charColumn.Add(row[absoluteCol]);
                    else
                        charColumn.Add(' ');
                }

                charColumns.Add(charColumn);
            }

            var groups = GroupColumnsByWhitespace(charColumns);

            var groupValues = new List<long>();
            foreach (var group in groups)
            {
                foreach (var column in group)
                {
                    var number = ConstructNumberFromColumn(column);
                    groupValues.Add(number);
                }
            }

            result.Add((op, groupValues));
        }

        return result;
    }

    private static List<List<List<char>>> GroupColumnsByWhitespace(List<List<char>> charColumns)
    {
        var groups = new List<List<List<char>>>();
        var currentGroup = new List<List<char>>();

        foreach (var column in charColumns)
        {
            bool isWhitespaceColumn = column.All(char.IsWhiteSpace);

            if (isWhitespaceColumn)
            {
                if (currentGroup.Count > 0)
                {
                    groups.Add(currentGroup);
                    currentGroup = [];
                }
            }
            else
            {
                currentGroup.Add(column);
            }
        }

        if (currentGroup.Count > 0)
        {
            groups.Add(currentGroup);
        }

        return groups;
    }

    private static long ConstructNumberFromColumn(List<char> column)
    {
        var digits = new StringBuilder();

        foreach (var ch in column)
        {
            if (!char.IsWhiteSpace(ch))
            {
                digits.Append(ch);
            }
        }

        if (digits.Length == 0)
        {
            return 0;
        }

        var result = long.Parse(digits.ToString());
        return result;
    }

    protected long CalculateTrimmedGridValue()
    {
        var accumulator = 0L;

        foreach (var (op, columnValues) in TrimmedColumns)
        {
            var columnResult = ApplyOperation(op, columnValues);
            accumulator += columnResult;
        }

        return accumulator;
    }

    protected long CalculateDenseGridValue()
    {
        var accumulator = 0L;

        foreach (var (op, groupValues) in DenseGroups)
        {
            var groupResult = ApplyOperation(op, groupValues);
            accumulator += groupResult;
        }

        return accumulator;
    }

    protected static long ApplyOperation(char symbol, IEnumerable<long> numbers)
    {
        return symbol switch
        {
            '+' => numbers.Sum(),
            '*' => numbers.Aggregate(1L, (acc, n) => acc * n),
            _ => throw new InvalidOperationException($"Unknown operation symbol: {symbol}"),
        };
    }
}
