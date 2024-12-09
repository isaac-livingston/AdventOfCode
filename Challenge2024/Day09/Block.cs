namespace Challenge2024.Day09;

internal abstract record Block(long FileId, long Position);

internal record EmptyBlock(long Position) : Block(-1, Position);

internal record FileBlock(long FileId, long Position) : Block(FileId, Position);