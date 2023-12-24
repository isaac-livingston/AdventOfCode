using Challenge2023.Common;
using Challenge2023.Day23.Enums;
using Challenge2023.Day23.Models;

namespace Challenge2023.Day23;

//https://adventofcode.com/2023/day/23

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day23";

    protected Tile[,] TileMap { get; private set; } = new Tile[0, 0];

    protected Tile? StartingTile { get; private set; }

    protected Tile? EndingTile { get; private set; }

    protected List<PathFinder> PathFinders { get; } = [];

    protected void LoadTileMap(string[] inputs)
    {
        TileMap = new Tile[inputs.Length, inputs[0].Length];

        for (var r = 0; r < inputs.Length; r++)
        {
            for (var c = 0; c < inputs[r].Length; c++)
            {
                char? up = null;
                char? down = null;
                char? left = null;
                char? right = null;

                if (r > 0)
                {
                    up = inputs[r - 1][c];
                }

                if (r < inputs.Length - 1)
                {
                    down = inputs[r + 1][c];
                }

                if (c > 0)
                {
                    left = inputs[r][c - 1];
                }

                if (c < inputs[r].Length - 1)
                {
                    right = inputs[r][c + 1];
                }

                TileMap[r, c] = new Tile(r, c, inputs[r][c], up, down, left, right);
            }
        }

        StartingTile = TileMap.Cast<Tile>().FirstOrDefault(t => t.StartTile);

        EndingTile = TileMap.Cast<Tile>().FirstOrDefault(t => t.EndTile);
    }

    protected void GoHiking()
    {
        var firstPathFinder = new PathFinder
        {
            Row = StartingTile.Row,
            Column = StartingTile.Column,
            NextMove = StartingTile.PossibleMoves
        };

        PathFinders.Add(firstPathFinder);

        while (PathFinders.Any(pf => pf.Status == PathFinderStatus.Hiking))
        {
            MovePathFinders();
        }
    }

    private void MovePathFinders()
    {
        foreach (var pf in PathFinders.ToList())
        {
            if (pf.Status == PathFinderStatus.Hiking)
            {
                var rowMove = 0;
                var colMove = 0;

                if (pf.NextMove == Moves.D)
                {
                    rowMove++;
                }
                else if (pf.NextMove == Moves.U)
                {
                    rowMove--;
                }
                else if (pf.NextMove == Moves.L)
                {
                    colMove--;
                }
                else if (pf.NextMove == Moves.R)
                {
                    colMove++;
                }

                var tile = TileMap[pf.Row + rowMove, pf.Column + colMove];

                if (tile.PathFinderAllowed(pf))
                {
                    pf.Row += rowMove;
                    pf.Column += colMove;

                    var nextMoves = tile.EnterTile(pf, pf.ArrivedFrom);

                    if (nextMoves.All(m => m == Moves.N))
                    {
                        if (tile == EndingTile)
                        {
                            pf.Status = PathFinderStatus.Arrived;
                        }
                        else
                        {
                            pf.Status = PathFinderStatus.Dead;
                        }
                    }
                    else
                    {
                        pf.NextMove = nextMoves[0];

                        for (var m = 1; m < nextMoves.Count; m++)
                        {
                            var newPf = new PathFinder
                            {
                                Row = pf.Row,
                                Column = pf.Column,
                                NextMove = nextMoves[m]
                            };

                            newPf.Ancestors.Add(pf);

                            foreach (var ancestor in pf.Ancestors)
                            {
                                newPf.Ancestors.Add(ancestor);
                            }

                            PathFinders.Add(newPf);

                            BackfillTilesWithPathFinder(pf, newPf);
                        }
                    }
                }
                else
                {
                    pf.Status = PathFinderStatus.Dead;
                }
            }
        }
    }

    private void BackfillTilesWithPathFinder(PathFinder currentPathFinder, PathFinder pathFinderToAdd)
    {
        foreach(var tile in TileMap)
        {
            if (tile.VisitedByPathFinder(currentPathFinder))
            {
                tile.BackfillPathFinder(pathFinderToAdd);
            }
        }
    }
}
