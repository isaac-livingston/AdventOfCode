namespace Challenge2024.Day08;

internal record Antenna(string Frequency, int X, int Y)
{
    public override string ToString() => $"{Frequency} ({X}, {Y})";
}
