namespace Challenge2024.Day09;

internal class Problem1 : Day09Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day09", false);
        ParseInputs(inputs);

        var diskSize = TheDisk.Count;
        var freeSpace = TheDisk.Count(block => block is EmptyBlock);
        var fileSpace = TheDisk.Count(block => block is FileBlock);

        Console.WriteLine($"Disk size: {diskSize}");
        Console.WriteLine($"Free space: {freeSpace}");
        Console.WriteLine($"File space: {fileSpace}");

        DefragmentDisk();

        var checksum = Checksum();

        Console.WriteLine($"Checksum: {checksum}");
    }
}
