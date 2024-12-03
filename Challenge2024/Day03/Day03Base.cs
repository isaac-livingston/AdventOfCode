using System.Text.RegularExpressions;

namespace Challenge2024.Day03;

internal class Day03Base : ProblemBase
{
    public string Program { get; private set; } = string.Empty;
    public List<(int,int)> MemorySets { get; } = [];

    public void ParseInputs(string[] inputs)
    {
        var program = string.Join("", inputs);

        Program = program;
    }

    public void LoadInstruction(string instruction)
    {
        MemorySets.AddRange(Regex.Matches(instruction, @"mul\(\d+,\d+\)")
                                 .Where(x => x.Success)
                                 .Select(x => x.Value.Replace("mul(", "").Replace(")", ""))
                                 .Select(x => x.Split(',', SPLIT_OPTS).Select(int.Parse).ToArray())
                                 .Select(x => (x[0], x[1])));
    }

    public override void RunSolution()
    {
        throw new NotImplementedException();
    }
}
