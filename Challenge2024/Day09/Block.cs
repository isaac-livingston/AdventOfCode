namespace Challenge2024.Day09;

internal abstract record Block(long FileId);

internal record EmptyBlock() : Block(-1)
{
    public override string ToString() => ".";
};

internal record FileBlock(long FileId) : Block(FileId)
{
    public override string ToString() => FileId.ToString();
};