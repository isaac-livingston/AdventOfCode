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

    public void AllocateFileBlocksToStartOfDisk()
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

    public void AllocateFileBlocksWithoutFragmentation()
    {
        int right = TheDisk.Count - 1;

        while (right >= 0)
        {
            if (TheDisk[right] is FileBlock lastFileBlock)
            {
                int fileBlockStart = right;
                while (fileBlockStart > 0 && 
                       TheDisk[fileBlockStart - 1] is FileBlock previousFileBlock && 
                       previousFileBlock.FileId == lastFileBlock.FileId)
                {
                    fileBlockStart--;
                }

                int fileBlockCount = right - fileBlockStart + 1;

                for (int left = 0; left < fileBlockStart; left++)
                {
                    if (TheDisk[left] is EmptyBlock)
                    {
                        int emptyBlockStart = left;
                        while (emptyBlockStart + 1 < TheDisk.Count && 
                               TheDisk[emptyBlockStart + 1] is EmptyBlock)
                        {
                            emptyBlockStart++;
                        }

                        int emptyBlockCount = emptyBlockStart - left + 1;

                        if (emptyBlockCount >= fileBlockCount)
                        {
                            for (int i = 0; i < fileBlockCount; i++)
                            {
                                TheDisk[left + i] = TheDisk[fileBlockStart + i];
                                TheDisk[fileBlockStart + i] = new EmptyBlock(fileBlockStart + i);
                            }
                            //PrintDisk();
                            break;
                        }
                    }
                }

                right = fileBlockStart - 1;
            }
            else
            {
                right--;
            }
        }
    }

    public void PrintDisk()
    {
        for (int i = 0; i < TheDisk.Count; i++)
        {
            Console.Write(TheDisk[i]);
        }
        Console.WriteLine();
    }

    public decimal Checksum()
    {
        decimal checksum = 0;

        for (int i = 0; i < TheDisk.Count; i++)
        {
            if (TheDisk[i] is FileBlock fileBlock)
            {
                decimal n = fileBlock.FileId * i;
                checksum += n;
            }
        }

        return checksum;
    }
}
