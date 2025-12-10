namespace Challenge2025.Day10;

internal class Problem2 : DayBase
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day10", useTest: false);
        ParseInputs(inputs);

        long result = 0;

        foreach (var panel in Panels)
        {
            if (panel.SolveJoltage())
            {
                var joltageStr = string.Join(",", panel.Joltages);
                var pressedButtons = panel.Buttons.Where(b => b.Presses > 0)
                                                  .Select(b => $"({string.Join(",", b.Toggles)})×{b.Presses}");

                Console.WriteLine($"{{{joltageStr}}} -> {panel.MinimumPresses} presses: {string.Join(", ", pressedButtons)}");
                
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
