using Challenge2023.Common;
using Challenge2023.Day20.Models;

namespace Challenge2023.Day20;

internal abstract class DayBase : ProblemBase
{
    protected const string DAY_FOLDER = "Day20";

    protected Machine Machine { get; } = new Machine();

    protected void LoadData(string[] inputs)
    {
        var comps = inputs.Select(i => i.Split("->", SPLIT_OPTS)).Select(i => new { Id = i[0], Type = i[1].Split(',', SPLIT_OPTS) }).ToList();

        Dictionary<string, string[]> hookups = [];

        foreach (var comp in comps)
        {
            var kind = comp.Id[0];
            var compKey = comp.Id[1..];
            
            if (kind == 'b')
            {
                compKey = "b" + compKey;
            }

            hookups[compKey] = comp.Type;

            switch (kind)
            {
                case '&':
                    var conj = new Conjunction() { Id = compKey};
                    Machine.Components[compKey] = conj;
                    break;
                case '%':
                    var flip = new FlipFlop() { Id = compKey };
                    Machine.Components[compKey] = flip;
                    break;
                case 'b':
                    var broa = new Broadcaster() { Id = compKey };
                    Machine.Components[compKey] = broa;
                    break;
                default:
                    throw new Exception("Unknown component type");
            }
        }

        foreach (var k in hookups.Keys)
        {
            foreach(var h in hookups[k])
            {
                if (!Machine.Components.ContainsKey(h))
                {
                    Machine.Components[h] = new Outputer() { Id = h };
                    break;
                }
            }
        }

        foreach (var k in hookups.Keys)
        {
            var comp = Machine.Components[k];

            foreach(var h in hookups[k])
            {
                var hook = Machine.Components[h];

                comp.ConnectedComponents.Add(hook);

                if (hook is Conjunction conj)
                {
                    conj.RegisterInputComponentWithMemory(comp);
                }   
            }
        }   
    }
}
