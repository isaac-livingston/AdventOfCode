namespace Challenge2024.Day09;

internal abstract record Block(long FileId, long Position);

internal record EmptyBlock(long Position) : Block(-1, Position)
{
    public override string ToString() => ".";
};

internal record FileBlock(long FileId, long Position) : Block(FileId, Position)
{
    public override string ToString() => FileId.ToString();
};