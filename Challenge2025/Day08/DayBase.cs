namespace Challenge2025.Day08;

internal record JunctionBox(int X, int Y, int Z)
{
    public static JunctionBox Parse(string line)
    {
        var parts = line.Split(',');
        return new JunctionBox(
            int.Parse(parts[0]),
            int.Parse(parts[1]),
            int.Parse(parts[2])
        );
    }

    public long DistanceSquaredTo(JunctionBox other)
    {
        long dx = X - other.X;
        long dy = Y - other.Y;
        long dz = Z - other.Z;
        return dx * dx + dy * dy + dz * dz;
    }
}

internal record JunctionPair(JunctionBox A, JunctionBox B, long DistanceSquared)
{
    public double Distance => Math.Sqrt(DistanceSquared);
}

internal class Circuit
{
    public HashSet<JunctionBox> Members { get; } = [];
    public HashSet<(JunctionBox, JunctionBox)> DirectConnections { get; } = [];

    public Circuit() { }

    public Circuit(JunctionBox initial)
    {
        Members.Add(initial);
    }

    public bool Contains(JunctionBox box) => Members.Contains(box);

    public bool AreDirectlyConnected(JunctionBox a, JunctionBox b)
    {
        return DirectConnections.Contains((a, b)) || DirectConnections.Contains((b, a));
    }

    public void Connect(JunctionBox a, JunctionBox b)
    {
        Members.Add(a);
        Members.Add(b);
        DirectConnections.Add((a, b));
    }

    public void Merge(Circuit other)
    {
        foreach (var member in other.Members)
        {
            Members.Add(member);
        }
        foreach (var conn in other.DirectConnections)
        {
            DirectConnections.Add(conn);
        }
    }
}

internal abstract class DayBase : ProblemBase
{
    protected List<JunctionBox> JunctionBoxes = [];
    protected List<JunctionPair> AllPairs = [];
    protected List<Circuit> Circuits = [];

    protected void ParseInputs(string[] inputs)
    {
        JunctionBoxes = [.. inputs.Select(JunctionBox.Parse)];
    }

    protected void BuildAllPairs()
    {
        var pairs = new List<JunctionPair>();

        for (int i = 0; i < JunctionBoxes.Count; i++)
        {
            for (int j = i + 1; j < JunctionBoxes.Count; j++)
            {
                var a = JunctionBoxes[i];
                var b = JunctionBoxes[j];
                var distSq = a.DistanceSquaredTo(b);
                pairs.Add(new JunctionPair(a, b, distSq));
            }
        }

        AllPairs = [.. pairs.OrderBy(p => p.DistanceSquared)];
    }

    protected void MakeConnections(int connectionCount)
    {
        Circuits = [.. JunctionBoxes.Select(jb => new Circuit(jb))];

        int connectionsMade = 0;

        foreach (var pair in AllPairs)
        {
            if (connectionsMade >= connectionCount)
                break;

            var a = pair.A;
            var b = pair.B;

            var circuitA = Circuits.First(c => c.Contains(a));
            var circuitB = Circuits.First(c => c.Contains(b));

            if (circuitA == circuitB && circuitA.AreDirectlyConnected(a, b))
            {
                continue;
            }

            if (circuitA == circuitB)
            {
                circuitA.Connect(a, b);
            }
            else
            {
                circuitA.Connect(a, b);
                circuitA.Merge(circuitB);
                Circuits.Remove(circuitB);
            }

            connectionsMade++;
        }
    }

    protected JunctionPair ConnectUntilSingleCircuit()
    {
        Circuits = [.. JunctionBoxes.Select(jb => new Circuit(jb))];

        JunctionPair? lastMergingPair = null;
        int connectionsMade = 0;

        foreach (var pair in AllPairs)
        {
            if (Circuits.Count == 1)
            {
                break;
            }

            var a = pair.A;
            var b = pair.B;

            var circuitA = Circuits.First(c => c.Contains(a));
            var circuitB = Circuits.First(c => c.Contains(b));

            if (circuitA == circuitB && circuitA.AreDirectlyConnected(a, b))
            {
                continue;
            }

            if (circuitA == circuitB)
            {
                circuitA.Connect(a, b);
            }
            else
            {
                circuitA.Connect(a, b);
                circuitA.Merge(circuitB);
                Circuits.Remove(circuitB);
                lastMergingPair = pair;
            }

            connectionsMade++;
        }

        Console.WriteLine($"Last merging pair: ({lastMergingPair!.A.X},{lastMergingPair.A.Y},{lastMergingPair.A.Z}) <-> ({lastMergingPair.B.X},{lastMergingPair.B.Y},{lastMergingPair.B.Z})");

        return lastMergingPair!;
    }

    protected long CalculateTopCircuitsProduct(int topN)
    {
        var topSizes = Circuits.Select(c => c.Members.Count)
                               .OrderByDescending(s => s)
                               .Take(topN)
                               .ToList();

        return topSizes.Aggregate(1L, (acc, size) => acc * size);
    }
}

