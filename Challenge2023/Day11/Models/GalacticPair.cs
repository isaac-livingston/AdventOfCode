namespace Challenge2023.Day11.Models
{
    internal class GalacticPair(Galaxy galaxy1, Galaxy galaxy2) : IEquatable<GalacticPair>
    {
        public Galaxy Galaxy1 { get; } = galaxy1;
        public Galaxy Galaxy2 { get; } = galaxy2;

        public (long startingColumn, long columnSteps, long startingRow, long rowSteps) GetPathData()
        {
            var leftGalaxy = Galaxy1.SpaceTimeColumn < Galaxy2.SpaceTimeColumn 
                           ? Galaxy1 
                           : Galaxy2;

            var rightGalaxy = leftGalaxy == Galaxy1 
                            ? Galaxy2 
                            : Galaxy1;

            var topGalaxy = Galaxy1.SpaceTimeRow < Galaxy2.SpaceTimeRow 
                          ? Galaxy1 
                          : Galaxy2;

            var bottomGalaxy = topGalaxy == Galaxy1 
                             ? Galaxy2 
                             : Galaxy1;

            var startingColumn = leftGalaxy.SpaceTimeColumn;
            var columnSteps = Math.Abs(startingColumn - rightGalaxy.SpaceTimeColumn);

            var startingRow = topGalaxy.SpaceTimeRow;
            var rowSteps = Math.Abs(startingRow - bottomGalaxy.SpaceTimeRow);

            return (startingColumn, columnSteps, startingRow, rowSteps);
        }

        public bool Equals(GalacticPair? other)
        {
            if (other == null)
            {
                return false;
            }

            return (Galaxy1 == other.Galaxy1 || Galaxy1 == other.Galaxy2)
                && (Galaxy2 == other.Galaxy2 || Galaxy2 == other.Galaxy1);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as GalacticPair);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Galaxy1, Galaxy2);
        }
    }
}
