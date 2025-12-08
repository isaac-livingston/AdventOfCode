namespace Challenge2025.Day07;

internal record TachyonParticle(int Column, long Rank = 1)
{
    public (TachyonParticle Left, TachyonParticle Right) Split()
    {
        var left = new TachyonParticle(Column - 1, Rank);
        var right = new TachyonParticle(Column + 1, Rank);
        return (left, right);
    }
}

internal abstract class DayBase : ProblemBase
{
    protected char[][] Grid = [];
    protected int StartColumn;
    protected int TotalSplits;

    protected void ParseInputs(string[] inputs)
    {
        Grid = [.. inputs.Select(line => line.ToCharArray())];

        StartColumn = Array.IndexOf(Grid[0], 'S');
        if (StartColumn == -1)
        {
            throw new InvalidOperationException("Could not find 'S' in the first row");
        }
    }

    protected List<TachyonParticle> SimulateBeam()
    {
        TotalSplits = 0;

        var activeBeams = new List<TachyonParticle>
        {
            new(StartColumn)
        };

        for (int row = 1; row < Grid.Length; row++)
        {
            activeBeams = ProcessRow(row, activeBeams);
        }

        return activeBeams;
    }

    private List<TachyonParticle> ProcessRow(int row, List<TachyonParticle> incomingBeams)
    {
        var beamsByColumn = new Dictionary<int, List<TachyonParticle>>();

        foreach (var beam in incomingBeams)
        {
            int col = beam.Column;

            if (col < 0 || col >= Grid[row].Length)
            {
                continue;
            }

            char cell = Grid[row][col];

            if (cell == '.' || cell == 'S')
            {
                AddBeamToColumn(beamsByColumn, col, beam);
            }
            else if (cell == '^')
            {
                TotalSplits++;
                var (left, right) = beam.Split();

                AddBeamToColumn(beamsByColumn, left.Column, left);
                AddBeamToColumn(beamsByColumn, right.Column, right);
            }
        }

        var result = new List<TachyonParticle>();
        foreach (var (col, beamsAtCol) in beamsByColumn)
        {
            if (beamsAtCol.Count == 1)
            {
                result.Add(beamsAtCol[0]);
            }
            else
            {
                long mergedRank = beamsAtCol.Sum(b => b.Rank);
                var merged = new TachyonParticle(col, mergedRank);
                result.Add(merged);
            }
        }

        return result;
    }

    private static void AddBeamToColumn(Dictionary<int, List<TachyonParticle>> dict, int col, TachyonParticle beam)
    {
        if (!dict.TryGetValue(col, out List<TachyonParticle>? value))
        {
            value = [];
            dict[col] = value;
        }

        value.Add(beam);
    }
}

