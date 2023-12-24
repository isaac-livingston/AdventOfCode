using Challenge2023.Day23.Enums;

namespace Challenge2023.Day23.Models
{
    internal class PathFinder : IEquatable<PathFinder>
    {
        public Guid Id { get; } = Guid.NewGuid();

        public PathFinderStatus Status { get; set; } = PathFinderStatus.Hiking;

        public bool Equals(PathFinder? other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            return Equals(obj as PathFinder);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Id.ToString();
        }
    }   
}
