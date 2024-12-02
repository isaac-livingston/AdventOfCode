namespace Challenge.Common;

public class Shoelace
{
    public static double CalculateArea(List<(long x, long y)> points)
    {
        var area = 0.0;
        var j = points.Count - 1;
        for (var i = 0; i < points.Count; i++)
        {
            area += (points[j].x + points[i].x) * (points[j].y - points[i].y);
            j = i;
        }
        return Math.Abs(area / 2);
    }
}
