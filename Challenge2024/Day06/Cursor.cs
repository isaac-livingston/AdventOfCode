namespace Challenge2024.Day06;

internal class Cursor(int y, int x, DirectionFlag forwardDirection)
{
    public int X { get; private set; } = x;
    public int Y { get; private set; } = y;
    public DirectionFlag ForwardDirection { get; private set; } = forwardDirection;

    public Node? SeekNextNodeInForwardDirection(SecurityGrid grid)
    {
        int stepY = ForwardDirection switch
        {
            DirectionFlag.Up => -1,
            DirectionFlag.Down => 1,
            DirectionFlag.Left => 0,
            DirectionFlag.Right => 0,
            _ => throw new ArgumentException($"Invalid direction: {ForwardDirection}")
        };

        int stepX = ForwardDirection switch
        {
            DirectionFlag.Up => 0,
            DirectionFlag.Down => 0,
            DirectionFlag.Left => -1,
            DirectionFlag.Right => 1,
            _ => throw new ArgumentException($"Invalid direction: {ForwardDirection}")
        };

        Node? foundNode = grid.SeekNode(X, Y, stepX, stepY);

        if (foundNode != null)
        {
            X = foundNode.X - stepX;
            Y = foundNode.Y - stepY;
        }

        return foundNode;
    }

    public void TurnRight()
    {
        switch (ForwardDirection)
        {
            case DirectionFlag.Up:
                ForwardDirection = DirectionFlag.Right;
                break;
            case DirectionFlag.Down:
                ForwardDirection = DirectionFlag.Left;
                break;
            case DirectionFlag.Left:
                ForwardDirection = DirectionFlag.Up;
                break;
            case DirectionFlag.Right:
                ForwardDirection = DirectionFlag.Down;
                break;
            default:
                throw new ArgumentException($"Invalid direction: {ForwardDirection}");
        }
    }
}

