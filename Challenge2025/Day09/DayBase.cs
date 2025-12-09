namespace Challenge2025.Day09;

internal record RedTile(int X, int Y)
{
    public static RedTile Parse(string line)
    {
        var parts = line.Split(',');
        return new RedTile(int.Parse(parts[0]), int.Parse(parts[1]));
    }
}

internal abstract class DayBase : ProblemBase
{
    protected List<RedTile> RedTiles = [];
    protected List<(int X, int Y)> PolygonVertices = [];

    protected void ParseInputs(string[] inputs)
    {
        RedTiles = [.. inputs.Select(RedTile.Parse)];
        PolygonVertices = [.. RedTiles.Select(t => (t.X, t.Y))];
    }

    /// <summary>
    /// Check if a rectangle is fully inside the polygon.
    /// Valid if no polygon edge crosses the interior of the rectangle.
    /// </summary>
    protected bool IsRectangleValid(int minX, int maxX, int minY, int maxY)
    {
        // Check each polygon edge to see if it crosses the rectangle interior
        for (int i = 0; i < PolygonVertices.Count; i++)
        {
            var (x1, y1) = PolygonVertices[i];
            var (x2, y2) = PolygonVertices[(i + 1) % PolygonVertices.Count];

            if (x1 == x2)
            {
                // Vertical edge at x=x1
                // Crosses interior if x is strictly inside and y-ranges overlap
                if (x1 > minX && x1 < maxX)
                {
                    int edgeMinY = Math.Min(y1, y2);
                    int edgeMaxY = Math.Max(y1, y2);

                    // Check if y-ranges overlap
                    if (edgeMinY < maxY && edgeMaxY > minY)
                    {
                        return false;
                    }
                }
            }
            else // y1 == y2, horizontal edge
            {
                // Horizontal edge at y=y1
                // Crosses interior if y is strictly inside and x-ranges overlap
                if (y1 > minY && y1 < maxY)
                {
                    int edgeMinX = Math.Min(x1, x2);
                    int edgeMaxX = Math.Max(x1, x2);
                    
                    // Check if x-ranges overlap
                    if (edgeMinX < maxX && edgeMaxX > minX)
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Find the largest rectangle using any two RED tiles as opposite corners.
    /// </summary>
    protected long FindLargestRectangleArea()
    {
        long maxArea = 0;

        for (int i = 0; i < RedTiles.Count; i++)
        {
            for (int j = i + 1; j < RedTiles.Count; j++)
            {
                var a = RedTiles[i];
                var b = RedTiles[j];

                long width = Math.Abs(b.X - a.X) + 1;
                long height = Math.Abs(b.Y - a.Y) + 1;
                long area = width * height;

                if (area > maxArea)
                {
                    maxArea = area;
                }
            }
        }

        return maxArea;
    }

    /// <summary>
    /// Find the largest rectangle with RED tiles as opposite corners,
    /// where the entire rectangle is inside the polygon
    /// </summary>
    protected long FindLargestInteriorRectangleArea()
    {
        var pairs = new List<(RedTile A, RedTile B, long Area)>();
        
        for (int i = 0; i < RedTiles.Count; i++)
        {
            for (int j = i + 1; j < RedTiles.Count; j++)
            {
                var a = RedTiles[i];
                var b = RedTiles[j];
                long width = Math.Abs(b.X - a.X) + 1;
                long height = Math.Abs(b.Y - a.Y) + 1;
                pairs.Add((a, b, width * height));
            }
        }

        pairs.Sort((p1, p2) => p2.Area.CompareTo(p1.Area));

        foreach (var (a, b, area) in pairs)
        {
            int minX = Math.Min(a.X, b.X);
            int maxX = Math.Max(a.X, b.X);
            int minY = Math.Min(a.Y, b.Y);
            int maxY = Math.Max(a.Y, b.Y);

            if (IsRectangleValid(minX, maxX, minY, maxY))
            {
                return area;
            }
        }

        return 0;
    }
}
