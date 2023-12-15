using Challenge2023.Common;
using Challenge2023.Day10.Models;

namespace Challenge2023.Day10;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER);

        stopwatch.Start();

        LoadData(inputs);

        var runFromEast = TraverseNetwork(comingInFrom: EAST);

        var loopPoints = runFromEast.Keys.Select(x =>
        {
            var noR = x.Replace("r", null);
            var vals = noR.Split('c', SPLIT_OPTS);
            var xy = new Point(int.Parse(vals[1]), int.Parse(vals[0]));
            return xy;
        }).ToList();

        var polygon = new Polygon(loopPoints);
        var containedPoints = new List<Point>();

        for (int r = 0; r < Grid.Length; r++)
        {
            for (int c = 0; c < Grid[r].Length; c++)
            {
                var point = new Point(c, r);

                var maybeKey = RCNotation(r, c);

                if (!runFromEast.ContainsKey(maybeKey) && polygon.IsInside(point))
                {
                    containedPoints.Add(point);
                }
            }
        }

        var solution = containedPoints.Count;

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
