namespace Challenge2023.Day03.Models
{
    internal class Symbol(int row, int col)
    {
        public Point Point { get; } = new Point(row, col);

        public List<Point> Influences { get; set; } = [];

        public char Value { get; set; }
    }
}
