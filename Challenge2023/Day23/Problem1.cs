using Challenge2023.Common;
using Challenge2023.Day23.Models;
using Challenge2023.Day23.Visualizer;
using System.Text;

namespace Challenge2023.Day23;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER, useTest: false);

        stopwatch.Start();

        LoadTileMap(inputs);

        GoHiking();

        var arrivedPathFinders = PathFinders.Where(p => p.Status == Enums.PathFinderStatus.Arrived).ToList();

        Console.WriteLine($"Pathfinders: {arrivedPathFinders.Count}");

        var tiles = TileMap.Cast<Tile>().ToList();

        var hikes = new Dictionary<PathFinder, int>();

        foreach (var pathFinder in arrivedPathFinders)
        {
            hikes[pathFinder] = tiles.Where(t => t.PathFinders.Contains(pathFinder)).Count();
        }

        //foreach(var hike in hikes.OrderBy(x => x.Value))
        //{
        //    Console.WriteLine($"Hike: {hike.Value}\t{hike.Key}");
        //}

        //var checkem = hikes.Where(h => h.Value > 1).OrderBy(x => x.Value).ToList();

        //foreach (var xx in checkem)
        //{
        //    var plotter = new TilePlotter(TileMap.GetLength(0) * 10, TileMap.GetLength(1) * 10, tiles, xx.Key.Id);
        //    plotter.CreatePathBitmap(DAY_FOLDER, $"output_{xx.Value}_{xx.Key.Id}.png");
        //}

        //var plotter2 = new TilePlotter(TileMap.GetLength(0) * 10, TileMap.GetLength(1) * 10, tiles, Guid.NewGuid());
        //plotter2.CreatePathBitmap(DAY_FOLDER, $"output_base.png");

        var solution = hikes.Values.Max();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}

