namespace Challenge2024.Day06;

internal record Node (int X, int Y)
{
    public Node? Up { get; set; }
    public Node? Down { get; set; }
    public Node? Left { get; set; }
    public Node? Right { get; set; }

    public void Connect(Node node, DirectionFlag direction)
    {
        switch(direction)
        {
            case DirectionFlag.Up:
                Up = node;
                break;
            case DirectionFlag.Down:
                Down = node;
                break;
            case DirectionFlag.Left:
                Left = node;
                break;
            case DirectionFlag.Right:
                Right = node;
                break;
            default:
                throw new ArgumentException($"Invalid direction: {direction}");
        }
    }
}
