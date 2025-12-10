namespace Challenge2025.Day10;

internal class Problem1 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day10", useTest: false);
        ParseInputs(inputs);

        long result = 0;

        foreach (var panel in Panels)
        {
            if (panel.Solve())
            {
                var targetStr = string.Join("", panel.TargetState.Select(b => b ? '#' : '.'));
                var pressedButtons = panel.Buttons.Where(b => b.Presses > 0).Select(b => b.Index);
                Console.WriteLine($"[{targetStr}] -> {panel.MinimumPresses} presses (buttons: {string.Join(",", pressedButtons)})");
                result += panel.MinimumPresses;
            }
            else
            {
                Console.WriteLine("No solution found for panel!");
            }
        }

        Console.WriteLine($"Answer: {result}");
    }
}
