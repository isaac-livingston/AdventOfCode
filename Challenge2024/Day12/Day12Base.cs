namespace Challenge2024.Day12;

internal class Day12Base : ProblemBase
{
    private bool[,] _visited = new bool[0, 0];

    public static readonly int[][] Directions =
    [
        [-1, 0],
        [1, 0],
        [0, -1],
        [0, 1]
    ];

    private int _rows = 0;
    private int _cols = 0;

    public Plant[,] Garden { get; private set; } = new Plant[0, 0];

    public void ParseInputs(string[] inputs)
    {
        _rows = inputs.Length;
        _cols = inputs[0].Length;

        Garden = new Plant[_rows, _cols];
        _visited = new bool[_rows, _cols];

        for (var r = 0; r < inputs.Length; r++)
        {
            for (var c = 0; c < inputs[r].Length; c++)
            {
                Garden[r, c] = new Plant(r, c, inputs[r][c]);
            }
        }
    }

    public List<Plot> FindPlots()
    {
        var plots = new List<Plot>();

        for (int r = 0; r < _rows; r++)
        {
            for (int c = 0; c < _cols; c++)
            {
                if (!_visited[r, c])
                {
                    char character = Garden[r, c].PlantType;
                    var (plants, size, perimeter) = FloodFill(r, c, character);
                    plots.Add(new Plot(plants, size, perimeter));
                }
            }
        }

        return plots;
    }

    private (List<Plant> Plants, int Size, int Perimeter) FloodFill(int startRow, int startCol, char character)
    {
        int size = 0;
        int perimeter = 0;
        var plants = new List<Plant>();

        var stack = new Stack<(int Row, int Col)>();
        stack.Push((startRow, startCol));
        _visited[startRow, startCol] = true;

        while (stack.Count > 0)
        {
            var (row, col) = stack.Pop();
            size++;

            plants.Add(new Plant(row, col, character));

            foreach (var direction in Directions)
            {
                int newRow = row + direction[0];
                int newCol = col + direction[1];

                if (newRow < 0 || newRow >= _rows || newCol < 0 || newCol >= _cols)
                {
                    perimeter++;
                }
                else if (Garden[newRow, newCol].PlantType != character)
                {
                    perimeter++;
                }
                else if (!_visited[newRow, newCol])
                {
                    _visited[newRow, newCol] = true;
                    stack.Push((newRow, newCol));
                }
            }
        }

        return (plants, size, perimeter);
    }
}

internal record Plant(int Row, int Col, char PlantType)
{
    public bool LeftEdge { get; set; }
    public bool TopEdge { get; set; }
    public bool RightEdge { get; set; }
    public bool BottomEdge { get; set; }

    public override string ToString() => $"Plant at ({Row}, {Col}) of type {PlantType};  L:{LeftEdge}, T:{TopEdge}, R:{RightEdge}, B:{BottomEdge}";
}

internal record Plot(List<Plant> Plants, int Size, int Perimeter)
{
    public int PerimeterFenceCost => Size * Perimeter;
    public int SidesFenceCost => Size * Sides;

    private int? _sides = null;

    public int Sides
    {
        get
        {
            _sides ??= GetNumberOfSides();

            return _sides.Value;
        }
    }

    public int GetNumberOfSides()
    {
        PopulateNeighbors();

        var topEdges = Plants.Where(x => x.TopEdge)
                             .OrderBy(x => x.Row)
                             .ThenBy(x => x.Col);

        var bottomEdges = Plants.Where(x => x.BottomEdge)
                                .OrderBy(x => x.Row)
                                .ThenBy(x => x.Col);

        var leftEdges = Plants.Where(x => x.LeftEdge)
                              .OrderBy(x => x.Col)
                              .ThenBy(x => x.Row);

        var rightEdges = Plants.Where(x => x.RightEdge)
                               .OrderBy(x => x.Col)
                               .ThenBy(x => x.Row);

        var leftEdgeGroups = GroupEdgePlants(leftEdges, groupByColumn: false);
        var rightEdgeGroups = GroupEdgePlants(rightEdges, groupByColumn: false);
        var topEdgeGroups = GroupEdgePlants(topEdges, groupByColumn: true);
        var bottomEdgeGroups = GroupEdgePlants(bottomEdges, groupByColumn: true);

        int sides = topEdgeGroups.Count + 
                    leftEdgeGroups.Count + 
                    rightEdgeGroups.Count + 
                    bottomEdgeGroups.Count;

        return sides;
    }

    public static List<List<Plant>> GroupEdgePlants(IEnumerable<Plant> plants, bool groupByColumn)
    {
        var groupedPlants = new List<List<Plant>>();
        List<Plant> currentGroup = [];

        foreach (var plant in plants)
        {
            if (currentGroup.Count == 0)
            {
                currentGroup.Add(plant);
            }
            else
            {
                var lastPlant = currentGroup[^1];

                bool isAdjacent = groupByColumn
                    ? plant.Row == lastPlant.Row && plant.Col == lastPlant.Col + 1
                    : plant.Col == lastPlant.Col && plant.Row == lastPlant.Row + 1;

                if (isAdjacent)
                {
                    currentGroup.Add(plant);
                }
                else
                {
                    groupedPlants.Add(currentGroup);
                    currentGroup = [plant];
                }
            }
        }

        if (currentGroup.Count > 0)
        {
            groupedPlants.Add(currentGroup);
        }

        return groupedPlants;
    }

    public void PopulateNeighbors()
    {
        var plantSet = Plants.ToHashSet();

        int[] dRow = [0, -1, 0, 1];
        int[] dCol = [-1, 0, 1, 0];

        foreach (var plant in Plants)
        {
            for (int i = 0; i < 4; i++)
            {
                int neighborRow = plant.Row + dRow[i];
                int neighborCol = plant.Col + dCol[i];

                bool hasNeighbor = plantSet.Any(x=>x.Row == neighborRow && x.Col == neighborCol);

                switch (i)
                {
                    case 0: plant.LeftEdge = !hasNeighbor; break;
                    case 1: plant.TopEdge = !hasNeighbor; break;
                    case 2: plant.RightEdge = !hasNeighbor; break;
                    case 3: plant.BottomEdge = !hasNeighbor; break;
                }
            }
        }
    }

    public override string ToString() => $"A region of {Plants.FirstOrDefault()?.PlantType} plants with price {Size} * {Sides} = {SidesFenceCost}";
}
