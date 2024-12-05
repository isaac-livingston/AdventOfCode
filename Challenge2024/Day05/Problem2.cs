namespace Challenge2024.Day05;

internal class Problem2 : Day05Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day05", false);
        ParseInputs(inputs);

        foreach (var request in UpdateRequests)
        {
            ValidateRequest(request);
        }

        var invalidRequests = UpdateRequests.Except(ValidRequests).ToList();
        var ruleFails = RequestRuleFailures.Select(x => x.Key).ToList();

        var corrected = new List<int[]>();

        foreach(var rule in ruleFails)
        {
            var correctedOrder = CorrectOrder(rule);
            corrected.Add(correctedOrder);
        }

        var middlePagesSum = corrected.Sum(GetMiddleValue);

        Console.WriteLine($"Total: {middlePagesSum}");
    }

    private int[] CorrectOrder(int[] update)
    {
        var orderedList = update.ToList();

        bool swapped;

        do
        {
            swapped = false;

            foreach (var (before, after) in PageOrderRules)
            {
                int beforeIndex = orderedList.IndexOf(before);
                int afterIndex = orderedList.IndexOf(after);

                if (beforeIndex != -1 
                 && afterIndex != -1 
                 && beforeIndex > afterIndex)
                {
                    orderedList.RemoveAt(beforeIndex);
                    orderedList.Insert(afterIndex, before);
                    swapped = true;
                }
            }
        } while (swapped);

        return [.. orderedList];
    }
}
