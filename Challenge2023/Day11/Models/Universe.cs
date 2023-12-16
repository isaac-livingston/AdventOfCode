namespace Challenge2023.Day11.Models
{
    internal class Universe(long[] spaceTimeColumns, long[] spaceTimeRows)
    {
        public long[] SpaceTimeColumns { get; } = spaceTimeColumns;
        public long[] SpaceTimeRows { get; } = spaceTimeRows;

        public List<Galaxy> Galaxies { get; } = [];

        public HashSet<GalacticPair> GalacticPairs { get; private set; } = [];

        public long CalculateShortestPathOfGalaticPair(GalacticPair galacticPair)
        {
            var (startingColumn, columnSteps, startingRow, rowSteps) = galacticPair.GetPathData();

            var columnSpace = 0L;
            for (long c = startingColumn; c < startingColumn + columnSteps; c++)
            {
                columnSpace += SpaceTimeColumns[c];
            }

            var rowSpace = 0L;
            for (long r = startingRow; r < startingRow + rowSteps; r++)
            {
                rowSpace += SpaceTimeRows[r];
            }

            return columnSpace + rowSpace;
        }

        public void GenerateGalacticPairs()
        {
            var pairs = new HashSet<GalacticPair>();

            for (int g1 = 0; g1 < Galaxies.Count; g1++)
            {
                for (int g2 = g1 + 1; g2 < Galaxies.Count; g2++)
                {
                    pairs.Add(new GalacticPair(Galaxies[g1], Galaxies[g2]));
                }
            }

            GalacticPairs = pairs;
        }
    }
}
