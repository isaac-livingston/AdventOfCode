using Challenge.Common;
using Challenge2023.Day11.Models;

namespace Challenge2023.Day11;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day11";

    protected Universe Universe { get; private set; }

    protected void LoadData(string[] inputs, long expansionFactor = 1)
    {
        var rows = new long[inputs.Length];
        var columns = new long[inputs[0].Length];

        Array.Fill(rows, 1);
        Array.Fill(columns, 1);

        Universe = new Universe(columns, rows);

        bool expand;
        for (int r = 0; r < rows.Length; r++)
        {
            expand = true;
            for (int c = 0; c < columns.Length; c++)
            {
                if (inputs[r][c] == '#')
                {
                    Universe.Galaxies.Add(new Galaxy(c, r));
                    expand = false;
                }
            }
            if (expand)
            {
                Universe.SpaceTimeRows[r] += expansionFactor;
            }
        }

        for (int c = 0; c < columns.Length; c++)
        {
            expand = true;
            for (int r = 0; r < rows.Length; r++)
            {
                if (inputs[r][c] == '#')
                {
                    expand = false;
                }
            }
            if (expand)
            {
                Universe.SpaceTimeColumns[c] += expansionFactor;
            }
        }

        Universe.GenerateGalacticPairs();
    }
}
