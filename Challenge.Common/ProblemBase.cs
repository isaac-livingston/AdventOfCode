using System.Diagnostics;
using System.Text;

namespace Challenge.Common;

public abstract class ProblemBase
{
    protected const StringSplitOptions SPLIT_OPTS = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

    public readonly Stopwatch stopwatch = new();

    public virtual string[] GetInputs(string folder, bool useTest = false, string rootFilename = "input.txt")
    {
        if (useTest)
        {
            rootFilename = "test-" + rootFilename;
        }

        var records = File.ReadAllLines(@$"{folder}\{rootFilename}", Encoding.UTF8);

        return records;
    }

    public virtual void RunSolution()
    {
        throw new NotImplementedException();
    }
}
