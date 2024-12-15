namespace Challenge2024.Day14;

internal class Problem2 : Day14Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day14", false);
        ParseInputs(inputs, maxRow: 102, maxColumn: 100);

        (int q0, int q1, int q2, int q3) quadCounts = GetDeltaForceQuadrants(print: false);

        var d0 = quadCounts.q0;
        var d1 = quadCounts.q1;
        var d2 = quadCounts.q2;
        var d3 = quadCounts.q3;

        var countAtHigh = 0;
        var latestHighQuad = "";

        for (int count = 0; count < 7371; count++)
        {
            AdvanceDeltaForce();
            quadCounts = GetDeltaForceQuadrants(print: false);

            if (quadCounts.q0 > d0)
            {
                d0 = quadCounts.q0;
                countAtHigh = count;
                latestHighQuad = "q0";
            }

            if (quadCounts.q1 > d1)
            {
                d1 = quadCounts.q1;
                countAtHigh = count;
                latestHighQuad = "q1";
            }

            if (quadCounts.q2 > d2)
            {
                d2 = quadCounts.q2;
                countAtHigh = count;
                latestHighQuad = "q2";
            }

            if (quadCounts.q3 > d3)
            {
                d3 = quadCounts.q3;
                countAtHigh = count;
                latestHighQuad = "q3";
            }

            //hacky as hell, don't ask me why 311... just that running 
            //the loop for 10_000 iterations was enough to find a magic number
            if (d0 == 311 || d1 == 311 || d2 == 311 || d3 == 311)
            {
                Console.WriteLine($"Found magic number at count: {count + 1}");
                break;
            }
        }

        Console.WriteLine("");
        PrintDeltaForce();
    }
}
