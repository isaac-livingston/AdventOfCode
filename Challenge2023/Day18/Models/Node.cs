namespace Challenge2023.Day18.Models
{
    internal class Node(string direction, long steps, string color, long x, long y)
    {
        public string Direction { get; } = direction;
        
        public long Steps { get; } = steps;
        
        public string ColorHex { get; } = color;

        public long X { get; } = x;

        public long Y { get; } = y;
    }
}
