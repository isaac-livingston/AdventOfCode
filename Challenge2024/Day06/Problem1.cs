using System;

namespace Challenge2024.Day06;

internal class Problem1 : Day06Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day06", false);
        ParseInputs(inputs);

        var steppedOn = new HashSet<(int x, int y)>();

        var currentStep = (SecurityGuard.X, SecurityGuard.Y);
        var currentNode = SecurityGuard.SeekNextNodeInForwardDirection(SecurityGrid);
        var endingStep = (SecurityGuard.X, SecurityGuard.Y);

        RecordSteps(steppedOn, currentStep, endingStep);

        SecurityGuard.TurnRight();
        List<Node> visitedNodes = [];

        while (currentNode != null)
        {
            currentStep = (SecurityGuard.X, SecurityGuard.Y);
            var nextNode = SecurityGuard.SeekNextNodeInForwardDirection(SecurityGrid);
            endingStep = (SecurityGuard.X, SecurityGuard.Y);
            RecordSteps(steppedOn, currentStep, endingStep);
            visitedNodes.Add(currentNode);
            currentNode = nextNode;
            if (currentNode != null)
            {
                SecurityGuard.TurnRight();
            }
        }

        currentStep = (SecurityGuard.X, SecurityGuard.Y);
        endingStep = GetExitStep(currentStep.X, currentStep.Y, SecurityGuard.ForwardDirection);
        RecordSteps(steppedOn, currentStep, endingStep);

        Console.WriteLine($"Total Steps: {steppedOn.Count}");
    }

    private static void RecordSteps(HashSet<(int x, int y)> steppedOn, (int x, int y) currentStep, (int x, int y) endingStep)
    {
        int x1 = currentStep.x;
        int y1 = currentStep.y;
        int x2 = endingStep.x;
        int y2 = endingStep.y;

        if (x1 == x2)
        {
            int step = y1 < y2 ? 1 : -1;
            for (int y = y1; y != y2 + step; y += step)
            {
                steppedOn.Add((x1, y));
            }
        }
        else if (y1 == y2)
        {
            int step = x1 < x2 ? 1 : -1;
            for (int x = x1; x != x2 + step; x += step)
            {
                steppedOn.Add((x, y1));
            }
        }
        else
        {
            throw new ArgumentException("Steps must progress either vertically or horizontally.");
        }
    }

    public (int X, int Y) GetExitStep(int x, int y, DirectionFlag direction)
    {
        int ySize = SecurityGrid.Grid.GetLength(0);
        int xSize = SecurityGrid.Grid.GetLength(1);

        if (direction == DirectionFlag.Up)
        {
            return (x, 0);
        }
        else if (direction == DirectionFlag.Down)
        {
            return (x, ySize - 1);
        }
        else if (direction == DirectionFlag.Left)
        {
            return (0, y);
        }
        else if (direction == DirectionFlag.Right)
        {
            return (xSize - 1, y);
        }
        else
        {
            throw new ArgumentException($"Invalid direction: {direction}");
        }
    }
}
