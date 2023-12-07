using Challenge2023.Common;

namespace Challenge2023.Day06
{
    internal abstract class Day06Base : ProblemBase
    {
        protected List<double> Times { get; set; } = [];
        protected List<double> RecordDistances { get; set; } = [];

        protected abstract void InitializeInput(string[] inputs);

        protected double GetSolutions()
        {
            var solutions = new List<int>();

            for (int i = 0; i < Times.Count; i++)
            {
                solutions.Add(WaysToBeatRecord(Times[i], RecordDistances[i]));
            }

            return solutions.Aggregate(1, (x, y) => x * y);
        }

        private static int WaysToBeatRecord(double time, double recordDistance)
        {
            var ways = 0;

            for (double i = 0; i < time; i++)
            {
                ways += i * (time - i) > recordDistance ? 1 : 0;
            }

            return ways;
        }
    }
}
