namespace Challenge2024.Day09;

internal class Day09Base : ProblemBase
{
    public List<Block> TheDisk { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        long positionAccumulator = 0;
        int fileId = 0;
        foreach (var input in inputs)
        {
            var span = input.AsSpan();
            for (int c = 0; c < span.Length; c += 2)
            {
                positionAccumulator = ProcessPair(span.Slice(c, Math.Min(2, span.Length - c)), fileId, positionAccumulator);
                fileId++;
            }
        }
    }

    private long ProcessPair(ReadOnlySpan<char> pair, int fileId, long positionAccumulator)
    {
        long fileSpace = pair.Length > 0 ? pair[0] - '0' : 0;
        long freeSpace = pair.Length > 1 ? pair[1] - '0' : 0;

        for (int i = 0; i < fileSpace; i++)
        {
            TheDisk.Add(new FileBlock(fileId, positionAccumulator));
            positionAccumulator++;
        }

        for (int i = 0; i < freeSpace; i++)
        {
            TheDisk.Add(new EmptyBlock(positionAccumulator));
            positionAccumulator++;
        }

        return positionAccumulator;
    }

    public void DefragmentDisk()
    {
        int left = 0;
        int right = TheDisk.Count - 1;

        while (left < right)
        {
            while (left < right && TheDisk[left] is not EmptyBlock)
            {
                left++;
            }

            while (left < right && TheDisk[right] is not FileBlock)
            {
                right--;
            }

            if (left < right)
            {
                (TheDisk[left], TheDisk[right]) = (TheDisk[right], TheDisk[left]);
                left++;
                right--;
            }
        }
    }

    public long Checksum()
    {
        long checksum = 0;

        int position = 0;

        while (TheDisk[position] is FileBlock)
        {
            checksum += TheDisk[position].FileId * position;
            position++;
        }

        return checksum;
    }
}
