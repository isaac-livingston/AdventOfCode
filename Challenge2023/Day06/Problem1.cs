
namespace Challenge2023.Day06
{
    internal class Problem1 : Day06Base
    {
        protected override void InitializeInput(string[] inputs)
        {
            Times.AddRange(inputs[0].Split(':', SPLIT_OPTS)[1].Split(' ', SPLIT_OPTS).Select(double.Parse));
            RecordDistances.AddRange(inputs[1].Split(':', SPLIT_OPTS)[1].Split(' ', SPLIT_OPTS).Select(double.Parse));
        }

        public override void RunSolution()
        {
            stopwatch.Start();
            var inputs = GetInputs(folder: "day06");

            //inputs = [
            //    "Time:      7  15   30",
            //    "Distance:  9  40  200"
            //];

            InitializeInput(inputs);

            var solution = GetSolutions();

            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("solution: " + solution);
            Console.WriteLine();
            Console.WriteLine("duration: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}
