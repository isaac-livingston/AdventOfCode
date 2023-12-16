using Challenge2023.Common;

namespace Challenge2023.Day12;

internal class Problem2 : DayBaseXII
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: DAY_FOLDER);

        //inputs = [
        //    "???.### 1,1,3",
        //    ".??..??...?##. 1,1,3",
        //    "?#?#?#?#?#?#?#? 1,3,1,6",
        //    "????.#...#... 4,1,1",
        //    "????.######..#####. 1,6,5",
        //    "?###???????? 3,2,1"
        //];

        stopwatch.Start();

        LoadData(inputs);

        var solution = Part2();

        stopwatch.Stop();

        ConsoleTools.PrintSolutionMessage($"{solution}");
        ConsoleTools.PrintDurationMessage(stopwatch.ElapsedMilliseconds);
    }
}
