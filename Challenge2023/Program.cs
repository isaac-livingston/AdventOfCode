using Challenge2023.Common;
using System.Reflection;

string? assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
string? defaultNameSpace = assemblyName;

int day = 7;
int problem = 1;

if (args.Length == 2)
{
    day = int.Parse(args[0]);
    problem = int.Parse(args[1]);
}

var problemName = $"{defaultNameSpace}.Day{day.ToString().PadLeft(2, '0')}.Problem{problem}, {assemblyName}";
var problemType = Type.GetType(problemName);

if (problemType == null)
{
    Console.WriteLine($"A valid type could not be determined using the given params of [{day}] and [{problem}].\n" +
                      $"The resulting qualified name string was: [{problemName}]");
    return;
}

var problemInstance = Activator.CreateInstance(problemType) as ProblemBase;

if (problemInstance == null)
{
    Console.WriteLine($"Hmmm... an instance of [{problemType}] was not able to be created.");
    return;
}

problemInstance.RunSolution();

Console.WriteLine();
Console.WriteLine("fin.");