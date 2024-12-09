using System.Diagnostics;

namespace Challenge2024.Day09;

internal class Problem2 : Day09Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day09", false);
        ParseInputs(inputs);

        AllocateFileBlocksWithoutFragmentation();

        var checksum = Checksum();

        Console.WriteLine($"Checksum: {checksum}");
    }
}
