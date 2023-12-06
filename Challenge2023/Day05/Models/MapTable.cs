namespace Challenge2023.Day05.Models
{
    internal class MapTable(long[] sources, long[] destinations, long[] ranges)
    {
        public long[] Source { get; } = sources;
        public long[] Destination { get; } = destinations;
        public long[] Range { get; } = ranges;
        public long[] MaxSource { get; private set; } = SetMaxSource(sources, ranges);

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
