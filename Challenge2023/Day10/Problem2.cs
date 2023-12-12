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

            inputs = [
                "FF7FSF7F7F7F7F7F---7",
                "L|LJ||||||||||||F--J",
                "FL-7LJLJ||||||LJL-77",
                "F--JF--7||LJLJ7F7FJ-",
                "L---JF-JLJ.||-FJLJJ7",
                "|F|F-JF---7F7-L7L|7|",
                "|FFJF7L7F-JF7|JL---7",
                "7-L-JL7||F7|L7F-7F7|",
                "L.L7LFJ|||||FJL7||LJ",
                "L7JLJL-JLJLJL--JLJ.L"
            ];

            stopwatch.Start();

            LoadData(inputs);

            var runFromEast = TraverseNetwork(comingInFrom: EAST);

            var y = runFromEast.Keys.Select(r => (double)r[0]).ToArray();
            var x = runFromEast.Keys.Select(c => (double)c[1]).ToArray();

            var insideCount = 0L;

            for (int r = 0; r < Grid.Length; r++)
            {
                for (int c = 0; c < Grid[r].Length; c++)
                {
                    if (!runFromEast.ContainsKey(RCNotation(r, c)))
                    {
                        insideCount++;
                    }
                }
            }

            //var solution = runFromEast.Values.ElementAt((runFromEast.Count / 2) + 1);
            var solution = insideCount;// runFromEast.Values.ElementAt((runFromEast.Count / 2) + 1);

            stopwatch.Stop();

            ConsoleTools.PrintSolutionMessage($"{solution}");
            ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
        }
    }
}
