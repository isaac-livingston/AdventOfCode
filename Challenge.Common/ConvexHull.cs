namespace Challenge.Common;

/// <summary>
/// Provides methods to compute the convex hull of a set of points in 2D space.
/// </summary>
/// <remarks>
/// Resources to learn more about convex hulls:
/// <list type="bullet">
/// <item>
/// <description>
/// Video explanation: <see href="https://www.youtube.com/watch?v=B2AJoQSZf4M">Convex Hull Algorithm by Abdul Bari</see>
/// </description>
/// </item>
/// <item>
/// <description>
/// Wikipedia article: <see href="https://en.wikipedia.org/wiki/Convex_hull">Convex Hull - Wikipedia</see>
/// </description>
/// </item>
/// <item>
/// <description>
/// Interactive visualizer: <see href="https://algorithm-visualizer.org/">Algorithm Visualizer: Convex Hull</see>
/// </description>
/// </item>
/// </list>
/// </remarks>
public static class ConvexHull
{
    /// <summary>
    /// Computes the convex hull of a given set of points in 2D space.
    /// </summary>
    /// <param name="points">A list of points, where each point is represented as a tuple of row and column coordinates.</param>
    /// <returns>A list of points forming the convex hull, in counter-clockwise order.</returns>

    public static List<(int Row, int Col)> Compute(List<(int Row, int Col)> points)
    {
        // Sort points by row, then by column
        points = [.. points.OrderBy(p => p.Row).ThenBy(p => p.Col)];

        List<(int Row, int Col)> hull = [];

        // Lower hull
        foreach (var point in points)
        {
            while (hull.Count >= 2 && CrossProduct(hull[^2], hull[^1], point) <= 0)
            {
                hull.RemoveAt(hull.Count - 1);
            }
            hull.Add(point);
        }

        // Upper hull
        int lowerHullCount = hull.Count;
        for (int i = points.Count - 1; i >= 0; i--)
        {
            var point = points[i];
            while (hull.Count > lowerHullCount && CrossProduct(hull[^2], hull[^1], point) <= 0)
            {
                hull.RemoveAt(hull.Count - 1);
            }
            hull.Add(point);
        }

        // Remove the last point because it's the same as the first point
        hull.RemoveAt(hull.Count - 1);

        return hull;
    }

    private static int CrossProduct((int Row, int Col) a, (int Row, int Col) b, (int Row, int Col) c)
    {
        // Cross product to determine the orientation of the turn
        return (b.Col - a.Col) * (c.Row - a.Row) - (b.Row - a.Row) * (c.Col - a.Col);
    }
}
