using System.Text;

namespace Challenge2024.Day10;

internal record Path
{
    public List<Step> Steps { get; init; } = [];

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var step in Steps)
        {
            sb.Append(" > ");
            sb.Append(step.ToString());
        }
        return sb.ToString();
    }
}
