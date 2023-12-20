using Challenge2023.Common;
using Challenge2023.Day18.Models;

namespace Challenge2023.Day18;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day18";

    protected Node Origin { get; set; } = new Node("D", 1, "#000000", 0, 0);
    protected Lagoon Lagoon { get; set; } = new Lagoon();
    protected long Perimeter { get; set; } = 1;

    protected void LoadData(string[] inputs)
    {
        var node = Origin;
        Lagoon.Queue.Enqueue(node);

        for (int i = 0; i < inputs.Length; i++)
        {
            var data = inputs[i].Replace("#", null)
                                .Replace("(", null)
                                .Replace(")", null)
                                .Split(' ', SPLIT_OPTS);

            node = MapData(node, data);
        }
    }

    private void LogNode(Node? node)
    {
        Console.WriteLine($"X:{node.X} Y:{node.X} D:{node.Direction} C:{node.ColorHex}");
    }

    private Node? MapData(Node? origin, string[] data)
    {
        if (origin == null)
        {
            return null;
        }

        var direction = data[0];
        var steps = int.Parse(data[1]);
        var color = data[2];

        Node? node = null;

        for (var i = 0; i < steps; i++)
        {
            var locX = origin.X;
            var locY = origin.Y;

            if (direction == "L")
            {
                locX = origin.X + i;
            }
            else if (direction == "R")
            {
                locX = origin.X - i;
            }
            else if (direction == "U")
            {
                locY = origin.Y + i;
            }
            else if (direction == "D")
            {
                locY = origin.Y - i;
            }

            node = new Node(direction, steps - i, color, locX, locY);
            Lagoon.Queue.Enqueue(node);
            Perimeter++;
        }

        return node;
    }

    static long Determinant(long x1, long y1, long x2, long y2)
    {
        return x1 * y2 - x2 * y1;
    }

    protected double ComputeTrench()
    {
        var points = Lagoon.Queue.ToList();

        if (points.Count < 3)
        {
            throw new ArgumentException("At least three points are required to calculate area.");
        }

        double area = 0;
        //int j = points.Count - 1; // The last vertex is the 'previous' one to the first

        for (int i = 0; i < points.Count - 1; i++)
        {
            area += Determinant(points[i].X, points[i].Y, points[i + 1].X, points[i + 1].Y);
            //area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y);
            //j = i;  // j is previous vertex to i
        }

        return (Math.Abs(area) + Perimeter) / 2 + 1;
    }
}
