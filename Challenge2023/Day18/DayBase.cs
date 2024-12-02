using Challenge.Common;
using Challenge2023.Day18.Models;

namespace Challenge2023.Day18;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day18";

    protected Node Origin { get; set; } = new Node("D", 1, "#000000", 0, 0);
    protected Lagoon Lagoon { get; set; } = new Lagoon();
    protected long Perimeter { get; set; } = 1;

    protected void LoadData(string[] inputs, bool solvePart2 = false)
    {
        var node = Origin;
        Lagoon.Queue.Enqueue(node);

        for (int i = 0; i < inputs.Length; i++)
        {
            var data = inputs[i].Replace("#", null)
                                .Replace("(", null)
                                .Replace(")", null)
                                .Split(' ', SPLIT_OPTS);

            node = MapData(node, data, solvePart2);
        }
    }

    private static long ConvertHexToLong(string hex)
    {
        return Convert.ToInt64(hex, 16);
    }

    private Node? MapData(Node? origin, string[] data, bool solvePart2)
    {
        if (origin == null)
        {
            return null;
        }

        var direction = data[0];
        var steps = long.Parse(data[1]);
        var color = data[2];

        if (solvePart2)
        {
            steps = ConvertHexToLong(color[..5]);
            direction = color.Last() switch
            {
                '0' => "R",
                '1' => "D",
                '2' => "L",
                '3' => "U",
                _ => throw new ArgumentException("Invalid direction.")
            };
        }

        var offsetX = origin.X;
        var offsetY = origin.Y;

        if (direction == "L")
        {
            offsetX = origin.X + steps;
        }
        else if (direction == "R")
        {
            offsetX = origin.X - steps;
        }
        else if (direction == "U")
        {
            offsetY = origin.Y + steps;
        }
        else if (direction == "D")
        {
            offsetY = origin.Y - steps;
        }

        var node = new Node(direction, steps, color, offsetX, offsetY);

        Lagoon.Queue.Enqueue(node);
        Perimeter += steps;

        return node;
    }

    protected double ShoeLaceAreaCalculator()
    {
        var points = Lagoon.Queue.Select(n => (n.X, n.Y)).ToList();

        var result = Shoelace.CalculateArea(points);

        return result;
        //if (points.Count < 3)
        //{
        //    throw new ArgumentException("At least three points are required to calculate area.");
        //}

        //double area = 0;
        //int j = points.Count - 1; // The last vertex is the 'previous' one to the first

        //for (int i = 0; i < points.Count; i++)
        //{
        //    area += (points[j].X + points[i].X) * (points[j].Y - points[i].Y);
        //    j = i;  // j is previous vertex to i
        //}

        //return Math.Abs(area) / 2;
    }
}
