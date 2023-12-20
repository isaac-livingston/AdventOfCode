namespace Challenge2023.Day18.Models
{
    internal class Node(string direction, int steps, string color, int x, int y)
    {
        public string Direction { get; } = direction;
        
        public int Steps { get; } = steps;
        
        public string ColorHex { get; } = color;

        public int X { get; } = x;

        public int Y { get; } = y;
    }
}
