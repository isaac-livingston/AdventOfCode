using System.Diagnostics;
using System.Text;

namespace Challenge2023.Common
{
    internal abstract class ProblemBase
    {
        protected const StringSplitOptions SPLIT_OPTS = StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries;

        protected readonly Stopwatch stopwatch = new();

        public virtual string[] GetInputs(string folder, bool useTest = false, string rootFilename = "input.txt")
        {
            if (useTest)
            {
                rootFilename = "test-" + rootFilename;
            }

            var records = File.ReadAllLines(@$"{folder}\{rootFilename}", Encoding.UTF8);

            return records;
        }

        public abstract void RunSolution();
    }
}
