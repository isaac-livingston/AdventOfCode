namespace Challenge2023.Day05.Models
{
    internal class Map
    {
        public Map(List<string> tables)
        {
            var tableRecords = tables.Select(table => table.Split(' ').Select(long.Parse).ToArray()).ToArray();

            var sources = new long[tableRecords.Length];
            var destinations = new long[tableRecords.Length];
            var ranges = new long[tableRecords.Length];

            for (int i = 0; i < tableRecords.Length; i++)
            {
                var vx = tableRecords[i];
                destinations[i] = tableRecords[i][0];
                sources[i] = tableRecords[i][1];
                ranges[i] = tableRecords[i][2];
            }

            Table = new MapTable(sources,
                                 destinations,
                                 ranges);
        }

        public long this[long key]
        {
            get => Table[key];
        }

        private MapTable Table { get; }
    }
}
