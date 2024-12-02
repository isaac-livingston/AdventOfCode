namespace Challenge2023.Day11.Models
{
    internal class Galaxy(long spaceTimeColumn, long spaceTimeRow) : IEquatable<Galaxy>
    {
        public long SpaceTimeColumn { get; } = spaceTimeColumn;

        public long SpaceTimeRow { get; } = spaceTimeRow;

        public bool Equals(Galaxy? other)
        {
            if (other == null)
            {
                return false;
            }

            return SpaceTimeColumn == other.SpaceTimeColumn
                && SpaceTimeRow == other.SpaceTimeRow;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Galaxy);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(SpaceTimeRow, SpaceTimeColumn);
        }
    }
}
