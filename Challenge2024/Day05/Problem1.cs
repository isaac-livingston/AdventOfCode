namespace Challenge2024.Day05;

internal class Problem1 : Day05Base
{
    public override void RunSolution()
    {
        var inputs = GetInputs(folder: "day05", false);
        ParseInputs(inputs);

        foreach(var request in UpdateRequests)
        {
            ValidateRequest(request);
        }

        var middlePagesSum = ValidRequests.Sum(GetMiddleValue);

        Console.WriteLine($"Total: {middlePagesSum}");
    }
}
