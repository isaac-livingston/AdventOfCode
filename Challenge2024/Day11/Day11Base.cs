namespace Challenge2024.Day11;

internal class Day11Base : ProblemBase
{
    public LinkedList<Stone> Stones { get; private set; } = [];

    public void ParseInputs(string[] inputs)
    {
        for(int i = 0; i < inputs.Length; i++)
        {
            var stones = inputs[i].Split(' ')
                                 .Select(x => new Stone(long.Parse(x)));

            foreach (var stone in stones)
            {
                Stones.AddLast(stone);
            }
        }
    }

    public void ProcessStones(LinkedList<Stone>? stones = null)
    {
        stones ??= Stones;

        var current = stones.First;

        while (current != null)
        {
            var next = current.Next;
            ProcessStone(stones, current);
            current = next;
        }
    }

    private static void ProcessStone(LinkedList<Stone> stones, LinkedListNode<Stone> currentStoneNode)
    {
        if (currentStoneNode == null) return;

        var newStones = currentStoneNode.Value.Blink();
        var nextNode = currentStoneNode.Next;

        stones.Remove(currentStoneNode);

        if (nextNode == null)
        {
            foreach (var stone in newStones)
            {
                stones.AddLast(stone);
            }
        }
        else
        {
            foreach (var stone in newStones)
            {
                stones.AddBefore(nextNode, stone);
            }
        }
    }

    public void PrintStones(LinkedList<Stone>? stones = null)
    {
        stones ??= Stones;

        var current = stones.First;
        while (current != null)
        {
            Console.Write($"{current.Value} ");
            current = current.Next;
        }
        Console.WriteLine();
    }
}

internal record Stone(long Value)
{
    public Stone[] Blink()
    {
        if (Value == 0)
        {
            return [new Stone(1)];
        }
        
        var (even, digits) = StoneTools.CheckIfNumberHasEvenDigits(Value);

        if (even)
        {
            var (left, right) = StoneTools.SplitNumber(Value, digits);
            return [new Stone(left), new Stone(right)];
        }

        return [new Stone(Value * 2024)];
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}

internal static class StoneTools
{
    public static (bool even, long digits) CheckIfNumberHasEvenDigits(long number)
    {
        long digits = (long)Math.Log10(number) + 1;

        if (digits % 2 != 0)
        {
            return (false, -1);
        }

        return (true, digits);
    }

    public static (long left, long right) SplitNumber(long number, long digits)
    {
        long halfDigits = digits / 2;
        long divisor = (long)Math.Pow(10, halfDigits);

        long left = number / divisor;
        long right = number % divisor;

        return (left, right);
    }
}
