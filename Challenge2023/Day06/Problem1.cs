
namespace Challenge2023.Day06
{
    internal class Problem1 : Day06Base
    {
        public override void RunSolution()
        {
            stopwatch.Start();
            var inputs = GetInputs(folder: "day06");

            stopwatch.Stop();

            Console.WriteLine();
            Console.WriteLine("duration: " + stopwatch.ElapsedMilliseconds + " ms");
        }
    }
}
