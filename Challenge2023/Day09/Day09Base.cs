using Challenge.Common;

namespace Challenge2023.Day09
{
    internal abstract class Day09Base : ProblemBase
    {
        protected Dictionary<int, List<int[]>> Measurements { get; private set; } = [];

        protected void LoadMeasurements(string[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                Measurements[i] = [inputs[i].Split(' ', SPLIT_OPTS).Select(int.Parse).ToArray()];
            }
        }

        protected void ExtendMeasurements(bool intoThePast = false)
        {
            for (int m = 0; m < Measurements.Keys.Count; m++)
            {
                var set = Measurements[m];

                while(true)
                {
                    var set1 = set.Last();
                    var calc = new int[set1.Length - 1];

                    for (int i = 0; i < set1.Length - 1; i++)
                    {
                        calc[i] = set1[i + 1] - set1[i];
                    }

                    set.Add(calc);

                    if (calc.All(x => x.Equals(0)))
                    {
                        var extensions = new int[set.Count];

                        for (int s = set.Count - 1; s >= 0; s--)
                        {
                            extensions[s] = intoThePast ? set.ElementAt(s).First() : set.ElementAt(s).Last();
                        }

                        set.Add(extensions);

                        break;
                    }
                }
            }
        }
    }
}
