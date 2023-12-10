using Challenge2023.Common;

namespace Challenge2023.Day10
{
    internal class Problem2 : Day10Base
    {
        public override void RunSolution()
        {
            var inputs = GetInputs(folder: "day10");

            //inputs = [
            //    ".....",
            //    ".S-7.",
            //    ".|.|.",
            //    ".L-J.",
            //    "....."
            //];

            //inputs = [
            //    "7-F7-",
            //    ".FJ|7",
            //    "SJLL7",
            //    "|F--J",
            //    "LJ.LJ"
            //];

            stopwatch.Start();

            LoadData(inputs);

            var runFromEast = TraverseNetwork(comingInFrom: EAST);

            var solution = runFromEast.Values.ElementAt(runFromEast.Count / 2);

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
