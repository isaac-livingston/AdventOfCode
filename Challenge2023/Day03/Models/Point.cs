namespace Challenge2023.Day03.Models
{
    internal class Point(int row, int col) : IEquatable<Point>
    {
        public int Row { get; } = row;
        public int Col { get; } = col;

        public bool Equals(Point? other)
        {
            if (other == null)
            {
                return false;
            }

            return Row == other.Row
                && Col == other.Col;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Point);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col);
        }
    }
}
