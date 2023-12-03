using System.Text;

namespace Challenge2023.Common
{
    internal abstract class ProblemBase
    {
        public virtual string[] GetInputs(string folder, string filename = "input.txt")
        {
            var records = File.ReadAllLines(@$"{folder}\{filename}", Encoding.UTF8);

            return records;
        }

        public abstract void RunSolution();
    }
}
