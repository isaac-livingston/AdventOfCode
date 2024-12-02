namespace Challenge2024.Day01;

internal class Day01Base : ProblemBase
{
    public List<int> Lefts = [];
    public List<int> Rights = [];

    public void ParseInputs(string[] inputs)
    {
        foreach (var input in inputs)
        {
            var parts = input.Split(" ", SPLIT_OPTS);
            Lefts.Add(int.Parse(parts[0]));
            Rights.Add(int.Parse(parts[1]));
        }
    }

    public override void RunSolution()
    {
        throw new NotImplementedException();
    }
}
