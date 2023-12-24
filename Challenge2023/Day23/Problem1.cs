using Challenge2023.Common;
using Challenge2023.Day23.Models;
using System.Text;

namespace Challenge2023.Day23;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        LoadTileMap(inputs);

        var sp = TileMap.Cast<Tile>().FirstOrDefault(t => t.StartingPoint);
        var ep = TileMap.Cast<Tile>().FirstOrDefault(t => t.EndingPoint);

        Console.WriteLine($"Starting Point: {sp}");
        Console.WriteLine($"Ending Point: {ep}");
        //var OUTPUT_FOLDER = Path.Combine(DAY_FOLDER, "Output");

        //if (!Directory.Exists(OUTPUT_FOLDER))
        //{
        //    Directory.CreateDirectory(OUTPUT_FOLDER);
        //}
        //var sb = new StringBuilder();

        //for(var r = 0; r < TileMap.GetLength(0); r++)
        //{
        //    for(var c = 0; c < TileMap.GetLength(1); c++)
        //    {
        //        sb.Append($"[{TileMap[r, c].PossibleMoves.ToString().Replace(',','.')}],");
        //    }
        //    sb.Append('\n');
        //}

        //File.WriteAllText(Path.Combine(OUTPUT_FOLDER, "output.csv"), sb.ToString());

        var solution = 0;

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

