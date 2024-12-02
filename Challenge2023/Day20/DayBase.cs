using Challenge.Common;
using Challenge2023.Day20.Models.ComponentModels;

namespace Challenge2023.Day20;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day20";

    protected Machine? Machine { get; private set; }

    protected void BuildMachine(string[] inputs)
    {

        var components = inputs.Select(i => i.Split("->", SPLIT_OPTS))
                               .Select(i => new { Id = i[0], ConnectedComponentIds = i[1].Split(',', SPLIT_OPTS) })
                               .ToList();

        Dictionary<string, string[]> connectedComponentMap = [];

        Dictionary<string, BaseComponent> machineParts = [];

        foreach (var component in components)
        {
            var kind = component.Id[0];

            var compKey = component.Id[1..];

            connectedComponentMap[compKey] = component.ConnectedComponentIds;

            machineParts[compKey] = kind switch
            {
                '&' => new Conjunction() { Id = compKey },
                '%' => new FlipFlop() { Id = compKey },
                'b' => new Broadcaster() { Id = compKey },
                _ => throw new Exception("Unknown component type"),
            };
        }

        // identify the outputter
        foreach (var k in connectedComponentMap.Keys)
        {
            foreach(var c in connectedComponentMap[k])
            {
                if (!machineParts.ContainsKey(c))
                {
                    machineParts[c] = new Outputer() { Id = c };
                    break;
                }
            }
        }

        foreach (var k in connectedComponentMap.Keys)
        {
            var component = machineParts[k];

            foreach(var connectedComponentId in connectedComponentMap[k])
            {
                var connectedComponent = machineParts[connectedComponentId];

                component.ConnectedComponents = [.. component.ConnectedComponents, connectedComponent];

                if (connectedComponent is Conjunction conjunction)
                {
                    conjunction.RegisterInputComponentWithMemory(component);
                }   
            }
        }

        Machine = new Machine(machineParts);
    }
}
