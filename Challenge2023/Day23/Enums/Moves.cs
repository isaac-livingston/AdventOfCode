namespace Challenge2023.Day23.Enums
{
    [Flags]
    internal enum Moves
    {
        N = 1 << 0,
        U = 1 << 1,
        D = 1 << 2,
        L = 1 << 3,
        R = 1 << 4
    }
}
