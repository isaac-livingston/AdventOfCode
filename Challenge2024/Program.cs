
string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
string? defaultNameSpace = assemblyName;

int day = 7;
int problem = 1;

if (args.Length == 2)
{
    try
    {
        day = int.Parse(args[0]);
        problem = int.Parse(args[1]);
    }
    catch
    {
        ConsoleTools.PrintExceptionMessage($"params must be integers for day and problem, the given input was day:[{args[0]}] problem:[{args[1]}]");
        throw;
    }
}

var problemName = $"{defaultNameSpace}.Day{day.ToString().PadLeft(2, '0')}.Problem{problem}, {assemblyName}";
var problemType = Type.GetType(problemName);

if (problemType == null)
{
    ConsoleTools.PrintExceptionMessage($"A valid type could not be determined using the given params of [{day}] and [{problem}].\n" +
                                       $"The resulting qualified name string was: [{problemName}]");
    return;
}

var problemInstance = Activator.CreateInstance(problemType) as ProblemBase;

if (problemInstance == null)
{
    ConsoleTools.PrintExceptionMessage($"Hmmm... an instance of [{problemType}] was not able to be created.");
    return;
}
problemInstance.stopwatch.Start();
problemInstance.RunSolution();
problemInstance.stopwatch.Stop();
Console.ResetColor();
Console.WriteLine();
Console.WriteLine();
Console.WriteLine($"fin. {problemInstance.stopwatch.ElapsedMilliseconds:n2}ms");