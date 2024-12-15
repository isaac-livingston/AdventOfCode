namespace Challenge2024.Day14;

internal class Problem1 : Day14Base
{
    public override void RunSolution()
    {
        var test = false;

        var inputs = GetInputs(folder: "day14", test, "input.txt");

        if (test)
        {
            ParseInputs(inputs, maxRow: 6, maxColumn: 10);
        }
        else
        {
            ParseInputs(inputs, maxRow: 102, maxColumn: 100);
        }

        for(int i = 0; i < 100; i++)
        {
            AdvanceDeltaForce();
        }

        (int q0, int q1, int q2, int q3) quadCounts = GetDeltaForceQuadrants(false);

        var safetyFactor = quadCounts.q0 * quadCounts.q1 * quadCounts.q2 * quadCounts.q3;

        Console.WriteLine("");
        Console.WriteLine($"Safety Factor: {safetyFactor}");
    }
}
