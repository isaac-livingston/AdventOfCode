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

            Source = sources;
            Destination = destinations;
            Range = ranges;
            MaxSource = SetMaxSource(sources, ranges);
        }

        public long[] Source { get; }
        public long[] Destination { get; }
        public long[] Range { get; }
        public long[] MaxSource { get; private set; }

        private static long[] SetMaxSource(long[] sources, long[] ranges)
        {
            var maxes = new long[sources.Length];
            for (int i = 0; i < sources.Length; i++)
            {
                maxes[i] = sources[i] + ranges[i];
            }

            return maxes;
        }

        public long this[long source]
        {
            get
            {
                for (var i = 0; i < Source.Length; i++)
                {
                    if (source >= Source[i] && source <= MaxSource[i])
                    {
                        return Destination[i] + (Range[i] - (MaxSource[i] - source));
                    }
                }

                return source;
            }
        }
    }
}
