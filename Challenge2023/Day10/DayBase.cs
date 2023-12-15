using Challenge2023.Common;
using System.Collections.Frozen;
using System.Text;

namespace Challenge2023.Day10;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day10";

    protected const int NORTH = 1 << 3;
    protected const int SOUTH = 1 << 2;
    protected const int EAST = 1 << 1;
    protected const int WEST = 1 << 0;

    protected int[] StartingCell = new int[2];

    protected List<int> Moves = [];

    protected FrozenDictionary<char, int> Keys = new Dictionary<char, int>()
        {
            { '.', 0 },
            { '|', NORTH | SOUTH },
            { '-', EAST | WEST},
            { 'L', NORTH | EAST },
            { 'J', NORTH | WEST},
            { '7', SOUTH | WEST },
            { 'F', SOUTH | EAST },
            { 'S', SOUTH | EAST },
        }.ToFrozenDictionary();

    protected int[][] Grid = [];

    protected void LoadData(string[] inputs)
    {
        //inputs = expandForProblem2 ? GetExpandedInputs(inputs) : inputs;

        var rows = inputs.Length;

        Grid = new int[rows][];

        for (int r = 0; r < rows; r++)
        {
            var symbols = inputs[r].ToCharArray();

            Grid[r] = new int[symbols.Length];
            for (int c = 0; c < symbols.Length; c++)
            {
                var currentSymbol = symbols[c];
                if (currentSymbol == 'S')
                {
                    StartingCell[0] = r;
                    StartingCell[1] = c;
                }

                Grid[r][c] = Keys[currentSymbol];
            }
        }
    }

    static int RowOffset(int direction) => direction switch
    {
        NORTH => -1,
        SOUTH => 1,
        EAST => 0,
        WEST => 0,
        _ => throw new InvalidOperationException("Invalid direction")
    };

    static int ColOffset(int direction) => direction switch
    {
        EAST => 1,
        WEST => -1,
        NORTH => 0,
        SOUTH => 0,
        _ => throw new InvalidOperationException("Invalid direction")
    };

    static int ComingFrom(int direction) => direction switch
    {
        EAST => WEST,
        NORTH => SOUTH,
        WEST => EAST,
        SOUTH => NORTH,
        _ => throw new InvalidOperationException("Invalid direction")
    };

    protected static string RCNotation(int r, int c) => $"r{r}c{c}";

    protected Dictionary<string, long> TraverseNetwork(int comingInFrom)
    {
        var row = StartingCell[0];
        var col = StartingCell[1];

        var steps = 0L;
        var record = new Dictionary<string, long> {
                { RCNotation(row, col), 0 }
            };
        while (true)
        {
            var cell = Grid[row][col];
            var move = cell ^ comingInFrom;
            row += RowOffset(move);
            col += ColOffset(move);

            comingInFrom = ComingFrom(move);

            record[RCNotation(row, col)] = steps;

            if (row == StartingCell[0] && col == StartingCell[1])
            {
                break;
            }
            steps++;
        }

        return record;
    }

    //protected static bool IsPointInsidePolygon(double[] x, double[] y, double testX, double testY)
    //{
    //    int n = x.Length;
    //    bool inside = false;

    //    for (int i = 0, j = n - 1; i < n; j = i++)
    //    {
    //        if (((y[i] > testY) != (y[j] > testY)) &&
    //            (testX < (x[j] - x[i]) * (testY - y[i]) / (y[j] - y[i]) + x[i]))
    //        {
    //            inside = !inside;
    //        }
    //    }

    //    return inside;
    //}

    //private string[] GetExpandedInputs(string[] inputs)
    //{
    //    var expandedInputs = new List<string>();

    //    for (int i = 0; i < inputs.Length; i++)
    //    {
    //        var input = inputs[i];
    //        var sb = new StringBuilder();
    //        foreach (char ch in input)
    //        {
    //            sb.Append(ch);
    //            sb.Append('-');
    //            sb.Append('-');
    //        }

    //        var expandedInput = sb.ToString();

    //        expandedInputs.Add(expandedInput);
    //        expandedInputs.Add("".PadLeft(expandedInput.Length, '|'));
    //        expandedInputs.Add("".PadLeft(expandedInput.Length, '|'));
    //    }

    //    return [.. expandedInputs];
    //}
}

