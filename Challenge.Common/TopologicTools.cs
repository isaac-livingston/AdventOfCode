namespace Challenge.Common;

public static class TopologicTools
{
    /// <summary>
    /// Performs a topological sort of a directed graph using Kahn's algorithm.
    /// </summary>
    /// <param name="graph">A dictionary representing the graph where the key is a node and the value is a list of its neighbors.</param>
    /// <returns>A list of nodes in topologically sorted order.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the graph contains a cycle.</exception>
    /// <remarks>
    /// For more information, see:
    /// <list type="bullet">
    /// <item><description><a href="https://www.geeksforgeeks.org/topological-sorting-indegree-based-solution/">GeeksforGeeks: Topological Sorting</a></description></item>
    /// <item><description><a href="https://en.wikipedia.org/wiki/Topological_sorting">Wikipedia: Topological Sorting</a></description></item>
    /// <item><description><a href="https://www.techiedelight.com/kahn-topological-sort-algorithm/">Techie Delight: Kahn's Topological Sort Algorithm</a></description></item>
    /// </list>
    /// </remarks>
    public static List<int> TopologicalSort(Dictionary<int, List<int>> graph)
    {
        var inDegree = new Dictionary<int, int>();
        var zeroInDegreeQueue = new Queue<int>();
        var result = new List<int>();

        foreach (var node in graph.Keys)
        {
            if (!inDegree.ContainsKey(node))
            {
                inDegree[node] = 0;
            }

            foreach (var neighbor in graph[node])
            {
                if (inDegree.ContainsKey(neighbor))
                {
                    inDegree[neighbor]++;
                }
                else
                {
                    inDegree[neighbor] = 1;
                }
            }
        }

        foreach (var node in inDegree.Keys)
        {
            if (inDegree[node] == 0)
            {
                zeroInDegreeQueue.Enqueue(node);
            }
        }

        while (zeroInDegreeQueue.Count > 0)
        { 
            var node = zeroInDegreeQueue.Dequeue();
            result.Add(node);
            
            if(!graph.ContainsKey(node))
            {
                continue;
            }

            foreach (var neighbor in graph[node])
            {
                inDegree[neighbor]--;
                if (inDegree[neighbor] == 0)
                {
                    zeroInDegreeQueue.Enqueue(neighbor);
                }
            }
        }

        if (result.Count != inDegree.Count)
        {
            throw new InvalidOperationException("Graph has a cycle");
        }

        return result;
    }

    /// <summary>
    /// Validates if a given sequence of nodes follows the topological order.
    /// </summary>
    /// <param name="sequence">The sequence of nodes to validate.</param>
    /// <param name="topologicOrder">The topological order of nodes.</param>
    /// <returns>True if the sequence follows the topological order; otherwise, false.</returns>
    public static bool ValidateSequence(List<int> sequence, List<int> topologicOrder)
    { 
        var indexMap = topologicOrder.Select((value, index) => (value, index))
                                     .ToDictionary(x => x.value, x => x.index);

        for (int i = 1; i < sequence.Count; i++)
        {
            if (indexMap[sequence[i - 1]] > indexMap[sequence[i]])
            {
                return false;
            }
        }

        return true;
    }
}
