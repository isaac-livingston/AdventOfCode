using System.Text.RegularExpressions;

namespace Challenge2025.Day10;

internal class Button(int index, int[] toggles)
{
    public int Index { get; } = index;
    public int[] Toggles { get; } = toggles;
    public int Presses { get; set; } = 0;

    public void Press() => Presses++;
    public void Reset() => Presses = 0;
}

internal class LightPanel(bool[] targetState, int[][] buttonToggles, int[] joltages)
{
    public bool[] TargetState { get; } = targetState;
    public int LightCount => TargetState.Length;
    public Button[] Buttons { get; } = [.. buttonToggles.Select((toggles, i) => new Button(i, toggles))];
    public int[] Joltages { get; } = joltages;

    public int MinimumPresses => Buttons.Sum(b => b.Presses);

    public void ResetButtons()
    {
        foreach (var button in Buttons)
        {
            button.Reset();
        }
    }

    /// <summary>
    /// Solve the light panel using Gaussian elimination over GF(2).
    /// Finds the solution with minimum number of button presses.
    /// Returns true if a solution exists.
    /// </summary>
    public bool Solve()
    {
        ResetButtons();

        int numLights = LightCount;
        int numButtons = Buttons.Length;

        // Build augmented matrix [A | b] where A is lights×buttons, b is target
        // matrix[light, button] = 1 if button toggles that light
        // Last column is the target state
        var matrix = new int[numLights, numButtons + 1];

        for (int btnIdx = 0; btnIdx < numButtons; btnIdx++)
        {
            foreach (int lightIdx in Buttons[btnIdx].Toggles)
            {
                matrix[lightIdx, btnIdx] = 1;
            }
        }

        // Set target column (rightmost)
        for (int lightIdx = 0; lightIdx < numLights; lightIdx++)
        {
            matrix[lightIdx, numButtons] = TargetState[lightIdx] ? 1 : 0;
        }

        // Gaussian elimination over GF(2) - get to reduced row echelon form
        int pivotRow = 0;
        var pivotCols = new List<int>();  // Columns that have pivots (basic variables)
        var freeCols = new List<int>();   // Columns without pivots (free variables)

        for (int col = 0; col < numButtons && pivotRow < numLights; col++)
        {
            // Find pivot row
            int pivot = -1;
            for (int row = pivotRow; row < numLights; row++)
            {
                if (matrix[row, col] == 1)
                {
                    pivot = row;
                    break;
                }
            }

            if (pivot == -1)
            {
                freeCols.Add(col);
                continue;
            }

            pivotCols.Add(col);

            // Swap rows
            if (pivot != pivotRow)
            {
                for (int c = 0; c <= numButtons; c++)
                {
                    (matrix[pivotRow, c], matrix[pivot, c]) = (matrix[pivot, c], matrix[pivotRow, c]);
                }
            }

            // Eliminate other rows (both above and below for RREF)
            for (int row = 0; row < numLights; row++)
            {
                if (row != pivotRow && matrix[row, col] == 1)
                {
                    for (int c = 0; c <= numButtons; c++)
                    {
                        matrix[row, c] ^= matrix[pivotRow, c];
                    }
                }
            }

            pivotRow++;
        }

        // Add remaining columns as free variables
        for (int col = pivotCols.Count > 0 ? pivotCols[^1] + 1 : 0; col < numButtons; col++)
        {
            if (!pivotCols.Contains(col))
            {
                freeCols.Add(col);
            }
        }

        // Check for inconsistency (row of zeros with 1 in target column)
        for (int row = 0; row < numLights; row++)
        {
            bool allZero = true;
            for (int col = 0; col < numButtons; col++)
            {
                if (matrix[row, col] == 1)
                {
                    allZero = false;
                    break;
                }
            }
            if (allZero && matrix[row, numButtons] == 1)
            {
                return false; // No solution
            }
        }

        // Find minimum weight solution by trying all combinations of free variables
        int[] bestSolution = null!;
        int bestWeight = int.MaxValue;

        int numFree = freeCols.Count;
        int combinations = 1 << numFree;  // 2^numFree combinations

        for (int mask = 0; mask < combinations; mask++)
        {
            var solution = new int[numButtons];

            // Set free variables based on mask
            for (int i = 0; i < numFree; i++)
            {
                solution[freeCols[i]] = (mask >> i) & 1;
            }

            // Back-substitute to find basic variables
            for (int row = pivotCols.Count - 1; row >= 0; row--)
            {
                int col = pivotCols[row];
                int val = matrix[row, numButtons];
                for (int c = col + 1; c < numButtons; c++)
                {
                    val ^= matrix[row, c] * solution[c];
                }
                solution[col] = val;
            }

            // Calculate weight (number of 1s)
            int weight = solution.Sum();
            if (weight < bestWeight)
            {
                bestWeight = weight;
                bestSolution = solution;
            }
        }

        // Record button presses from best solution
        for (int btnIdx = 0; btnIdx < numButtons; btnIdx++)
        {
            if (bestSolution[btnIdx] == 1)
            {
                Buttons[btnIdx].Press();
            }
        }

        return true;
    }

    /// <summary>
    /// Solve the joltage configuration problem using Gaussian elimination over rationals.
    /// Find minimum button presses such that each counter reaches its target joltage.
    /// </summary>
    public bool SolveJoltage()
    {
        ResetButtons();

        int numCounters = Joltages.Length;
        int numButtons = Buttons.Length;

        // Build augmented matrix [A | b] using fractions for exact arithmetic
        // Each row is a counter, each column is a button, last column is target
        var matrix = new Fraction[numCounters, numButtons + 1];
        for (int r = 0; r < numCounters; r++)
        {
            for (int c = 0; c <= numButtons; c++)
            {
                matrix[r, c] = new Fraction(0, 1);
            }
        }

        for (int btnIdx = 0; btnIdx < numButtons; btnIdx++)
        {
            foreach (int counterIdx in Buttons[btnIdx].Toggles)
            {
                if (counterIdx < numCounters)
                {
                    matrix[counterIdx, btnIdx] = new Fraction(1, 1);
                }
            }
        }

        for (int c = 0; c < numCounters; c++)
        {
            matrix[c, numButtons] = new Fraction(Joltages[c], 1);
        }

        // Gaussian elimination to RREF
        var pivotCols = new List<int>();
        var freeCols = new List<int>();
        int pivotRow = 0;

        for (int col = 0; col < numButtons && pivotRow < numCounters; col++)
        {
            // Find pivot
            int pivot = -1;
            for (int row = pivotRow; row < numCounters; row++)
            {
                if (!matrix[row, col].IsZero)
                {
                    pivot = row;
                    break;
                }
            }

            if (pivot == -1)
            {
                freeCols.Add(col);
                continue;
            }

            pivotCols.Add(col);

            // Swap rows
            if (pivot != pivotRow)
            {
                for (int c = 0; c <= numButtons; c++)
                {
                    (matrix[pivotRow, c], matrix[pivot, c]) = (matrix[pivot, c], matrix[pivotRow, c]);
                }
            }

            // Scale pivot row
            var scale = matrix[pivotRow, col];
            for (int c = 0; c <= numButtons; c++)
            {
                matrix[pivotRow, c] = matrix[pivotRow, c] / scale;
            }

            // Eliminate other rows
            for (int row = 0; row < numCounters; row++)
            {
                if (row != pivotRow && !matrix[row, col].IsZero)
                {
                    var factor = matrix[row, col];
                    for (int c = 0; c <= numButtons; c++)
                    {
                        matrix[row, c] = matrix[row, c] - factor * matrix[pivotRow, c];
                    }
                }
            }

            pivotRow++;
        }

        // Add remaining columns as free
        for (int col = 0; col < numButtons; col++)
        {
            if (!pivotCols.Contains(col) && !freeCols.Contains(col))
            {
                freeCols.Add(col);
            }
        }

        // Check for inconsistency
        for (int row = 0; row < numCounters; row++)
        {
            bool allZero = true;
            for (int col = 0; col < numButtons; col++)
            {
                if (!matrix[row, col].IsZero)
                {
                    allZero = false;
                    break;
                }
            }

            if (allZero && !matrix[row, numButtons].IsZero)
            {
                return false;
            }
        }

        // Now search for minimum non-negative integer solution
        // Basic variables are determined by free variables
        // We need to find integer free variable values that make all basic vars non-negative integers
        
        int[] bestSolution = null!;
        long bestTotal = long.MaxValue;

        // Determine reasonable bounds for free variables
        int maxFreeValue = Joltages.Max() + 1;

        SearchJoltageSolution(matrix, pivotCols, freeCols, numButtons, new int[freeCols.Count], 0, maxFreeValue, ref bestSolution, ref bestTotal);

        if (bestSolution == null)
        {
            return false;
        }

        for (int btnIdx = 0; btnIdx < numButtons; btnIdx++)
        {
            Buttons[btnIdx].Presses = bestSolution[btnIdx];
        }

        return true;
    }

    private static void SearchJoltageSolution(Fraction[,] matrix, 
                                              List<int> pivotCols, 
                                              List<int> freeCols,
                                              int numButtons, 
                                              int[] freeVals,
                                              int freeIdx,
                                              int maxFreeValue,
                                              ref int[] bestSolution,
                                              ref long bestTotal)
    {
        if (freeIdx == freeCols.Count)
        {
            // Compute full solution from free variable values
            var solution = new int[numButtons];
            
            // Set free variables
            for (int i = 0; i < freeCols.Count; i++)
            {
                solution[freeCols[i]] = freeVals[i];
            }

            // Compute basic variables via back-substitution
            for (int row = pivotCols.Count - 1; row >= 0; row--)
            {
                int col = pivotCols[row];
                var val = matrix[row, numButtons];
                for (int c = col + 1; c < numButtons; c++)
                {
                    val -= matrix[row, c] * new Fraction(solution[c], 1);
                }

                // Check if integer and non-negative
                if (!val.IsInteger || val.IsNegative)
                {
                    return;
                }

                solution[col] = (int)val.ToLong();
            }

            // Check all values are non-negative
            for (int i = 0; i < numButtons; i++)
            {
                if (solution[i] < 0)
                {
                    return;
                }
            }

            long total = solution.Sum(x => (long)x);
            if (total < bestTotal)
            {
                bestTotal = total;
                bestSolution = (int[])solution.Clone();
            }

            return;
        }

        // Pruning: current sum of free vars
        long currentFreeSum = 0;
        for (int i = 0; i < freeIdx; i++)
        {
            currentFreeSum += freeVals[i];
        }

        if (currentFreeSum >= bestTotal)
        {
            return;
        }

        // Try values for this free variable
        for (int val = 0; val <= maxFreeValue; val++)
        {
            if (currentFreeSum + val >= bestTotal)
            {
                break;
            }

            freeVals[freeIdx] = val;

            SearchJoltageSolution(matrix, pivotCols, freeCols, numButtons, 
                freeVals, freeIdx + 1, maxFreeValue, ref bestSolution, ref bestTotal);
        }

        freeVals[freeIdx] = 0;
    }
}

/// <summary>
/// Simple fraction class for exact rational arithmetic
/// </summary>
internal readonly struct Fraction
{
    public long Num { get; }
    public long Den { get; }

    public Fraction(long num, long den)
    {
        if (den == 0)
        {
            throw new DivideByZeroException();
        }

        if (den < 0) 
        { 
            num = -num; den = -den; 
        }

        long g = GCD(Math.Abs(num), den);

        Num = num / g;
        Den = den / g;
    }

    public bool IsZero => Num == 0;
    public bool IsNegative => Num < 0;
    public bool IsInteger => Den == 1;
    public long ToLong() => Num / Den;

    public static Fraction operator +(Fraction a, Fraction b) =>
        new(a.Num * b.Den + b.Num * a.Den, a.Den * b.Den);

    public static Fraction operator -(Fraction a, Fraction b) =>
        new(a.Num * b.Den - b.Num * a.Den, a.Den * b.Den);

    public static Fraction operator *(Fraction a, Fraction b) =>
        new(a.Num * b.Num, a.Den * b.Den);

    public static Fraction operator /(Fraction a, Fraction b) =>
        new(a.Num * b.Den, a.Den * b.Num);

    private static long GCD(long a, long b) => b == 0 
                                             ? a 
                                             : GCD(b, a % b);
}

internal abstract partial class DayBase : ProblemBase
{
    protected List<LightPanel> Panels = [];

    protected void ParseInputs(string[] inputs)
    {
        Panels = [.. inputs.Select(ParseLine)];
    }

    private static LightPanel ParseLine(string line)
    {
        // Parse target state from [...]
        var bracketMatch = BracketRegex().Match(line);
        var targetStr = bracketMatch.Groups[1].Value;

        var targetState = targetStr.Select(c => c == '#')
                                   .ToArray();

        // Parse buttons from (...)
        var buttonMatches = ParenRegex().Matches(line);

        var buttonToggles = buttonMatches.Select(m => m.Groups[1].Value.Split(',').Select(int.Parse).ToArray())
                                         .ToArray();

        // Parse joltages from {...}
        var braceMatch = BraceRegex().Match(line);

        var joltages = braceMatch.Groups[1].Value.Split(',')
                                                 .Select(int.Parse)
                                                 .ToArray();

        return new LightPanel(targetState, buttonToggles, joltages);
    }

    [GeneratedRegex(@"\[([.#]+)\]")]
    private static partial Regex BracketRegex();

    [GeneratedRegex(@"\(([0-9,]+)\)")]
    private static partial Regex ParenRegex();

    [GeneratedRegex(@"\{([0-9,]+)\}")]
    private static partial Regex BraceRegex();
}
