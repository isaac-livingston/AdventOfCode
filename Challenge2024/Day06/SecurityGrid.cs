using System;

namespace Challenge2024.Day06;

internal class SecurityGrid
{
    private readonly Dictionary<(int X, int Y), Node> _nodes = [];
    public char[,] Grid { get; private set; } = default!;

    public Dictionary<(int x, int y, int dx, int dy), Node?> NodeMemo { get; } = [];

    public void InitializeGrid(char[,] grid)
    {
        _nodes.Clear();
        NodeMemo.Clear();

        Grid = grid;
        int ySize = Grid.GetLength(0);
        int xSize = Grid.GetLength(1);

        for (int y1 = 0; y1 < ySize; y1++)
        {
            for (int x1 = 0; x1 < xSize; x1++)
            {
                if (Grid[y1, x1] == '#')
                {
                    _nodes[(x1, y1)] = new Node(x1, y1);
                }
            }
        }
    }

    public Node? SeekNode(int x, int y, int dX, int dY)
    {
        if (Math.Abs(dX) > 1 || Math.Abs(dY) > 1)
        {
            throw new ArgumentException("Direction values must be -1, 0, or 1.");
        }

        var key = (x, y, dX, dY);
        if (NodeMemo.TryGetValue(key, out var memoizedNode))
        {
            return memoizedNode;
        }

        int currentX = x + dX;
        int currentY = y + dY;

        int maxX = Grid.GetLength(0);
        int maxY = Grid.GetLength(1);

        Node? foundNode = null;
        while (currentX >= 0 && currentX < maxX && currentY >= 0 && currentY < maxY)
        {
            if (_nodes.TryGetValue((currentX, currentY), out foundNode))
            {
                break;
            }

            currentX += dX;
            currentY += dY;
        }

        NodeMemo[key] = foundNode;

        return foundNode;
    }
}
