using Challenge2023.Common;
using Challenge2023.Day23.Models;

namespace Challenge2023.Day23;

//https://adventofcode.com/2023/day/23

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day23";

    protected Tile[,] TileMap { get; private set; } = new Tile[0, 0];

    protected List<PathFinder> PathFinders { get; } = [];

    protected void LoadTileMap(string[] inputs)
    {
        TileMap = new Tile[inputs.Length, inputs[0].Length];

        for(var r = 0; r < inputs.Length; r++)
        {
            for(var c = 0; c < inputs[r].Length; c++)
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
    }
}
